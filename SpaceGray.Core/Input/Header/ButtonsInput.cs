using SpaceGray.Core.UI;
using System.Drawing;
using System.Linq;

namespace SpaceGray.Core.Input;

public static class ButtonsInput
{
    private static HeaderButton FindButton(HeaderButtons buttons, Point point) =>
        buttons.FirstOrDefault(button => button.Rect.Contains(point));

    public static bool MouseDown(HeaderButtons buttons, Point point)
    {
        var button = FindButton(buttons, point);
        if (button.IsActive)
        {
            buttons.SetClickedButton(button);
            return true;
        }
        return false;
    }

    public static bool Click(HeaderButtons buttons, Point point)
    {
        var button = FindButton(buttons, point);
        if (button.IsActive)
        {
            button.Execute();
            return true;
        }
        return false;
    }

    public static bool Hover(HeaderButtons buttons, Point point)
    {
        var button = FindButton(buttons, point);
        if (buttons.HoveredButton != button)
        {
            if (button.IsActive) buttons.SetHoveredButton(button);
            else buttons.ClearHoveredButton();
            return true;
        }
        return false;
    }

    public static bool Leave(HeaderButtons buttons)
    {
        if (buttons.IsAnyButtonHovered)
        {
            buttons.ClearHoveredButton();
            return true;
        }
        return false;
    }
    public static void MouseUp(HeaderButtons buttons) => buttons.ClearClickedButton();
}
