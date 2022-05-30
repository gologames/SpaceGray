using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class RenamedNewPathIsUnknownReport : IErrorReport
{
    private readonly string message;
    public string Title => Resources.ReportRenamedNewPathIsUnknown;

    public string Message => message;

    public ErrorSeverity Severity => ErrorSeverity.Warning;

    public RenamedNewPathIsUnknownReport(string message) => this.message = message;
}
