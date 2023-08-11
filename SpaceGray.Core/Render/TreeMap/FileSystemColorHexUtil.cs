using SpaceGray.Core.FileSystem;
using SpaceGray.Core.Layout;
using System.Collections.Generic;

namespace SpaceGray.Core.Render;

public static class FileSystemColorHexUtil
{
    private static FileSystemNode GetMarkedParent(FileSystemNode node)
    {
        var parent = node.Parent;
        while (parent != null && !parent.IsMarked)
        { parent = parent.Parent; }
        return parent;
    }

    private static string GetBaseColoredHex(SpaceGrayApplication application, LayoutNode<FileSystemNode> node)
    {
        string baseColoredHex = null;
        if (application.UIState.Tabs.IsMark)
        {
            if (node.Node.IsMarked)
            { baseColoredHex = application.FileSystemLayout.GetColorHexByNode(node.Node); }
            else if (node.Parent?.BaseColoredHex != null)
            { baseColoredHex = node.Parent.BaseColoredHex; }
            else if (application.FileSystemState.DisplayRoot == node.Node)
            {
                var parent = GetMarkedParent(node.Node);
                if (parent != null)
                { baseColoredHex = application.FileSystemLayout.GetColorHexByNode(parent); }
            }
        }
        else if (application.UIState.IsColored && !node.Node.HasContent)
        { baseColoredHex = application.FileSystemLayout.GetColorHexByFilename(node.Node.Text); }
        return baseColoredHex;
    }

    public static void PropagateBaseColoredHex(SpaceGrayApplication application)
    {
        var queue = new Queue<LayoutNode<FileSystemNode>>();
        queue.Enqueue(application.FileSystemLayout.Root);
        application.FileSystemLayout.ResetSelectedBaseColoredHexes();
        while (queue.Count != 0)
        {
            var node = queue.Dequeue();
            node.BaseColoredHex = GetBaseColoredHex(application, node);
            if (node.BaseColoredHex != null && application.FileSystemLayout.SelectedMarkedNodes.Contains(node.Node))
            { application.FileSystemLayout.AddSelectedBaseColoredHex(node.BaseColoredHex); }
            foreach (var child in node.Children) queue.Enqueue(child);
        }
    }
}
