namespace SpaceGray.Core.Error;

public class ErrorItemNode : ErrorNode
{
    private readonly ErrorGroupNode parent;
    private readonly IErrorReport report;
    public override ErrorNode Parent => parent;
    public override IErrorReport Report => report;
    public override string Text => report.Message;
    public override ErrorSeverity Severity => report.Severity;
    public override int Depth => 2;
    public override long Size => 1L;
    public override bool HasContent => false;

    public ErrorItemNode(ErrorGroupNode parent, IErrorReport report)
    {
        this.parent = parent;
        this.report = report;
    }
}
