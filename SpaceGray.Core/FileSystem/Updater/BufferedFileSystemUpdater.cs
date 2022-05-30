using SpaceGray.Core.Error;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SpaceGray.Core.FileSystem;

public class BufferedFileSystemUpdater : IBufferedFileSystemUpdater
{
    private readonly FileSystemState fileSystemState;
    private long totalSize;
    private long totalCount;
    private readonly BlockingCollection<object> nodesToDiscover;
    private readonly List<FileSystemNode> nodesToCalculateSize;
    private FileSystemEvent nextFileSystemEvent;
    private readonly FileSystemEventsHandler fileSystemEventsHandler;
    private readonly BlockingCollection<object> fileSystemEvents;
    private readonly BlockingCollection<object>[] blockingCollections;
    public bool IsEmpty => nodesToDiscover.Count == 0 && fileSystemEvents.Count == 0;
    public event IBufferedFileSystemUpdater.UpdatedEventHandler Updated;

    public BufferedFileSystemUpdater(FileSystemState fileSystemState, ErrorState errorState)
    {
        this.fileSystemState = fileSystemState;
        totalSize = totalCount = 0L;
        nodesToDiscover = new();
        nodesToCalculateSize = new();
        nextFileSystemEvent = null;
        fileSystemEvents = new();
        fileSystemEventsHandler = new(this, fileSystemState, errorState);
        blockingCollections = new BlockingCollection<object>[]
            { nodesToDiscover, fileSystemEvents };
    }

    public void AddFileSystemNode(FileSystemNode node)
    {
        if (node.HasContent) nodesToDiscover.Add(node);
        else
        {
            totalSize += node.Size;
            totalCount++;
        }
        nodesToCalculateSize.Add(node);
    }
    public FileSystemNode TryGetNextFileSystemNodeSync()
    {
        BlockingCollection<object>.TakeFromAny(blockingCollections, out object item);
        if (item is FileSystemNode node) return node;
        if (item is FileSystemEvent fileSystemEvent)
        { nextFileSystemEvent = fileSystemEvent; }
        return null;
    }
    public void WakeUp() => nodesToDiscover.Add(null);

    public void AddFileSystemEvent(FileSystemEvent fileSystemEvent) =>
        fileSystemEvents.Add(fileSystemEvent);
    public int GetCountOfFileSystemEvents() =>
        fileSystemEvents.Count + (nextFileSystemEvent != null ? 1 : 0);
    public FileSystemEvent GetNextFileSystemEvent()
    {
        if (nextFileSystemEvent != null)
        {
            var fileSystemEvent = nextFileSystemEvent;
            nextFileSystemEvent = null;
            return fileSystemEvent;
        }
        return fileSystemEvents.Take() as FileSystemEvent;
    }

    private bool IsUpdate()
    {
        if (nodesToDiscover.Count == 0) return true;
        long rootSize;
        lock (fileSystemState.Locker) rootSize = fileSystemState.Root.Size;
        return FileSystemUpdateBalancer.IsReadyForUpdateSize(totalSize, rootSize) ||
            FileSystemUpdateBalancer.IsReadyForUpdateCount(
                totalCount, totalCount - nodesToCalculateSize.Count);
    }
    public void TryUpdate()
    {
        if (IsUpdate())
        {
            lock (fileSystemState.Locker)
            {
                foreach (var node in nodesToCalculateSize)
                { if (!node.IsCalculated) FileSystemSizeUtil.CalculateSize(node); }
                nodesToCalculateSize.Clear();
                fileSystemEventsHandler.ProcessFileSystemEvents();
            }
            Updated?.Invoke(this, EventArgs.Empty);
        }
    }
}
