using System.Windows.Forms;

namespace SpaceGray.Core.Input;

public static class CopyToClipboardUtil
{
    public static void CopyToClipboard(string text) => Clipboard.SetText(text);
}
