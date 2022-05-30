using SpaceGray.Core.FileSystem;
using SpaceGray.Core.Layout;
using SpaceGray.Core.Render;
using SpaceGray.Core.TreeMap;
using SpaceGray.Core.UI;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpaceGray.Core.Input;

public class TreeMapNodeUtil
{
    private static bool IsClickOnGroupHeader(UIState uiState, RectangleF rect, Point point)
    {
        var headerRect = new RectangleF(rect.X, rect.Y, rect.Width,
            RenderGroupUtil.GetGroupHeaderHeight(uiState.Scaler, uiState.Graphics.OneSymbolHeight));
        return headerRect.Contains(point);
    }

    public static LayoutNode<T> FindNode<T>(UIState uiState, LayoutNode<T> root, Point point) where T : ITreeMapNode
    {
        if (IsClickOnGroupHeader(uiState, root.Rect, point)) return root;
        while (root.Children.Any())
        {
            var child = root.Children.First(child => child.Rect.Contains(point));
            if (child.Node.HasContent && IsClickOnGroupHeader(uiState, child.Rect, point))
            { return child; }
            root = child;
        }
        return root;
    }

    public static ISet<FileSystemNode> GetSelectedMarkedNodes(FileSystemNode node)
    {
        var selectedMarkedNodes = new HashSet<FileSystemNode>();
        while (node != null)
        {
            if (node.IsMarked) selectedMarkedNodes.Add(node);
            node = node.Parent;
        }
        return selectedMarkedNodes;
    }
}
