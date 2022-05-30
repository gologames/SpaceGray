using System.Drawing;
using System.Windows.Forms;
using SpaceGray.Core.Localization;
using SpaceGray.Core.UI;

namespace SpaceGray.Core.Render;

public static class InfoRenderer
{
    public static void DrawInfo(UIState uiState)
    {
        uiState.Graphics.FillRectangle(KnownColors.InfoBackgroundHex, uiState.ContentRect);
        uiState.Graphics.DrawNodeText(Resources.TabInfo, Rectangle.Round(uiState.ContentRect),
            KnownColors.TextDarkHex, TextFormatFlags.HorizontalCenter |
            TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
    }
}
