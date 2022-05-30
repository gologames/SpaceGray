using System.Collections.Generic;

namespace SpaceGray.Core.FileSystem;

public static class FileSystemSizeUtil
{
    public static void PropagateSize(FileSystemNode node, long length)
    {
        while (node != null)
        {
            node.AddSize(length);
            node = node.Parent;
        }
    }

    public static void CalculateSize(FileSystemNode root)
    {
        var nodes = new List<FileSystemNode>();
        var queue = new Queue<FileSystemNode>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            nodes.AddRange(node.GetChildren());
            foreach (var child in node.GetChildren())
            { queue.Enqueue(child); }
        }
        for (var i = nodes.Count - 1; i >= 0; i--)
        {
            nodes[i].Parent.AddSize(nodes[i].Size);
            nodes[i].SetAsCalculated();
        }
        root.SetAsCalculated();
        PropagateSize(root.Parent, root.Size);
    }
}
