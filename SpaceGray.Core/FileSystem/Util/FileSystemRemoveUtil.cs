namespace SpaceGray.Core.FileSystem;

public static class FileSystemRemoveUtil
{
    private static bool IsChild(FileSystemNode root, FileSystemNode node)
    {
        while (node != null)
        {
            if (root == node) return true;
            node = node.Parent;
        }
        return false;
    }

    private static FileSystemNode GetNewDisplayRoot(FileSystemNode displayRoot, FileSystemNode removedNode) =>
        IsChild(removedNode, displayRoot) ? removedNode.Parent ?? removedNode : displayRoot;

    public static void TryRemoveDisplayRoot(FileSystemState fileSystemState, FileSystemNode removedNode)
    {
        var newDisplayRoot = GetNewDisplayRoot(fileSystemState.DisplayRoot, removedNode);
        var newPreviousDisplayRoot = GetNewDisplayRoot(fileSystemState.PreviousDisplayRoot, removedNode);
        fileSystemState.SetDisplayRoot(newDisplayRoot, newPreviousDisplayRoot);
        if (newDisplayRoot == newPreviousDisplayRoot) fileSystemState.ClearPreviousDisplayRoot();
    }
}
