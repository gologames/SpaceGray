using SpaceGray.Core.FileSystem;
using SpaceGray.Core.Layout;
using SpaceGray.Core.UI;
using System.Collections.Generic;

namespace SpaceGray.Core.Render;

public static class FileSystemRenderer
{
    private static void DrawTreeMapFile(UIState uiState, LayoutNode<FileSystemNode> node,
        long depth, bool isColored, bool isHatched, bool isHovered, bool isClicked)
    {
        var fileBorderColorHex = KnownColors.ItemBorderColorHex(depth, isColored, node.BaseColoredHex);
        var fileBackgroundColorHex = KnownColors.ItemBackgroundColorHex(
            depth, isHovered, isClicked, isColored, node.BaseColoredHex);
        FileRenderer.DrawFile(uiState.Graphics, uiState.Scaler, node.Rect, fileBorderColorHex,
            fileBackgroundColorHex, isHatched, node.Node.Text, node.Node.Size);
    }
    private static void DrawTreeMapFolder(UIState uiState, FileSystemLayout fileSystemLayout,
        LayoutNode<FileSystemNode> node, long depth, bool isColored, bool isHatched, bool isHovered, bool isClicked)
    {
        var folderBorderColorHex = KnownColors.GroupBorderColorHex(depth, isColored);
        var folderBackgroundColorHex = KnownColors.GroupBackgroundColorHex(
            depth, isHovered, isClicked, isColored);
        var name = node == fileSystemLayout.Root ? node.Node.GetPath() : node.Node.Text;
        FolderRenderer.DrawFolder(uiState.Graphics, uiState.Scaler, node.Rect, node.IsExpanded,
            folderBorderColorHex, folderBackgroundColorHex, isHatched, name, node.Node.Size);
    }

    private static void DrawTreeMap(UIState uiState, FileSystemLayout fileSystemLayout)
    {
        var queue = new Queue<LayoutNode<FileSystemNode>>();
        queue.Enqueue(fileSystemLayout.Root);
        while (queue.Count != 0)
        {
            var node = queue.Dequeue();
            foreach (var child in node.Children) queue.Enqueue(child);

            var isHatched = uiState.Tabs.IsMark && node.BaseColoredHex == null;
            var isColored = uiState.IsColored && !isHatched;
            var depth = node.Node.Depth - fileSystemLayout.Root.Node.Depth;
            var isHovered = fileSystemLayout.HoveredNode == node.Node;
            var isClicked = fileSystemLayout.ClickedNode == node.Node;
            if (uiState.Tabs.IsMark && fileSystemLayout.ClickedNode != null &&
                fileSystemLayout.SelectedBaseColoredHexes.Contains(node.BaseColoredHex))
            { isHovered = isClicked = true; }

            if (node.Node.HasContent)
            {
                DrawTreeMapFolder(uiState, fileSystemLayout, node,
                    depth, isColored, isHatched, isHovered, isClicked);
            }
            else
            {
                DrawTreeMapFile(uiState, node, depth - 1,
                    isColored, isHatched, isHovered, isClicked);
            }
        }
    }

    public static void DrawFileSystem(SpaceGrayApplication application)
    {
        lock (application.FileSystemState.Locker)
        {
            if (application.FileSystemLayout.IsReady)
            {
                FileSystemColorHexUtil.PropogateBaseColoredHex(application);
                DrawTreeMap(application.UIState, application.FileSystemLayout);
            }
        }
    }
}
