using SpaceGray.Core.UI;
using System.Drawing;

namespace SpaceGray.Core.Input;

public static class HeaderInput
{
    public static bool MouseDown(UIState uiState, Point point)
    {
        var header = uiState.Header;
        if (header.Buttons.Rect.Contains(point))
        { return ButtonsInput.MouseDown(header.Buttons, point); }
        else if (header.Bar.Rect.Contains(point))
        { return BarInput.MouseDown(uiState); }
        return false;
    }

    public static bool DoubleClick(SpaceGrayApplication application, Point point)
    {
        var header = application.UIState.Header;
        if (header.Buttons.Rect.Contains(point))
        { return ButtonsInput.Click(header.Buttons, point);  }
        else if (header.Bar.Rect.Contains(point))
        { return BarInput.DobuleClick(application); }
        return false;
    }

    public static bool Hover(SpaceGrayApplication application, Point point)
    {
        var hoverChanged = false;
        var header = application.UIState.Header;
        if (header.Buttons.Rect.Contains(point))
        {
            if (BarInput.Leave(header.Bar)) hoverChanged = true;
            return ButtonsInput.Hover(header.Buttons, point) || hoverChanged;
        }
        else if (header.Bar.Rect.Contains(point))
        {
            if (ButtonsInput.Leave(header.Buttons)) hoverChanged = true;
            return BarInput.Hover(application) || hoverChanged;
        }
        return hoverChanged;
    }

    public static bool Leave(Header header, bool forceClear = true)
    {
        var leaveChanged = ButtonsInput.Leave(header.Buttons);
        leaveChanged |= BarInput.Leave(header.Bar, forceClear);
        return leaveChanged;
    }

    public static bool MouseUp(Header header)
    {
        if (header.Buttons.IsAnyButtonClicked)
        {
            ButtonsInput.MouseUp(header.Buttons);
            return true;
        }
        else if (header.Bar.IsClicked)
        {
            BarInput.MouseUp(header.Bar);
            return true;
        }
        return false;
    }
}
