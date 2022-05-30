using SpaceGray.Core.FileSystem;
using SpaceGray.Core.Layout;
using SpaceGray.Core.TreeMap;
using SpaceGray.Core.UI;
using System;
using System.Drawing;

namespace SpaceGray.Core.Input;

public static class TreeMapMouseDown
{
    private static void RightClickFileSystem(TabsController tabs, FileSystemLayout fileSystemLayout, FileSystemNode node)
    {
        if (tabs.IsMark)
        { fileSystemLayout.SetSelectedMarkedNodes(TreeMapNodeUtil.GetSelectedMarkedNodes(node)); }
    }

    private static bool MouseDown<T>(UIState uiState,
        TreeMapState<T> treeMapState, LayoutState<T> layoutState, Point point,
        bool isClickedRight, Action<T> onRightClick = null) where T : ITreeMapNode
    {
        lock (treeMapState.Locker)
        {
            if (layoutState.IsReady)
            {
                var node = TreeMapNodeUtil.FindNode(uiState, layoutState.Root, point);
                layoutState.SetClickedNode(node.Node, isClickedRight);
                if (isClickedRight) onRightClick?.Invoke(node.Node);
                return true;
            }
        }
        return false;
    }
    public static bool MouseDown(SpaceGrayApplication application, Point point, bool isClickedRight)
    {
        var tabs = application.UIState.Tabs;
        if (tabs.IsFileSystem)
        {
            return MouseDown(application.UIState, application.FileSystemState,
                application.FileSystemLayout, point, isClickedRight,
                node => RightClickFileSystem(application.UIState.Tabs, application.FileSystemLayout, node));
        }
        else if (tabs.IsError)
        {
            return MouseDown(application.UIState, application.ErrorState,
                application.ErrorLayout, point, isClickedRight);
        }
        return false;
    }
}
