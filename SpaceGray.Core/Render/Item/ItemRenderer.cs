using SpaceGray.Core.UI;
using System.Drawing;
using BufferedGraphics = SpaceGray.Core.UI.BufferedGraphics;

namespace SpaceGray.Core.Render;

public static class ItemRenderer
{
    public static void DrawItem(IBufferedGraphics graphics, ResolutionScaler scaler, RectangleF rect,
        string borderColorHex, string backgroundColorHex, string text)
    {
        graphics.FillRectangle(backgroundColorHex, rect);
        graphics.DrawFileRectangle(borderColorHex, rect);
        ItemTextRenderer.DrawItemText(graphics, scaler, rect, backgroundColorHex, text);
    }
}
