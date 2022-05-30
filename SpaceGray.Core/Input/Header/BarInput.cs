using SpaceGray.Core.UI;

namespace SpaceGray.Core.Input;

public static class BarInput
{
    public static bool MouseDown(UIState uiState)
    {
        if (uiState.Tabs.IsFileSystem)
        {
            uiState.Header.Bar.IsClicked = true;
            return true;
        }
        return false;
    }

    public static bool DobuleClick(SpaceGrayApplication application)
    {
        if (application.UIState.Tabs.IsFileSystem)
        {
            var fileSystemState = application.FileSystemState;
            lock (fileSystemState.Locker)
            {
                var rootChanged = fileSystemState.DisplayPreviousRoot();
                if (rootChanged)
                {
                    application.UIState.Header.Bar.Text =
                        fileSystemState.PreviousDisplayRoot.GetPath();
                }
                return rootChanged;
            }
        }
        return false;
    }

    public static bool Hover(SpaceGrayApplication application)
    {
        var bar = application.UIState.Header.Bar;
        if (application.UIState.Tabs.IsFileSystem && !bar.IsHovered)
        {
            if (application.FileSystemState.PreviousDisplayRoot != null)
            { bar.Text = application.FileSystemState.PreviousDisplayRoot.GetPath(); }
            else bar.ClearText();
            bar.IsHovered = true;
            return true;
        }
        if (application.UIState.Tabs.IsError) return Leave(bar);
        return false;
    }

    public static bool Leave(HeaderBar bar, bool forceClear = true)
    {
        if (bar.IsHovered || forceClear && !string.IsNullOrEmpty(bar.Text))
        {
            bar.ClearText();
            bar.IsHovered = false;
            return true;
        }
        return false;
    }
    public static void MouseUp(HeaderBar bar) => bar.IsClicked = false;
}
