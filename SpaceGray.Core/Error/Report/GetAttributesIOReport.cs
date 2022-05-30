using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Error;

public class GetAttributesIOReport : IErrorReport
{
    private readonly string message;
    public string Title => Resources.ReportGetAttributesIO;

    public string Message => message;

    public ErrorSeverity Severity => ErrorSeverity.Warning;

    public GetAttributesIOReport(string message) => this.message = message;
}
