using SpaceGray.Core.TreeMap;
using System.Drawing;

namespace SpaceGray.Core.Layout;

public class LayoutNodeEventArgs<T> where T : ITreeMapNode
{
    public T Node { get; }
    public RectangleF Rect { get; }

    public LayoutNodeEventArgs(T node, RectangleF rect)
    {
        Node = node;
        Rect = rect;
    }
}
