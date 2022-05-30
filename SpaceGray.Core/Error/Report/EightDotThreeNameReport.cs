using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class EightDotThreeNameReport : IErrorReport
{
    private readonly string message;
    public string Title => Resources.ReportEightDotThreeName;

    public string Message => message;

    public ErrorSeverity Severity => ErrorSeverity.Warning;

    public EightDotThreeNameReport(string message) => this.message = message;
}
