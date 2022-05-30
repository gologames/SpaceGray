using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class EnumerateIOReport : IErrorReport
{
    private readonly string message;
    public string Title => Resources.ReportEnumerateIO;

    public string Message => message;

    public ErrorSeverity Severity => ErrorSeverity.Warning;

    public EnumerateIOReport(string message) => this.message = message;
}
