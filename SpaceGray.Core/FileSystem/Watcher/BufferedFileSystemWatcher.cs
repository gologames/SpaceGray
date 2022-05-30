using SpaceGray.Core.Error;
using System;
using System.IO;

namespace SpaceGray.Core.FileSystem;

public class BufferedFileSystemWatcher
{
    private const int MaxBufferSize = 65536;
    private readonly ErrorState errorState;
    private FileSystemWatcher watcher;
    private string rootFolderName;
    private IBufferedFileSystemUpdater fileSystemUpdater;

    public BufferedFileSystemWatcher(ErrorState errorState) => this.errorState = errorState;

    private (bool, bool, bool) CheckPath(string name)
    {
        var hasPrefixFolder = rootFolderName != null;
        var isInsideRoot = hasPrefixFolder && FileSystemPathUtil.IsRootFolder(name, rootFolderName);
        return (hasPrefixFolder, isInsideRoot, hasPrefixFolder && rootFolderName == name);
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        var (hasPrefixFolder, isInsideRoot, isRoot) = CheckPath(e.Name);
        if (!hasPrefixFolder || isInsideRoot || isRoot)
        {
            var name = isInsideRoot ? FileSystemPathUtil.RemoveRootFolder(e.Name, rootFolderName) : e.Name;
            fileSystemUpdater.AddFileSystemEvent(new(e.ChangeType, isRoot, e.FullPath, name));
        }
    }

    private bool ProcessOnRenamedErrors(RenamedEventArgs e)
    {
        if (e.OldName == null)
        {
            if (e.Name != null)
            { ErrorReportUtil.ReportError(errorState, new RenamedOldPathIsUnknownReport(e.Name)); }
        }
        else if (e.Name == null)
        { ErrorReportUtil.ReportError(errorState, new RenamedNewPathIsUnknownReport(e.OldName)); }
        return e.OldName == null || e.Name == null;
    }
    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        if (ProcessOnRenamedErrors(e)) return;
        var (hasPrefixFolder, isInsideRoot, isRoot) = CheckPath(e.OldName);
        if (isRoot) rootFolderName = e.Name;
        if (!hasPrefixFolder || isInsideRoot || isRoot)
        {
            var oldName = isInsideRoot ? FileSystemPathUtil.RemoveRootFolder(e.OldName, rootFolderName) : e.OldName;
            var newName = isInsideRoot ? FileSystemPathUtil.RemoveRootFolder(e.Name, rootFolderName) : e.Name;
            fileSystemUpdater.AddFileSystemEvent(new(e.ChangeType, isRoot, e.FullPath, oldName, newName));
        }
    }

    private void OnError(object sender, ErrorEventArgs e)
    {
        ErrorReportUtil.ReportError(errorState, new FileSystemWatcherErrorReport());
        Reset();
    }

    public void WatchToBuffer(string path, IBufferedFileSystemUpdater fileSystemUpdater)
    {
        this.fileSystemUpdater = fileSystemUpdater;
        var directoryInfo = new DirectoryInfo(path);
        if (directoryInfo.Parent != null)
        {
            rootFolderName = directoryInfo.Name;
            path = directoryInfo.Parent.FullName;
        }

        try
        {
            watcher = new(path)
            {
                InternalBufferSize = MaxBufferSize,
                NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.Size
            };
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }
        catch (ArgumentException)
        { ErrorReportUtil.ReportError(errorState, new DirectoryNotFoundReport(path)); }
        catch (FileNotFoundException)
        { ErrorReportUtil.ReportError(errorState, new DirectoryNotFoundReport(path)); }
    }

    public void Reset()
    {
        watcher?.Dispose();
        watcher = null;
        rootFolderName = null;
        fileSystemUpdater = null;
    }
}
