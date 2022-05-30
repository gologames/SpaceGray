using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class SecurityReport : IErrorReport
{
    private readonly string message;
    public string Title => Resources.ReportSecurity;

    public string Message => message;

    public ErrorSeverity Severity => ErrorSeverity.Warning;

    public SecurityReport(string message) => this.message = message;
}
