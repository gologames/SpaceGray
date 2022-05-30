using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class DirectoryNotFoundReport : IErrorReport
{
    private readonly string message;
    public string Title => Resources.ReportDirectoryNotFound;

    public string Message => message;

    public ErrorSeverity Severity => ErrorSeverity.Info;

    public DirectoryNotFoundReport(string message) => this.message = message;
}
