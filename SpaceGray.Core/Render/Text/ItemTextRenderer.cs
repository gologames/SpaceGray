using SpaceGray.Core.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceGray.Core.Render;

public static class ItemTextRenderer
{
    private const int ItemTextHorizontalPadding = 5;

    public static void DrawItemText(IBufferedGraphics graphics, ResolutionScaler scaler,
        RectangleF rect, string backgroundColorHex, string text)
    {
        var textBounds = graphics.MeasureNodeText(text);
        var rectInt = Rectangle.Round(rect);
        var textPadding = scaler.Scale(ItemTextHorizontalPadding);
        var textWidth = Math.Min(textBounds.Width, rectInt.Width - 2 * textPadding);
        var textRect = new Rectangle(
            rectInt.X + rectInt.Width / 2 - textWidth / 2,
            rectInt.Y + rectInt.Height / 2 - textBounds.Height / 2,
            textWidth, textBounds.Height);

        var textColor = KnownColors.NodeTextColorHex(backgroundColorHex);
        var formatFlags = TextFormatFlags.Left | TextFormatFlags.NoPadding;
        graphics.DrawNodeText(text, textRect, textColor, formatFlags);
    }
}
