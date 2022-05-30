using SpaceGray.Core.TreeMap;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceGray.Core.Layout;

public interface ILayoutAlgorithm<T> where T : ITreeMapNode
{
    delegate void LayoutNodeEventHandler(object sender, LayoutNodeEventArgs<T> e);
    event LayoutNodeEventHandler NodeLayouted;
    void Layout(IEnumerable<T> nodes, long size, RectangleF rect);
}
