using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class ChangeEventOnUnaccessedPathReport : IErrorReport
{
    private readonly string message;
    public string Title => Resources.ReportChangeEventOnUnaccessedPath;

    public string Message => message;

    public ErrorSeverity Severity => ErrorSeverity.Info;

    public ChangeEventOnUnaccessedPathReport(string message) => this.message = message;
}
