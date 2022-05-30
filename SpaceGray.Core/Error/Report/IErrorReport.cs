namespace SpaceGray.Core.Error;

public enum ErrorSeverity
{
    Info = 1,
    Warning = 2,
    Error = 3
}

public interface IErrorReport
{
    string Title { get; }
    string Message { get; }
    ErrorSeverity Severity { get; }
}
