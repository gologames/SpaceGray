using SpaceGray.Core.UI;
using System.Drawing;
using BufferedGraphics = SpaceGray.Core.UI.BufferedGraphics;

namespace SpaceGray.Core.Render;

public static class FileRenderer
{
    public static void DrawFile(IBufferedGraphics graphics, ResolutionScaler scaler, RectangleF rect,
        string borderColorHex, string backgroundColorHex, bool isHatched, string filename, long size)
    {
        if (isHatched) graphics.FillHatchedRectangle(borderColorHex, backgroundColorHex, rect);
        else graphics.FillRectangle(backgroundColorHex, rect);
        graphics.DrawFileRectangle(borderColorHex, rect, isHatched);
        NodeTextRenderer.DrawNodeText(graphics, scaler, rect, backgroundColorHex, filename, size);
    }
}
