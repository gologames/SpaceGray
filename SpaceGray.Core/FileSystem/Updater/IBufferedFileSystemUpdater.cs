using System;

#nullable enable

namespace SpaceGray.Core.FileSystem
{
    public interface IBufferedFileSystemUpdater
    {
        bool IsEmpty { get; }
        public delegate void UpdatedEventHandler(object sender, EventArgs e);
        public event UpdatedEventHandler? Updated;

        void AddFileSystemNode(FileSystemNode node);
        FileSystemNode? TryGetNextFileSystemNodeSync();
        void WakeUp();

        void AddFileSystemEvent(FileSystemEvent fileSystemEvent);
        int GetCountOfFileSystemEvents();
        FileSystemEvent GetNextFileSystemEvent();

        void TryUpdate();
    }
}