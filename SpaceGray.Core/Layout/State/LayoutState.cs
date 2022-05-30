using SpaceGray.Core.TreeMap;

namespace SpaceGray.Core.Layout;

public abstract class LayoutState<T> where T : ITreeMapNode
{
    public LayoutNode<T> Root { get; private set; }
    public bool IsReady => Root != null;
    public DepthController Depth { get; }

    public LayoutState() => Depth = new();

    public bool IsAnyNodeHovered => HoveredNode != null;
    public bool IsAnyNodeClicked => ClickedNode != null;
    public ITreeMapNode HoveredNode { get; private set; }
    public ITreeMapNode ClickedNode { get; private set; }
    public bool IsClickedRight { get; private set; }

    public void SetRoot(LayoutNode<T> root) => Root = root;

    public void SetHoveredNode(ITreeMapNode hoveredNode) => HoveredNode = hoveredNode;
    public void ClearHoveredNode() => HoveredNode = null;
    public void SetClickedNode(ITreeMapNode clickedNode, bool isClickedRight)
    {
        ClickedNode = clickedNode;
        IsClickedRight = isClickedRight;
    }
    public void ClearClickedNode() => ClickedNode = null;

    public virtual void Reset() => Root = null;
}
