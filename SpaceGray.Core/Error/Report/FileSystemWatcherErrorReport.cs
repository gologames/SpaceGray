using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class FileSystemWatcherErrorReport : IErrorReport
{
    public string Title => Resources.ReportFileSystemWatcherErrorTitle;

    public string Message => Resources.ReportFileSystemWatcherErrorMessage;

    public ErrorSeverity Severity => ErrorSeverity.Error;
}
