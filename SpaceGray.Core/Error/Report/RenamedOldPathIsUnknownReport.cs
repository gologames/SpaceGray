using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class RenamedOldPathIsUnknownReport : IErrorReport
{
    private readonly string message;
    public string Title => Resources.ReportRenamedOldPathIsUnknown;

    public string Message => message;

    public ErrorSeverity Severity => ErrorSeverity.Warning;

    public RenamedOldPathIsUnknownReport(string message) => this.message = message;
}
