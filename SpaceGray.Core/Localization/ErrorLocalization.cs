using SpaceGray.Core.Error;
using System;

namespace SpaceGray.Core.Localization;

public static class ErrorLocalization
{
    private static string GetSeverityText(ErrorSeverity severity)
    {
        return severity switch
        {
            ErrorSeverity.Info => Resources.SeverityInfo,
            ErrorSeverity.Warning => Resources.SeverityWarning,
            ErrorSeverity.Error => Resources.SeverityError,
            _ => throw new NotImplementedException()
        };
    }
    public static string GetSeverityText(ErrorSeverity severity, int count)
    {
        var pluralPostfix = severity != ErrorSeverity.Info ? count != 1 ? "s" : "" : "";
        return count + " " + GetSeverityText(severity) + pluralPostfix;
    }

    public static string GetErrorItemCopiedText(string itemText, bool isCopied)
    {
        if (isCopied) itemText += " - " + Resources.BarCopiedToClipboard;
        return itemText;
    }
}
