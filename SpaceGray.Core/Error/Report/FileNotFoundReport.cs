using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class FileNotFoundReport : IErrorReport
{
    private readonly string message;
    public string Title => Resources.ReportFileNotFound;

    public string Message => message;

    public ErrorSeverity Severity => ErrorSeverity.Info;

    public FileNotFoundReport(string message) => this.message = message;
}
