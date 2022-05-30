using SpaceGray.Core.Layout;
using SpaceGray.Core.TreeMap;

namespace SpaceGray.Core.Input;

public static class TreeMapMouseUp
{
    private static bool MouseUp<T>(LayoutState<T> layoutState) where T : ITreeMapNode
    {
        if (layoutState.IsAnyNodeClicked)
        {
            layoutState.ClearClickedNode();
            return true;
        }
        return false;
    }

    public static bool MouseUp(SpaceGrayApplication application)
    {
        var tabs = application.UIState.Tabs;
        if (tabs.IsFileSystem) return MouseUp(application.FileSystemLayout);
        else if (tabs.IsError) return MouseUp(application.ErrorLayout);
        return false;
    }
}
