using Moq;
using SpaceGray.Core.Layout;
using SpaceGray.Core.TreeMap;
using SpaceGray.Test.Util;

namespace SpaceGray.Test.Layout.Algorithm;

[TestClass]
public class CalcAnotherSideTest
{
    private static void CalcAnotherSide(IList<ITreeMapNode> line, ITreeMapNode node,
        float side, float sizeCoef, float expected)
    {
        var type = typeof(LayoutSquarified<>).MakeGenericType(typeof(ITreeMapNode));
        var actual = type.InvokeStatic<float>(nameof(CalcAnotherSide), line, node, side, sizeCoef);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OneNode()
    {
        var node = new Mock<ITreeMapNode>();
        node.SetupGet(node => node.Size).Returns(120L);
        var line = new List<ITreeMapNode>() { node.Object };
        CalcAnotherSide(line, null!, 3.0f, 0.1f, 4.0f);
    }

    [TestMethod]
    public void TwoNodes()
    {
        var node1 = new Mock<ITreeMapNode>();
        node1.SetupGet(node => node.Size).Returns(100L);
        var node2 = new Mock<ITreeMapNode>();
        node2.SetupGet(node => node.Size).Returns(300L);
        var line = new List<ITreeMapNode>() { node1.Object, node2.Object };
        CalcAnotherSide(line, null!, 5.0f, 0.1f, 8.0f);
    }

    [TestMethod]
    public void TwoNodesAndNode()
    {
        var node1 = new Mock<ITreeMapNode>();
        node1.SetupGet(node => node.Size).Returns(100L);
        var node2 = new Mock<ITreeMapNode>();
        node2.SetupGet(node => node.Size).Returns(150L);
        var line = new List<ITreeMapNode>() { node1.Object, node2.Object };
        var node3 = new Mock<ITreeMapNode>();
        node3.SetupGet(node => node.Size).Returns(250L);
        CalcAnotherSide(line, node3.Object, 5.0f, 0.1f, 10.0f);
    }
}
