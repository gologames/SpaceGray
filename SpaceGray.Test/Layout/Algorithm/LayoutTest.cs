using Moq;
using SpaceGray.Core.Layout;
using SpaceGray.Core.TreeMap;
using System.Drawing;

namespace SpaceGray.Test.Layout.Algorithm;

[TestClass]
public class LayoutTest
{
    private static void OnNodeLayouted(IDictionary<ITreeMapNode, RectangleF> expectedNodeToRect, ITreeMapNode node, RectangleF rect)
    {
        var expected = expectedNodeToRect[node];
        Assert.AreEqual(expected, rect);
    }
    private static void Layout(int squareMinSize, IEnumerable<ITreeMapNode> nodes, long size, RectangleF rect,
        IDictionary<ITreeMapNode, RectangleF> expectedNodeToRect)
    {
        var layout = new LayoutSquarified<ITreeMapNode>(squareMinSize);
        layout.NodeLayouted += (_, e) => OnNodeLayouted(expectedNodeToRect, e.Node, e.Rect);
        layout.Layout(nodes, size, rect);
    }

    [TestMethod]
    public void ThreeNodes()
    {
        var node1 = new Mock<ITreeMapNode>();
        var size1 = 4L;
        node1.SetupGet(node => node.Size).Returns(size1);
        var node2 = new Mock<ITreeMapNode>();
        var size2 = 1L;
        node2.SetupGet(node => node.Size).Returns(size2);
        var node3 = new Mock<ITreeMapNode>();
        var size3 = 1L;
        node3.SetupGet(node => node.Size).Returns(size3);
        var nodes = new List<ITreeMapNode>() { node1.Object, node2.Object, node3.Object };
        var rect = new RectangleF(0.0f, 0.0f, 3.0f, 2.0f);
        var node1width = rect.Width / 3 * 2;
        var expectedNodeToRect = new Dictionary<ITreeMapNode, RectangleF>
        {
            [node1.Object] = new(rect.X, rect.Y, node1width, rect.Height),
            [node2.Object] = new(rect.X + node1width, rect.Y, rect.Width / 3, rect.Height/ 2),
            [node3.Object] = new(rect.X + node1width, rect.Y + rect.Height / 2, rect.Width / 3, rect.Height / 2)
        };
        Layout(1, nodes, size1 + size2 + size3, rect, expectedNodeToRect);
    }
}
