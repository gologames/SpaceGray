using SpaceGray.Core.UI;
using System.Drawing;
using System.Windows.Forms;
using BufferedGraphics = SpaceGray.Core.UI.BufferedGraphics;

namespace SpaceGray.Core.Render;

public static class HeaderRenderer
{
    private const int BarTextPadding = 1;

    private static void DrawHeaderButton(IBufferedGraphics graphics, Header header, HeaderButton button)
    {
        var backgroundColorHex = KnownColors.HeaderBackgroundColorHex(
            button.IsActive,
            header.Buttons.HoveredButton == button,
            header.Buttons.ClickedButton == button);
        graphics.FillRectangle(backgroundColorHex, button.Rect);
        graphics.DrawHeaderRectangle(KnownColors.HeaderBorderHex, button.Rect);
        graphics.DrawHeaderText(button.GetText(), Rectangle.Round(button.Rect),
            button.ColorHex, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }

    private static void DrawBar(IBufferedGraphics graphics, ResolutionScaler scaler, HeaderBar bar)
    {
        var backgroundColorHex = KnownColors.HeaderBackgroundColorHex(
            bar.IsActive, bar.IsHovered, bar.IsClicked);
        graphics.FillRectangle(backgroundColorHex, bar.Rect);

        var rectInt = Rectangle.Round(bar.Rect);
        var textPadding = scaler.Scale(BarTextPadding);
        rectInt.X += textPadding;
        rectInt.Width -= 2 * textPadding;
        graphics.DrawNodeText(bar.Text, rectInt, KnownColors.TextDarkHex,
            TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
    }

    public static void DrawHeader(UIState uiState)
    {
        DrawBar(uiState.Graphics, uiState.Scaler, uiState.Header.Bar);
        foreach (var button in uiState.Header.Buttons)
        {
            if (button.IsVisible)
            { DrawHeaderButton(uiState.Graphics, uiState.Header, button); }
        }
        uiState.Graphics.DrawHeaderRectangle(KnownColors.HeaderBorderHex, uiState.Header.Rect);
    }
}
