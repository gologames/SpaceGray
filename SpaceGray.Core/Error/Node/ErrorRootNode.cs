using SpaceGray.Core.Localization;
using System.Collections.Generic;
using System.Linq;

namespace SpaceGray.Core.Error;

public class ErrorRootNode : ErrorNode
{
    private readonly List<ErrorGroupNode> children;
    public override ErrorNode Parent => null;
    public override string Text => GetText();
    public override ErrorSeverity Severity => ErrorSeverity.Info;
    public override int Depth => 0;
    public override long Size => 1L;
    public override bool HasContent => true;

    public ErrorRootNode() => children = new();

    private string GetText()
    {
        var countSeverity = new SortedDictionary<ErrorSeverity, int>();
        foreach (var groupNode in children)
        {
            countSeverity.TryGetValue(groupNode.Severity, out var value);
            countSeverity[groupNode.Severity] = value + groupNode.GetChildrenCount();
        }
        return string.Join(", ",
            countSeverity.Reverse().Select(kv => ErrorLocalization.GetSeverityText(kv.Key, kv.Value)));
    }

    private ErrorGroupNode FindErrorGroup(IErrorReport report) =>
        children.Find(errorGroup => errorGroup.Text == report.Title);

    public bool IsEmpty() => children.Count == 0;
    public void AddErrorItem(IErrorReport report)
    {
        var groupNode = FindErrorGroup(report);
        if (groupNode == null)
        {
            groupNode = new ErrorGroupNode(this, report);
            children.Add(groupNode);
        }
        groupNode.AddErrorItem(report);
    }
    public void RemoveErrorNode(ErrorNode node)
    {
        if (!children.Remove(node as ErrorGroupNode))
        {
            var groupNode = FindErrorGroup(node.Report);
            groupNode.RemoveErrorItem(node as ErrorItemNode);
            if (groupNode.GetChildrenCount() == 0) children.Remove(groupNode);
        }
    }

    public override (IEnumerable<ErrorNode>, long) GetSortedChildren()
    {
        children.Sort((node1, node2) => node2.Size.CompareTo(node1.Size));
        return (children, children.Sum(child => child.Size));
    }
}
