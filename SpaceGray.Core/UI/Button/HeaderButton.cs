using System.Drawing;

namespace SpaceGray.Core.UI;

public abstract class HeaderButton
{
    protected SpaceGrayApplication application;
    protected UIState uiState;
    protected TabsController tabs;
    protected abstract string Text { get; }
    public abstract string ColorHex { get; }
    public bool IsWide { get; set; }
    public RectangleF Rect { get; set; }

    public virtual bool IsVisible => true;
    public virtual bool IsActive => true;

    public HeaderButton(SpaceGrayApplication application)
    {
        this.application = application;
        uiState = application.UIState;
        tabs = uiState.Tabs;
    }

    public string GetText() => IsWide ? Text : Text[..1];
    public abstract void Execute();
}
