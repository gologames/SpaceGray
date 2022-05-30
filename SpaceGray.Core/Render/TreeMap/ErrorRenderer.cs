using SpaceGray.Core.Error;
using SpaceGray.Core.Layout;
using SpaceGray.Core.UI;
using System.Collections.Generic;

namespace SpaceGray.Core.Render;

public static class ErrorRenderer
{
    private static void DrawErrorItem(UIState uiState, LayoutNode<ErrorNode> node,
        long depth, bool isColored, bool isHovered, bool isClicked)
    {
        var itemBorderColorHex = KnownColors.ItemBorderColorHex(depth, isColored, node.BaseColoredHex);
        var itemBackgroundColorHex = KnownColors.ItemBackgroundColorHex(depth,
            isHovered, isClicked, isColored, node.BaseColoredHex);
        ItemRenderer.DrawItem(uiState.Graphics, uiState.Scaler, node.Rect, itemBorderColorHex,
            itemBackgroundColorHex, node.Node.Text);
    }
    private static void DrawErrorGroup(UIState uiState, LayoutNode<ErrorNode> node,
        long depth, bool isColored, bool isHovered, bool isClicked)
    {
        var groupBorderColorHex = KnownColors.GroupBorderColorHex(depth, isColored);
        var groupBackgroundColorHex = KnownColors.GroupBackgroundColorHex(
            depth, isHovered, isClicked, isColored);
        GroupRenderer.DrawGroup(uiState.Graphics, uiState.Scaler, node.Rect, node.IsExpanded,
            groupBorderColorHex, groupBackgroundColorHex, node.Node.Text);
    }

    private static void DrawTreeMap(UIState uiState, ErrorLayout errorLayout)
    {
        var queue = new Queue<LayoutNode<ErrorNode>>();
        queue.Enqueue(errorLayout.Root);
        while (queue.Count != 0)
        {
            var node = queue.Dequeue();
            foreach (var child in node.Children) queue.Enqueue(child);

            var isColored = uiState.IsColored;
            var depth = node.Node.Depth - errorLayout.Root.Node.Depth;
            var isHovered = errorLayout.HoveredNode == node.Node;
            var isClicked = errorLayout.ClickedNode == node.Node;

            if (node.Node.HasContent)
            { DrawErrorGroup(uiState, node, depth, isColored, isHovered, isClicked); }
            else
            {
                if (isColored)
                { node.BaseColoredHex = ErrorLayout.GetColorHexBySeverity(node.Node.Severity); }
                DrawErrorItem(uiState, node, depth - 1, isColored, isHovered, isClicked);
            }
        }
    }

    public static void DrawError(SpaceGrayApplication application)
    {
        lock (application.ErrorState.Locker)
        {
            if (application.ErrorLayout.IsReady)
            { DrawTreeMap(application.UIState, application.ErrorLayout); }
        }
    }
}
