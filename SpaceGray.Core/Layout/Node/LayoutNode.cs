using SpaceGray.Core.TreeMap;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceGray.Core.Layout;

public class LayoutNode<T> where T : ITreeMapNode
{
    private readonly List<LayoutNode<T>> children;
    public LayoutNode<T> Parent { get; private set; }
    public T Node { get; }
    public RectangleF Rect { get; }
    public bool IsExpanded { get; private set; }
    public string BaseColoredHex { get; set; }
    public IEnumerable<LayoutNode<T>> Children => children;

    public LayoutNode(T node, RectangleF rect)
    {
        Parent = null;
        Node = node;
        Rect = rect;
        IsExpanded = false;
        children = new();
    }

    public RectangleF GetContentRect(float headerHeight) =>
        new(Rect.X, Rect.Y + headerHeight, Rect.Width, Rect.Height - headerHeight);

    public void SetAsExpanded() => IsExpanded = true;

    public void AddChild(LayoutNode<T> node)
    {
        node.Parent = this;
        children.Add(node);
    }
}
