using SpaceGray.Core.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceGray.Core.UI;

public class BufferedGraphics : IBufferedGraphics
{
    private const string MeasureSymbol = "X";
    private const string FontFamily = "Segoe UI";
    private const int TextFontSizeEm = 9;
    private const int HeaderFontSizeEm = 10;

    private readonly Font textFont;
    private readonly Font headerFont;
    private readonly MeasureTextCache measureText;
    private readonly IDictionary<string, (string, IList<RectangleF>)> fillHatchedRectangles;
    private readonly IDictionary<string, IList<RectangleF>> fillRectangles;
    private readonly IDictionary<string, IList<RectangleF>> drawFileHatchedRectangles;
    private readonly IDictionary<string, IList<RectangleF>> drawFileRectangles;
    private readonly IDictionary<string, IList<RectangleF>> drawFolderHatchedRectangles;
    private readonly IDictionary<string, IList<RectangleF>> drawFolderRectangles;
    private readonly IDictionary<string, IList<RectangleF>> drawHeaderRectangles;
    private readonly IList<(string, float, float, float, float)> drawLines;
    private readonly IList<(string, Rectangle, string, TextFormatFlags)> drawNodeTexts;
    private readonly IList<(string, Rectangle, string, TextFormatFlags)> drawHeaderTexts;
    private readonly float borderWidth;
    public int OneSymbolWidth { get; }
    public int OneSymbolHeight { get; }

    public BufferedGraphics(ResolutionScaler scaler)
    {
        textFont = new(FontFamily, TextFontSizeEm, FontStyle.Regular); ;
        headerFont = new(FontFamily, HeaderFontSizeEm, FontStyle.Bold);
        measureText = new MeasureTextCache();
        fillHatchedRectangles = new Dictionary<string, (string, IList<RectangleF>)>();
        fillRectangles = new Dictionary<string, IList<RectangleF>>();
        drawFileHatchedRectangles = new Dictionary<string, IList<RectangleF>>();
        drawFileRectangles = new Dictionary<string, IList<RectangleF>>();
        drawFolderHatchedRectangles = new Dictionary<string, IList<RectangleF>>();
        drawFolderRectangles = new Dictionary<string, IList<RectangleF>>();
        drawHeaderRectangles = new Dictionary<string, IList<RectangleF>>();
        drawLines = new List<(string, float, float, float, float)>();
        drawNodeTexts = new List<(string, Rectangle, string, TextFormatFlags)>();
        drawHeaderTexts = new List<(string, Rectangle, string, TextFormatFlags)>();
        borderWidth = RenderGroupUtil.GetBorderWidth(scaler);
        var measureSymbolBounds = measureText.MeasureText(MeasureSymbol, textFont);
        OneSymbolWidth = measureSymbolBounds.Width;
        OneSymbolHeight = measureSymbolBounds.Height;
    }

    public Size MeasureNodeText(string text) =>
        measureText.MeasureText(text, textFont, OneSymbolWidth, OneSymbolHeight);
    public Size MeasureHeaderText(string text) => measureText.MeasureText(text, headerFont);

    private RectangleF ShrinkRectToFitBorder(RectangleF rect)
    {
        var unscaledBorderWidth = RenderGroupUtil.GetUnscaledBorderWidth();
        var shift = MathF.Max(0.0f, (borderWidth - unscaledBorderWidth) / 2.0f);
        return new RectangleF(rect.X + shift, rect.Y + shift, rect.Width - 2.0f * shift, rect.Height - 2.0f * shift);
    }
    public void FillHatchedRectangle(string foreColorHex, string backColorGex, RectangleF rect)
    {
        if (!fillHatchedRectangles.TryGetValue(backColorGex, out var pair))
        { pair = fillHatchedRectangles[backColorGex] = (foreColorHex, new List<RectangleF>()); }
        pair.Item2.Add(ShrinkRectToFitBorder(rect));
    }
    public void FillHatchedRectangle(string foreColorHex, string backColorGex,
        float x, float y, float width, float height) =>
        FillHatchedRectangle(foreColorHex, backColorGex, new RectangleF(x, y, width, height));
    private void BufferColorRect(IDictionary<string, IList<RectangleF>> colorRectBuffer,
        string colorHex, RectangleF rect)
    {
        if (!colorRectBuffer.TryGetValue(colorHex, out var list))
        {
            list = colorRectBuffer[colorHex] = new List<RectangleF>();
        }
        list.Add(ShrinkRectToFitBorder(rect));
    }
    public void FillRectangle(string colorHex, RectangleF rect) =>
        BufferColorRect(fillRectangles, colorHex, rect);
    public void FillRectangle(string colorHex, float x, float y, float width, float height) =>
        BufferColorRect(fillRectangles, colorHex, new RectangleF(x, y, width, height));
    public void DrawFileRectangle(string colorHex, RectangleF rect, bool isHatched = false) =>
        BufferColorRect(isHatched ? drawFileHatchedRectangles : drawFileRectangles, colorHex, rect);
    public void DrawFolderRectangle(string colorHex, RectangleF rect, bool isHatched = false) =>
        BufferColorRect(isHatched ? drawFolderHatchedRectangles : drawFolderRectangles, colorHex, rect);
    public void DrawHeaderRectangle(string colorHex, RectangleF rect) =>
        BufferColorRect(drawHeaderRectangles, colorHex, rect);
    public void DrawLine(string colorHex, float x1, float y1, float x2, float y2) =>
        drawLines.Add((colorHex, x1, y1, x2, y2));
    public void DrawNodeText(string text, Rectangle rect, string colorHex, TextFormatFlags formatFlags) =>
        drawNodeTexts.Add((text, rect, colorHex, formatFlags));
    public void DrawHeaderText(string text, Rectangle rect, string colorHex, TextFormatFlags formatFlags) =>
        drawHeaderTexts.Add((text, rect, colorHex, formatFlags));

    public void Render(Graphics graphics)
    {
        BufferedGraphicsRenderer.FillHatchedRectangles(graphics, fillHatchedRectangles);
        BufferedGraphicsRenderer.FillRectangles(graphics, fillRectangles);
        BufferedGraphicsRenderer.DrawRectangles(graphics, borderWidth, drawFileHatchedRectangles);
        BufferedGraphicsRenderer.DrawRectangles(graphics, borderWidth, drawFileRectangles);
        BufferedGraphicsRenderer.DrawRectangles(graphics, borderWidth, drawFolderHatchedRectangles);
        BufferedGraphicsRenderer.DrawRectangles(graphics, borderWidth, drawFolderRectangles);
        BufferedGraphicsRenderer.DrawRectangles(graphics, borderWidth, drawHeaderRectangles);
        BufferedGraphicsRenderer.DrawLines(graphics, borderWidth, drawLines);
        BufferedGraphicsRenderer.DrawTexts(graphics, textFont, drawNodeTexts);
        BufferedGraphicsRenderer.DrawTexts(graphics, headerFont, drawHeaderTexts);
    }

    public void Clear()
    {
        fillHatchedRectangles.Clear();
        fillRectangles.Clear();
        measureText.TryClear();
        drawFileHatchedRectangles.Clear();
        drawFileRectangles.Clear();
        drawFolderHatchedRectangles.Clear();
        drawFolderRectangles.Clear();
        drawHeaderRectangles.Clear();
        drawLines.Clear();
        drawNodeTexts.Clear();
        drawHeaderTexts.Clear();
    }
}
