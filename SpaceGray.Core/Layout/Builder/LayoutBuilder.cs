using SpaceGray.Core.TreeMap;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceGray.Core.Layout;

public class LayoutBuilder<T> where T : ITreeMapNode
{
    private readonly ILayoutAlgorithm<T> layoutAlgorithm;
    private readonly LayoutNode<T> layoutRoot;
    private LayoutNode<T> currLayoutNode;
    private readonly Queue<LayoutNode<T>> layoutQueue;

    public LayoutBuilder(ILayoutAlgorithm<T> layoutAlgorithm, T treeMapRoot, RectangleF rect)
    {
        this.layoutAlgorithm = layoutAlgorithm;
        layoutRoot = new LayoutNode<T>(treeMapRoot, rect);
        currLayoutNode = null;
        layoutQueue = new Queue<LayoutNode<T>>();
    }

    private void OnNodeLayouted(object _, LayoutNodeEventArgs<T> e)
    {
        var layoutNode = new LayoutNode<T>(e.Node, e.Rect);
        currLayoutNode.AddChild(layoutNode);
        if (layoutNode.Node.HasContent)
        { layoutQueue.Enqueue(layoutNode); }
    }

    public LayoutNode<T> BuildTreeMap(Func<LayoutNode<T>, RectangleF?> contentRectGetter,
        Func<T, (IEnumerable<T>, long)> sortedChildrenGetter)
    {
        layoutQueue.Enqueue(layoutRoot);
        layoutAlgorithm.NodeLayouted += OnNodeLayouted;
        while (layoutQueue.Count != 0)
        {
            currLayoutNode = layoutQueue.Dequeue();
            var contentRect = contentRectGetter(currLayoutNode);
            if (contentRect != null)
            {
                var (sortedChildren, size) = sortedChildrenGetter(currLayoutNode.Node);
                if (size != 0L)
                {
                    currLayoutNode.SetAsExpanded();
                    layoutAlgorithm.Layout(sortedChildren, size, (RectangleF)contentRect);
                }
            }
        }
        return layoutRoot;
    }
}
