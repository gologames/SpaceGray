using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceGray.Core.UI;

public class MeasureTextCache
{
    private const int CacheLimit = 4096;
    private readonly IDictionary<string, Size> cache;

    public MeasureTextCache() => cache = new Dictionary<string, Size>();

    private Size MeasureText(string text, Font font, Size proposedSize)
    {
        if (!cache.TryGetValue(text, out var size))
        {
            size = cache[text] = TextRenderer.MeasureText(
                text, font, proposedSize, TextFormatFlags.NoPadding);
        }
        return size;
    }
    public Size MeasureText(string text, Font font) => MeasureText(text, font, default);
    public Size MeasureText(string text, Font font, int symbolWidth, int symbolHeight) =>
        MeasureText(text, font, new Size(symbolWidth * text.Length, symbolHeight));

    public void TryClear()
    {
        if (cache.Count > CacheLimit) cache.Clear();
    }
}
