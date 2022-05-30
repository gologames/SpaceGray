using SpaceGray.Core.UI;
using System.Drawing;

namespace SpaceGray.Core.Render;

public static class FolderRenderer
{
    private static string GetFolderHeaderText(string foldername, long size) =>
        foldername + " - " + RenderTextUtil.GetPrettySizeName(size);

    public static void DrawFolder(IBufferedGraphics graphics, ResolutionScaler scaler, RectangleF rect, bool isExpanded,
        string borderColorHex, string backgroundColorHex, bool isHatched, string foldername, long size)
    {
        var headerHeight = RenderGroupUtil.GetGroupHeaderHeight(scaler, graphics.OneSymbolHeight);
        var fillHeight = isExpanded ? headerHeight : rect.Height;
        if (isHatched)
        {
            graphics.FillHatchedRectangle(borderColorHex,
                backgroundColorHex, rect.X, rect.Y, rect.Width, fillHeight);
        }
        else graphics.FillRectangle(backgroundColorHex, rect.X, rect.Y, rect.Width, fillHeight);
        graphics.DrawFolderRectangle(borderColorHex, rect, isHatched);

        if (isExpanded)
        {
            var text = GetFolderHeaderText(foldername, size);
            GroupRenderer.DrawGroupHeader(graphics, scaler, rect, Rectangle.Round(rect),
                headerHeight, borderColorHex, backgroundColorHex, text);
        }
        else NodeTextRenderer.DrawNodeText(graphics, scaler, rect, backgroundColorHex, foldername, size);
    }
}
