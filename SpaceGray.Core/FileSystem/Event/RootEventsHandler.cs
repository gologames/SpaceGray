using System.IO;

namespace SpaceGray.Core.FileSystem;

public static class RootEventsHandler
{
    private static void ProcessCreatedEvent(IBufferedFileSystemUpdater fileSystemUpdater, FileSystemNode root) =>
        fileSystemUpdater.AddFileSystemNode(root);

    private static void ProcessDeletedEvent(FileSystemState fileSystemState)
    {
        fileSystemState.SetDisplayRoot(fileSystemState.Root, null);
        fileSystemState.Root.ClearChildren();
    }

    private static void ProcessRenamedEvent(FileSystemNode root, string newRootName) => root.Rename(newRootName);

    public static void ProcessRootEvent(IBufferedFileSystemUpdater fileSystemUpdater, FileSystemState fileSystemState, FileSystemEvent fileSystemEvent)
    {
        switch (fileSystemEvent.ChangeType)
        {
            case WatcherChangeTypes.Created:
                ProcessCreatedEvent(fileSystemUpdater, fileSystemState.Root);
                break;
            case WatcherChangeTypes.Deleted:
                ProcessDeletedEvent(fileSystemState);
                break;
            case WatcherChangeTypes.Renamed:
                ProcessRenamedEvent(fileSystemState.Root, fileSystemEvent.Path);
                break;
        }
    }
}
