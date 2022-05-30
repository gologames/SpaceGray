using SpaceGray.Core.Error;
using SpaceGray.Core.Layout;
using SpaceGray.Core.Localization;
using SpaceGray.Core.TreeMap;
using SpaceGray.Core.UI;
using System;
using System.Drawing;

namespace SpaceGray.Core.Input;

public static class TreeMapHover
{
    public static void HoverErrorItem(HeaderBar bar, ErrorLayout errorLayout, ErrorNode node)
    { bar.Text = ErrorLocalization.GetErrorItemCopiedText(node.Text, errorLayout.SelectedCopiedNode == node); }

    private static bool Hover<T>(UIState uiState, TreeMapState<T> treeMapState,
        LayoutState<T> layoutState, Point point, Action<T> onSetBarText) where T : ITreeMapNode
    {
        lock (treeMapState.Locker)
        {
            if (layoutState.IsReady)
            {
                var node = TreeMapNodeUtil.FindNode(uiState, layoutState.Root, point);
                if (layoutState.HoveredNode == null || !layoutState.HoveredNode.Equals(node.Node))
                {
                    layoutState.SetHoveredNode(node.Node);
                    onSetBarText(node.Node);
                    return true;
                }
            }
        }
        return false;
    }

    public static bool Hover(SpaceGrayApplication application, Point point)
    {
        var tabs = application.UIState.Tabs;
        if (tabs.IsFileSystem)
        {
            return Hover(application.UIState,
                application.FileSystemState, application.FileSystemLayout, point,
                node => application.UIState.Header.Bar.Text = node.Text);
        }
        else if (tabs.IsError)
        {
            return Hover(application.UIState,
                application.ErrorState, application.ErrorLayout, point,
                node => HoverErrorItem(application.UIState.Header.Bar, application.ErrorLayout, node));
        }
        return false;
    }
}
