using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class ArgumentReport : IErrorReport
{
    private readonly string message;
    public string Title => Resources.ReportArgument;

    public string Message => message;

    public ErrorSeverity Severity => ErrorSeverity.Warning;

    public ArgumentReport(string message) => this.message = message;
}
