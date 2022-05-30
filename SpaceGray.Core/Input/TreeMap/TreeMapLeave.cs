using SpaceGray.Core.Layout;
using SpaceGray.Core.TreeMap;

namespace SpaceGray.Core.Input;

public static class TreeMapLeave
{
    private static bool Leave<T>(LayoutState<T> layoutState) where T : ITreeMapNode
    {
        if (layoutState.IsAnyNodeHovered)
        {
            layoutState.ClearHoveredNode();
            return true;
        }
        return false;
    }

    public static bool Leave(SpaceGrayApplication application)
    {
        var tabs = application.UIState.Tabs;
        if (tabs.IsFileSystem) return Leave(application.FileSystemLayout);
        else if (tabs.IsError) Leave(application.ErrorLayout);
        return false;
    }
}
