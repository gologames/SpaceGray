using SpaceGray.Core.TreeMap;
using System;
using System.Collections.Generic;

namespace SpaceGray.Core.Layout;

public class DepthController
{
    private const int DefaultDepth = 3;
    private int depth;
    public int MaxDepth => depth;

    public DepthController() => depth = DefaultDepth;

    public bool IsNodeDepthIsLessThanMaxDepth(ITreeMapNode root, ITreeMapNode node) =>
        node.Depth - root.Depth < depth;

    public void IncreaseDepth()
    {
        if (depth < int.MaxValue) depth++;
    }
    public void DecreaseDepth<T>(LayoutNode<T> root) where T : ITreeMapNode
    {
        var maxDepth = 0;
        var stack = new Stack<LayoutNode<T>>();
        stack.Push(root);
        while (stack.Count != 0)
        {
            var node = stack.Pop();
            maxDepth = Math.Max(maxDepth, node.Node.Depth);
            foreach (var child in node.Children) stack.Push(child);
        }
        depth = Math.Min(depth, maxDepth - root.Node.Depth);
        if (depth > 0) depth--;
    }
}
