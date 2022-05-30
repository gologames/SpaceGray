using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class PathTooLongReport : IErrorReport
{
    private readonly string message;
    public string Title => Resources.ReportPathTooLong;

    public string Message => message;

    public ErrorSeverity Severity => ErrorSeverity.Warning;

    public PathTooLongReport(string message) => this.message = message;
}
