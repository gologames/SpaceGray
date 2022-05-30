using SpaceGray.Core.Error;
using SpaceGray.Core.FileSystem;
using SpaceGray.Core.Layout;
using SpaceGray.Core.TreeMap;
using SpaceGray.Core.UI;
using System;
using System.Drawing;

namespace SpaceGray.Core.Input;

public static class TreeMapRightClick
{
    private static void DisplayParent<T>(TreeMapState<T> treeMapState,
        T node, T parent) where T : ITreeMapNode
    {
        if (treeMapState.DisplayRoot.Equals(node) && parent != null)
        { treeMapState.SetDisplayRoot(parent); }
    }

    private static void RightClickMarkMode(FileSystemNode node)
    {
        var selectedMarkedNodes = TreeMapNodeUtil.GetSelectedMarkedNodes(node);
        foreach (var markedNode in selectedMarkedNodes) markedNode.Unmark();
        if (selectedMarkedNodes.Count == 0) node.Mark();
    }
    private static void RightClickFileSystem(UIState uiState,
        FileSystemState fileSystemState, FileSystemNode node)
    {
        if (uiState.Tabs.IsMark) RightClickMarkMode(node);
        else
        {
            if (node.IsMarked) node.Unmark();
            else
            {
                node.Mark();
                DisplayParent(fileSystemState, node, node.Parent);
            }
        }
    }

    private static void RightClickError(ErrorState errorState, ErrorNode node)
    {
        DisplayParent(errorState, node, node.Parent);
        errorState.RemoveError(node);
    }

    private static bool RightClick<T>(UIState uiState,
        TreeMapState<T> treeMapState, LayoutState<T> layoutState, Point point,
        Action<T> onRightClick) where T : ITreeMapNode
    {
        lock (treeMapState.Locker)
        {
            var node = TreeMapNodeUtil.FindNode(uiState, layoutState.Root, point);
            if (layoutState.ClickedNode != null && layoutState.ClickedNode.Equals(node.Node))
            {
                onRightClick(node.Node);
                return true;
            }
            return false;
        }
    }
    public static bool RightClick(SpaceGrayApplication application, Point point)
    {
        var uiState = application.UIState;
        if (uiState.Tabs.IsFileSystem)
        {
            return RightClick(uiState,
                application.FileSystemState, application.FileSystemLayout, point,
                node => RightClickFileSystem(uiState, application.FileSystemState, node));
        }
        else if (uiState.Tabs.IsError)
        {
            return RightClick(uiState,
                application.ErrorState, application.ErrorLayout, point,
                node => RightClickError(application.ErrorState, (node)));
        }
        return false;
    }
}
