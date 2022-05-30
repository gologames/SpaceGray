using SpaceGray.Core.TreeMap;

namespace SpaceGray.Core.Error;

public class ErrorState : TreeMapState<ErrorNode>
{
    private ErrorRootNode root;

    public void ReportError(IErrorReport report)
    {
        if (!IsReady) SetRoot(root = new ErrorRootNode());
        root.AddErrorItem(report);
    }

    public void RemoveError(ErrorNode node)
    {
        if (root == node) Reset();
        else
        {
            root.RemoveErrorNode(node);
            if (root.IsEmpty()) Reset();
        }
    }
}
