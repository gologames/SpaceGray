using SpaceGray.Core.Layout;
using SpaceGray.Core.Render;
using SpaceGray.Core.UI;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceGray.Core.TreeMap;

public class TreeMapLayoutUpdater
{
    private const int SquareMinSize = 42;

    public static RectangleF? GetContentRect<T>(UIState uiState, DepthController depthController,
        T root, LayoutNode<T> node) where T : ITreeMapNode
    {
        var isDepthLess = depthController.IsNodeDepthIsLessThanMaxDepth(root, node.Node);
        var isContent = RenderGroupUtil.IsGroupExpandable(uiState.Scaler, node.Rect, uiState.Graphics.OneSymbolHeight);
        return isDepthLess && isContent
            ? node.GetContentRect(RenderGroupUtil.GetGroupHeaderHeight(uiState.Scaler, uiState.Graphics.OneSymbolHeight))
            : null;
    }

    public static LayoutNode<T> BuildTreeMap<T>(UIState uiState,
        T displayRoot, Func<LayoutNode<T>, RectangleF?> contentRectGetter,
        Func<T, (IEnumerable<T>, long)> sortedChildrenGetter) where T : ITreeMapNode
    {
        var layout = new LayoutSquarified<T>(uiState.Scaler.Scale(SquareMinSize));
        var rect = uiState.ContentRect;
        var layoutBuilder = new LayoutBuilder<T>(layout, displayRoot, rect);
        return layoutBuilder.BuildTreeMap(contentRectGetter, sortedChildrenGetter);
    }

    private static void UpdateLayout<T>(UIState uiState, TreeMapState<T> treeMapState,
        LayoutState<T> layoutState, Func<T, (IEnumerable<T>, long)> sortedChildrenGetter) where T : ITreeMapNode
    {
        if (treeMapState.IsReady)
        {
            var displayRoot = treeMapState.DisplayRoot;
            var depthController = layoutState.Depth;
            var root = BuildTreeMap(uiState, displayRoot,
                node => GetContentRect(uiState, depthController, displayRoot, node),
                sortedChildrenGetter);
            layoutState.SetRoot(root);
        }
    }
    public static void UpdateLayout(SpaceGrayApplication application)
    {
        var uiState = application.UIState;
        if (uiState.Tabs.IsFileSystem)
        {
            lock (application.FileSystemState.Locker)
            {
                UpdateLayout(uiState, application.FileSystemState, application.FileSystemLayout,
                    node => node.GetSortedChildren(uiState.Tabs.IsMark));
            }
        }
        else if (uiState.Tabs.IsError)
        {
            lock (application.ErrorState.Locker)
            {
                UpdateLayout(uiState, application.ErrorState, application.ErrorLayout,
                    node => node.GetSortedChildren());
            }
        }
    }
}
