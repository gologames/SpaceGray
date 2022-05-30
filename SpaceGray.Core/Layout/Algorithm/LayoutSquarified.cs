using SpaceGray.Core.TreeMap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpaceGray.Core.Layout;

public class LayoutSquarified<T>: ILayoutAlgorithm<T> where T : ITreeMapNode
{
    private readonly int squareMinSize;
    public event ILayoutAlgorithm<T>.LayoutNodeEventHandler NodeLayouted;

    public LayoutSquarified(int squareMinSize) => this.squareMinSize = squareMinSize;

    private static float CalcAnotherSide(IList<T> line, ITreeMapNode node,
        float side, float sizeCoef)
    {
        var sizeSum = node?.Size ?? 0;
        foreach (var lineNode in line)
        { sizeSum += lineNode.Size; }
        return sizeSum * sizeCoef / side;
    }
    private static float CalcWorstAspect(IList<T> line, ITreeMapNode node,
        float side, float sizeCoef)
    {
        var anotherSide = CalcAnotherSide(line, node, side, sizeCoef);
        var nodeSide = node.Size * sizeCoef / anotherSide;
        var aspect = Math.Max(anotherSide / nodeSide, nodeSide / anotherSide);
        foreach (var lineNode in line)
        {
            var currNodeSide = lineNode.Size * sizeCoef / anotherSide;
            var currAspect = Math.Max(anotherSide / currNodeSide, currNodeSide / anotherSide);
            aspect = Math.Max(aspect, currAspect);
        }
        return aspect;
    }
    private static void LayoutVertical(List<T> line, ref RectangleF rect,
        float side, float sizeCoef, RectangleF[] rects)
    {
        var y = rect.Y;
        for (var i = 0; i < line.Count; i++)
        {
            var height = line[i].Size * sizeCoef / side;
            rects[i] = new RectangleF(rect.X, y, side, height);
            y += height;
        }
        rect.X += side;
        rect.Width -= side;
    }
    private static void LayoutHorizontal(List<T> line, ref RectangleF rect,
        float side, float sizeCoef, RectangleF[] rects)
    {
        var x = rect.X;
        for (var i = 0; i < line.Count; i++)
        {
            var width = line[i].Size * sizeCoef / side;
            rects[i] = new RectangleF(x, rect.Y, width, side);
            x += width;
        }
        rect.Y += side;
        rect.Height -= side;
    }
    private static void StretchVertical(ref RectangleF rect, RectangleF[] rects)
    {
        for (var i = 0; i < rects.Length; i++)
        { rects[i].Width += rect.Width; }
        rect.X += rect.Width;
        rect.Width = 0.0f;
    }
    private static void StretchHorizontal(ref RectangleF rect, RectangleF[] rects)
    {
        for (var i = 0; i < rects.Length; i++)
        { rects[i].Height += rect.Height; }
        rect.Y += rect.Height;
        rect.Height = 0.0f;
    }
    private void LayoutLine(List<T> line, ref RectangleF rect,
        float side, float sizeCoef)
    {
        var anotherSide = CalcAnotherSide(line, null, side, sizeCoef);
        var rects = new RectangleF[line.Count];
        if (rect.Width > rect.Height)
        {
            LayoutVertical(line, ref rect, anotherSide, sizeCoef, rects);
            if (rect.Width < squareMinSize) StretchVertical(ref rect, rects);
        }
        else
        {
            LayoutHorizontal(line, ref rect, anotherSide, sizeCoef, rects);
            if (rect.Height < squareMinSize) StretchHorizontal(ref rect, rects);
        }
        for (var i = 0; i < line.Count; i++)
        { NodeLayouted?.Invoke(this, new LayoutNodeEventArgs<T>(line[i], rects[i])); }
    }
    private IEnumerable<T> FeetNodesToRect(IEnumerable<T> nodes,
        RectangleF rect, long size, out float sizeCoef)
    {
        float minArea = squareMinSize * squareMinSize;
        float allArea = rect.Width * rect.Height;
        if (allArea < minArea)
        {
            sizeCoef = rect.Width * rect.Height / nodes.First().Size;
            return nodes.Take(1);
        }
        sizeCoef = allArea / size;
        var sumLength = 0L;
        var index = 0;
        foreach (var node in nodes)
        {
            index++;
            if (node.Size * sizeCoef < minArea)
            { sizeCoef = minArea / node.Size; }
            if (sumLength * sizeCoef > allArea)
            {
                sizeCoef = allArea / sumLength;
                return nodes.Take(index);
            }
            sumLength += node.Size;
        }
        return nodes;
    }

    public void Layout(IEnumerable<T> nodes, long size, RectangleF rect)
    {
        var line = new List<T>();
        var worstAspect = float.MaxValue;
        var smallestSide = Math.Min(rect.Width, rect.Height);
        var fittedNodes = FeetNodesToRect(nodes, rect, size, out var sizeCoef);
        foreach (var node in fittedNodes)
        {
            var currAspect = CalcWorstAspect(line, node, smallestSide, sizeCoef);
            if (currAspect < worstAspect)
            {
                worstAspect = currAspect;
                line.Add(node);
            }
            else
            {
                LayoutLine(line, ref rect, smallestSide, sizeCoef);
                if (rect.Width == 0.0f || rect.Height == 0.0f) return;
                line.Clear();
                smallestSide = Math.Min(rect.Width, rect.Height);
                worstAspect = CalcWorstAspect(line, node, smallestSide, sizeCoef);
                line.Add(node);
            }
        }
        LayoutLine(line, ref rect, smallestSide, sizeCoef);
    }
}
