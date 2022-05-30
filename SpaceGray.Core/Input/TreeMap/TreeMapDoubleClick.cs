using SpaceGray.Core.Error;
using SpaceGray.Core.Layout;
using SpaceGray.Core.TreeMap;
using SpaceGray.Core.UI;
using System;
using System.Drawing;

namespace SpaceGray.Core.Input;

public static class TreeMapDoubleClick
{
    private static void DoubleClickErrorItem(HeaderBar bar, ErrorLayout errorLayout, ErrorNode node)
    {
        errorLayout.SelectedCopiedNode = node;
        CopyToClipboardUtil.CopyToClipboard(node.Text);
        TreeMapHover.HoverErrorItem(bar, errorLayout, node);
    }

    private static bool DoubleClick<T>(UIState uiState,
        TreeMapState<T> treeMapState, LayoutState<T> layoutState, Point point,
        Func<T, T> parentNodeGetter, Action<T> onItemClick) where T : ITreeMapNode
    {
        lock (treeMapState.Locker)
        {
            if (!layoutState.IsReady) return false;
            var node = TreeMapNodeUtil.FindNode(uiState, layoutState.Root, point);
            if (layoutState.ClickedNode == null || !layoutState.ClickedNode.Equals(node.Node)) return false;
            if (node.Node.HasContent)
            {
                var displayRoot = treeMapState.DisplayRoot;
                if (!displayRoot.Equals(node.Node))
                {
                    treeMapState.SetDisplayRoot(node.Node);
                    return true;
                }
                else if (parentNodeGetter(node.Node) != null)
                {
                    treeMapState.SetDisplayRoot(parentNodeGetter(node.Node));
                    return true;
                }
            }
            else
            {
                onItemClick(node.Node);
                return true;
            }
        }
        return false;
    }

    public static bool DoubleClick(SpaceGrayApplication application, Point point)
    {
        var tabs = application.UIState.Tabs;
        if (tabs.IsFileSystem)
        {
            return DoubleClick(application.UIState,
                application.FileSystemState, application.FileSystemLayout, point,
                node => node.Parent,
                node => OpenFileUtil.OpenFile(node.GetPath()));
        }
        else if (tabs.IsError)
        {
            return DoubleClick(application.UIState,
                application.ErrorState, application.ErrorLayout, point,
                node => node.Parent,
                node => DoubleClickErrorItem(application.UIState.Header.Bar, application.ErrorLayout, node));
        }
        return false;
    }
}
