using SpaceGray.Core.UI;
using System.Drawing;
using System.Windows.Forms;
using BufferedGraphics = SpaceGray.Core.UI.BufferedGraphics;

namespace SpaceGray.Core.Render;

public static class GroupRenderer
{
    public static void DrawGroupHeader(IBufferedGraphics graphics, ResolutionScaler scaler, RectangleF rect, Rectangle rectInt,
        float headerHeight, string borderColorHex, string backgroundColorHex, string text)
    {
        graphics.DrawLine(borderColorHex, rect.X, rect.Y + headerHeight,
            rect.X + rect.Width, rect.Y + headerHeight);

        var textPadding = RenderGroupUtil.GetGroupHeaderTextPadding(scaler);
        var textWidth = rectInt.Width - 2 * textPadding;
        var textHeight = rectInt.Height - 2 * textPadding;
        var textRect = new Rectangle(rectInt.X + textPadding,
            rectInt.Y + textPadding, textWidth, textHeight);
        var textColorHex = KnownColors.NodeTextColorHex(backgroundColorHex);
        graphics.DrawNodeText(text, textRect, textColorHex, TextFormatFlags.Left);
    }

    private static void DrawExpandedGroup(IBufferedGraphics graphics, ResolutionScaler scaler, RectangleF rect,
        string borderColorHex, string backgroundColorHex, string text)
    {
        var headerHeight = RenderGroupUtil.GetGroupHeaderHeight(scaler, graphics.OneSymbolHeight);
        graphics.FillRectangle(backgroundColorHex, rect.X, rect.Y, rect.Width, headerHeight);
        graphics.DrawFolderRectangle(borderColorHex, rect);

        DrawGroupHeader(graphics, scaler, rect, Rectangle.Round(rect),
                headerHeight, borderColorHex, backgroundColorHex, text);
    }

    private static void DrawCollapsedGroup(IBufferedGraphics graphics, ResolutionScaler scaler, RectangleF rect,
        string borderColorHex, string backgroundColorHex, string text)
    {
        graphics.FillRectangle(backgroundColorHex, rect.X, rect.Y, rect.Width, rect.Height);
        graphics.DrawFolderRectangle(borderColorHex, rect);
        ItemTextRenderer.DrawItemText(graphics, scaler, rect, backgroundColorHex, text);
    }

    public static void DrawGroup(IBufferedGraphics graphics, ResolutionScaler scaler, RectangleF rect,
        bool isExpanded, string borderColorHex, string backgroundColorHex, string text)
    {
        if (isExpanded) DrawExpandedGroup(graphics, scaler, rect, borderColorHex, backgroundColorHex, text);
        else DrawCollapsedGroup(graphics, scaler, rect, borderColorHex, backgroundColorHex, text);
    }
}
