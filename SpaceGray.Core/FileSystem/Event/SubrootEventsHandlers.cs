using System.IO;

namespace SpaceGray.Core.FileSystem;

public static class SubrootEventsHandlers
{
    public static void ProcessCreatedEvent(IBufferedFileSystemUpdater fileSystemUpdater, string path, FileSystemNode parentNode)
    {
        var attr = File.GetAttributes(path);
        FileSystemNode node;
        if (attr.HasFlag(FileAttributes.Directory))
        {
            var directoryInfo = new DirectoryInfo(path);
            node = new FolderNode(directoryInfo.Name);
            fileSystemUpdater.AddFileSystemNode(node);
        }
        else
        {
            var fileInfo = new FileInfo(path);
            node = new FileNode(fileInfo.Name, fileInfo.Length);
            FileSystemSizeUtil.PropagateSize(parentNode, node.Size);
            node.SetAsCalculated();
        }
        parentNode.AddChild(node);
    }

    public static void ProcessDeletedEvent(FileSystemState fileSystemState, FileSystemNode parentNode, string nodeName)
    {
        var node = parentNode.GetChildByName(nodeName);
        FileSystemRemoveUtil.TryRemoveDisplayRoot(fileSystemState, node);
        parentNode.RemoveChild(node);
        FileSystemSizeUtil.PropagateSize(parentNode, -node.Size);
    }

    public static void ProcessChangedEvent(string path, FileSystemNode parentNode, string nodeName)
    {
        var attr = File.GetAttributes(path);
        if (!attr.HasFlag(FileAttributes.Directory))
        {
            var node = parentNode.GetChildByName(nodeName);
            var fileInfo = new FileInfo(path);
            FileSystemSizeUtil.PropagateSize(parentNode, fileInfo.Length - node.Size);
            node.SetSize(fileInfo.Length);
        }
    }

    public static void ProcessRenamedEvent(FileSystemNode parentNode, string nodeName, string newNodePath)
    {
        var node = parentNode.GetChildByName(nodeName);
        parentNode.RemoveChild(node);
        node.Rename(FileSystemPathUtil.SplitPathParts(newNodePath)[^1]);
        parentNode.AddChild(node);
    }
}
