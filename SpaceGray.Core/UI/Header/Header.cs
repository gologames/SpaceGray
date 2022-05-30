using System.Drawing;

namespace SpaceGray.Core.UI;

public class Header
{
    public RectangleF Rect { get; private set; }
    public HeaderButtons Buttons { get; }
    public HeaderBar Bar { get; }
    public bool IsFileSystemReady { get; protected set; }
    public bool IsErorReady { get; protected set; }

    public Header(SpaceGrayApplication application)
    {
        Buttons = new(application);
        Bar = new(application);
    }

    public void UpdateFileSystemReady(bool isReady) => IsFileSystemReady = isReady;
    public void UpdateErrorReady(bool isReady)
    {
        IsErorReady = isReady;
        if (!IsErorReady) Bar.ClearText();
    }

    public void Resize(float x, float y, float fullWidth)
    {
        Buttons.Resize(x, y, fullWidth);
        Rect = new(x, y, fullWidth, Buttons.Rect.Height);
        Bar.Rect = new(Buttons.Rect.X + Buttons.Rect.Width, Rect.Y,
            Rect.Width - Buttons.Rect.Width, Rect.Height);
    }

    public void Reset() => Bar.ClearText();
}
