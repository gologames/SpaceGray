using SpaceGray.Core.Layout;
using SpaceGray.Core.UI;

namespace SpaceGray.Core.Error;

public class ErrorLayout : LayoutState<ErrorNode>
{
    public ErrorNode SelectedCopiedNode { get; set; }

    public static string GetColorHexBySeverity(ErrorSeverity severity)
    {
        return severity switch
        {
            ErrorSeverity.Info => KnownColors.ErrorSeverityInfoHex,
            ErrorSeverity.Warning => KnownColors.ErrorSeverityWarningHex,
            ErrorSeverity.Error => KnownColors.ErrorSeverityErrorHex,
            _ => KnownColors.ItemBorderHex,
        };
    }
}
