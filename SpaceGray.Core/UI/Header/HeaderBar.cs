using System.Drawing;

namespace SpaceGray.Core.UI;

public class HeaderBar
{
    private readonly UIState uiState;
    private readonly TabsController tabs;
    public RectangleF Rect { get; set; }
    public string Text { get; set; }
    public bool IsActive => tabs.IsFileSystem && uiState.Header.IsFileSystemReady || tabs.IsError && uiState.Header.IsErorReady;
    public bool IsHovered { get; set; }
    public bool IsClicked { get; set; }

    public HeaderBar(SpaceGrayApplication application)
    {
        uiState = application.UIState;
        tabs = uiState.Tabs;
        ClearText();
    }
    public void ClearText() => Text = "";
}
