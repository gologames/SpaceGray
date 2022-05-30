using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class FileNotSupportedReport : IErrorReport
{
    private readonly string message;
    public string Title => Resources.ReportFileNotSupported;

    public string Message => message;

    public ErrorSeverity Severity => ErrorSeverity.Warning;

    public FileNotSupportedReport(string message) => this.message = message;
}
