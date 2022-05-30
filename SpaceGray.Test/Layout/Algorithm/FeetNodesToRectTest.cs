using Moq;
using SpaceGray.Core.Layout;
using SpaceGray.Core.TreeMap;
using SpaceGray.Test.Util;
using System.Drawing;

namespace SpaceGray.Test.Layout.Algorithm;

[TestClass]
public class FeetNodesToRectTest
{
    private static void FeetNodesToRect(int squareMinSize, IEnumerable<ITreeMapNode> nodes, RectangleF rect,
        long size, List<ITreeMapNode> expectedNodesList, float expectedSizeCoef)
    {
        var layout = new LayoutSquarified<ITreeMapNode>(squareMinSize);
        var parameters = new object[] { nodes, rect, size, 0.0f };
        var actualNodes = layout.Invoke<IEnumerable<ITreeMapNode>>(nameof(FeetNodesToRect), parameters);
        var actualNodesList = actualNodes.ToList();
        Assert.AreEqual(expectedNodesList.Count, actualNodesList.Count);
        for (var i = 0; i < expectedNodesList.Count; i++)
        { Assert.AreSame(expectedNodesList[i], actualNodesList[i]); }
        var actualSizeCoef = parameters[^1];
        Assert.AreEqual(expectedSizeCoef, actualSizeCoef);
    }

    [TestMethod]
    public void AllAreaLessThenMinArea()
    {
        var node1 = new Mock<ITreeMapNode>();
        var size1 = 1L;
        node1.SetupGet(node => node.Size).Returns(size1);
        var node2 = new Mock<ITreeMapNode>();
        var size2 = 1L;
        node2.SetupGet(node => node.Size).Returns(size2);
        var nodes = new List<ITreeMapNode>() { node1.Object, node2.Object };
        var rect = new RectangleF(0.0f, 0.0f, 1.0f, 1.0f);
        var expectedNodes = new List<ITreeMapNode>() { node1.Object };
        FeetNodesToRect(2, nodes, rect, size1 + size2, expectedNodes, 1.0f);
    }

    [TestMethod]
    public void TakeTwoNodesFromThree()
    {
        var node1 = new Mock<ITreeMapNode>();
        var size1 = 5L;
        node1.SetupGet(node => node.Size).Returns(size1);
        var node2 = new Mock<ITreeMapNode>();
        var size2 = 3L;
        node2.SetupGet(node => node.Size).Returns(size2);
        var node3 = new Mock<ITreeMapNode>();
        var size3 = 1L;
        node3.SetupGet(node => node.Size).Returns(size3);
        var nodes = new List<ITreeMapNode>() { node1.Object, node2.Object, node3.Object };
        var rect = new RectangleF(0.0f, 0.0f, 2.5f, 2.5f);
        var expectedNodes = new List<ITreeMapNode>() { node1.Object, node2.Object };
        FeetNodesToRect(2, nodes, rect, size1 + size2 + size3, expectedNodes, 1.25f);
    }
}
