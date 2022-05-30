using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class UnauthorizedAccessReport : IErrorReport
{
    private readonly string message;
    public string Title => Resources.ReportUnauthorizedAccess;

    public string Message => message;

    public ErrorSeverity Severity => ErrorSeverity.Warning;

    public UnauthorizedAccessReport(string message) => this.message = message;
}
