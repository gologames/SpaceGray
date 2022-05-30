using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace SpaceGray.Core.UI;

public static class BufferedGraphicsRenderer
{
    public static void FillHatchedRectangles(Graphics graphics, IDictionary<string, (string, IList<RectangleF>)> fillHatchedRectangles)
    {
        foreach (var kv in fillHatchedRectangles)
        {
            var foreColor = ColorTranslator.FromHtml(kv.Value.Item1);
            var backColor = ColorTranslator.FromHtml(kv.Key);
            using var brush = new HatchBrush(HatchStyle.BackwardDiagonal, foreColor, backColor);
            graphics.FillRectangles(brush, kv.Value.Item2.ToArray());
        }
    }

    public static void FillRectangles(Graphics graphics, IDictionary<string, IList<RectangleF>> fillRectangles)
    {
        foreach (var kv in fillRectangles)
        {
            using var brush = new SolidBrush(ColorTranslator.FromHtml(kv.Key));
            graphics.FillRectangles(brush, kv.Value.ToArray());
        }
    }

    public static void DrawRectangles(Graphics graphics, float borderWidth, IDictionary<string, IList<RectangleF>> drawRectangles)
    {
        foreach (var kv in drawRectangles)
        {
            using var pen = new Pen(ColorTranslator.FromHtml(kv.Key), borderWidth);
            graphics.DrawRectangles(pen, kv.Value.ToArray());
        }
    }

    public static void DrawLines(Graphics graphics, float borderWidth, IList<(string, float, float, float, float)> drawLines)
    {
        foreach (var drawLine in drawLines)
        {
            using var pen = new Pen(ColorTranslator.FromHtml(drawLine.Item1), borderWidth);
            graphics.DrawLine(pen, drawLine.Item2, drawLine.Item3, drawLine.Item4, drawLine.Item5);
        }
    }

    public static void DrawTexts(Graphics graphics, Font font, IList<(string, Rectangle, string, TextFormatFlags)> drawTexts)
    {
        foreach (var drawText in drawTexts)
        {
            var color = ColorTranslator.FromHtml(drawText.Item3);
            TextRenderer.DrawText(graphics, drawText.Item1,
                font, drawText.Item2, color, drawText.Item4);
        }
    }
}
