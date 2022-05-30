using Moq;
using SpaceGray.Core.Layout;
using SpaceGray.Core.TreeMap;
using SpaceGray.Test.Util;
using System.Drawing;

namespace SpaceGray.Test.Layout.Algorithm;

[TestClass]
public class LayoutLineTest
{
    private static void OnNodeLayouted(IDictionary<ITreeMapNode, RectangleF> expectedNodeToRect, ITreeMapNode node, RectangleF rect)
    {
        var expected = expectedNodeToRect[node];
        Assert.AreEqual(expected, rect);
    }
    private static void LayoutLine(int squareMinSize, IList<ITreeMapNode> line, RectangleF rect,
        float side, float sizeCoef, IDictionary<ITreeMapNode, RectangleF> expectedNodeToRect)
    {
        var layout = new LayoutSquarified<ITreeMapNode>(squareMinSize);
        layout.NodeLayouted += (_, e) => OnNodeLayouted(expectedNodeToRect, e.Node, e.Rect);
        layout.Invoke(nameof(LayoutLine), line, rect, side, sizeCoef);
    }

    [TestMethod]
    public void TwoNodes()
    {
        var node1 = new Mock<ITreeMapNode>();
        node1.SetupGet(node => node.Size).Returns(20L);
        var node2 = new Mock<ITreeMapNode>();
        node2.SetupGet(node => node.Size).Returns(20L);
        var line = new List<ITreeMapNode>() { node1.Object, node2.Object };
        var rect = new RectangleF(0.0f, 0.0f, 10.0f, 40.0f);
        var expectedNodeToRect = new Dictionary<ITreeMapNode, RectangleF>
        {
            [node1.Object] = new(rect.X, rect.Y, rect.Width / 2.0f, rect.Height),
            [node2.Object] = new(rect.X + rect.Width / 2.0f, rect.Y, rect.Width / 2.0f, rect.Height)
        };
        LayoutLine(100, line, rect, 10.0f, 10.0f, expectedNodeToRect);
    }

    [TestMethod]
    public void OneNodeStretch()
    {
        var node1 = new Mock<ITreeMapNode>();
        node1.SetupGet(node => node.Size).Returns(20L);
        var line = new List<ITreeMapNode>() { node1.Object };
        var rect = new RectangleF(0.0f, 0.0f, 10.0f, 25.0f);
        var expectedNodeToRect = new Dictionary<ITreeMapNode, RectangleF> { [node1.Object] = rect };
        LayoutLine(10, line, rect, 10.0f, 10.0f, expectedNodeToRect);
    }
}
