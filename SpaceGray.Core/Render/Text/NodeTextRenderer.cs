using SpaceGray.Core.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceGray.Core.Render;

public static class NodeTextRenderer
{
    private const int NodeTextHorizontalPadding = 5;
    private const int NodeTextVerticalPadding = 2;

    public static void DrawNodeText(IBufferedGraphics graphics, ResolutionScaler scaler,
        RectangleF rect, string backgroundColorHex, string nodeName, long size)
    {
        var nameBounds = graphics.MeasureNodeText(nodeName);
        var sizeName = RenderTextUtil.GetPrettySizeName(size);
        var sizeBounds = graphics.MeasureNodeText(sizeName);
        var rectInt = Rectangle.Round(rect);

        var horizontalPadding = scaler.Scale(NodeTextHorizontalPadding);
        var nameWidth = Math.Min(nameBounds.Width, rectInt.Width - 2 * horizontalPadding);
        var nameRect = new Rectangle(
            rectInt.X + rectInt.Width / 2 - nameWidth / 2,
            rectInt.Y + rectInt.Height / 2 - (nameBounds.Height + sizeBounds.Height) / 2,
            nameWidth, nameBounds.Height);

        var sizeWidth = Math.Min(sizeBounds.Width, rectInt.Width - 2 * horizontalPadding);
        var sizeRect = new Rectangle(
            rectInt.X + rectInt.Width / 2 - sizeWidth / 2,
            rectInt.Y + rectInt.Height / 2 - (sizeBounds.Height - nameBounds.Height) / 2,
            sizeWidth, sizeBounds.Height);

        if (nameBounds.Height + sizeBounds.Height > rectInt.Height)
        {
            var verticalPadding = scaler.Scale(NodeTextVerticalPadding);
            nameRect.Y = Math.Max(nameRect.Y, rectInt.Y);
            nameRect.Height = Math.Min(nameRect.Height, rectInt.Height - verticalPadding);
            sizeRect.Y = nameRect.Y + nameBounds.Height;
            sizeRect.Height = Math.Max(0, rectInt.Height - nameBounds.Height - verticalPadding);
        }

        var textColor = KnownColors.NodeTextColorHex(backgroundColorHex);
        var formatFlags = TextFormatFlags.Left | TextFormatFlags.NoPadding;
        graphics.DrawNodeText(nodeName, nameRect, textColor, formatFlags);
        graphics.DrawNodeText(sizeName, sizeRect, textColor, formatFlags);
    }
}