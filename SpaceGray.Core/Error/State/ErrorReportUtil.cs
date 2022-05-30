namespace SpaceGray.Core.Error;

public static class ErrorReportUtil
{
    public static void ReportError(ErrorState errorState, IErrorReport report)
    { lock (errorState.Locker) errorState.ReportError(report); }
}
