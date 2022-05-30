using System.Collections.Generic;

namespace SpaceGray.Core.Error;

public class ErrorGroupNode : ErrorNode
{
    private readonly ErrorRootNode parent;
    private readonly string text;
    private readonly ErrorSeverity severity;
    private readonly long size;
    private readonly IList<ErrorItemNode> children;
    public override ErrorNode Parent => parent;
    public override string Text => text;
    public override ErrorSeverity Severity => severity;
    public override int Depth => 1;
    public override long Size => size;
    public override bool HasContent => true;

    public ErrorGroupNode(ErrorRootNode parent, IErrorReport report)
    {
        this.parent = parent;
        text = report.Title;
        severity = report.Severity;
        size = (long)report.Severity;
        children = new List<ErrorItemNode>();
    }

    public int GetChildrenCount() => children.Count;
    public void AddErrorItem(IErrorReport report) => children.Add(new ErrorItemNode(this, report));
    public void RemoveErrorItem(ErrorItemNode itemNode) => children.Remove(itemNode);

    public override (IEnumerable<ErrorNode>, long) GetSortedChildren() => (children, children.Count);
}
