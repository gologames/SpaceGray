using SpaceGray.Core.TreeMap;
using System.Drawing;

namespace SpaceGray.Core.Input;

public static class SpaceGrayInput
{
    public static void MouseDown(SpaceGrayApplication application, Point point, bool isClickedRight)
    {
        var mouseDownChanged = false;
        if (application.UIState.Header.Rect.Contains(point))
        { mouseDownChanged = HeaderInput.MouseDown(application.UIState, point); }
        else if (application.UIState.ContentRect.Contains(point))
        { mouseDownChanged = TreeMapMouseDown.MouseDown(application, point, isClickedRight); }
        if (mouseDownChanged) application.UIState.Refresh();
    }

    public static void LeftClick(SpaceGrayApplication application, Point point)
    {
        if (application.UIState.Header.Buttons.Rect.Contains(point) &&
            ButtonsInput.Click(application.UIState.Header.Buttons, point))
        {
            TreeMapLayoutUpdater.UpdateLayout(application);
            application.UIState.Refresh();
        }
    }

    public static void RightClick(SpaceGrayApplication application, Point point)
    {
        if (application.UIState.ContentRect.Contains(point) &&
            TreeMapRightClick.RightClick(application, point))
        {
            TreeMapLayoutUpdater.UpdateLayout(application);
            application.UIState.Refresh();
        }
    }

    public static void DoubleClick(SpaceGrayApplication application, Point point)
    {
        var clickChanged = false;
        if (application.UIState.Header.Rect.Contains(point))
        { clickChanged = HeaderInput.DoubleClick(application, point); }
        else if (application.UIState.ContentRect.Contains(point))
        { clickChanged = TreeMapDoubleClick.DoubleClick(application, point); }
        if (clickChanged)
        {
            TreeMapLayoutUpdater.UpdateLayout(application);
            application.UIState.Refresh();
        }
    }

    public static void Hover(SpaceGrayApplication application, Point point)
    {
        var hoverChanged = false;
        if (application.UIState.Header.Rect.Contains(point))
        {
            if (TreeMapLeave.Leave(application)) hoverChanged = true;
            hoverChanged |= HeaderInput.Hover(application, point);
        }
        else if (application.UIState.ContentRect.Contains(point))
        {
            if (HeaderInput.Leave(application.UIState.Header, false)) hoverChanged = true;
            hoverChanged |= TreeMapHover.Hover(application, point);
        }
        if (hoverChanged) application.UIState.Refresh();
    }

    public static void Leave(SpaceGrayApplication application)
    {
        var leaveChanged = HeaderInput.Leave(application.UIState.Header);
        leaveChanged |= TreeMapLeave.Leave(application);
        if (leaveChanged) application.UIState.Refresh();
    }

    public static void MouseUp(SpaceGrayApplication application)
    {
        var mouseUpChanged = HeaderInput.MouseUp(application.UIState.Header);
        mouseUpChanged |= TreeMapMouseUp.MouseUp(application);
        if (mouseUpChanged) application.UIState.Refresh();
    }
}
