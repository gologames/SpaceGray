using Moq;
using SpaceGray.Core.Layout;
using SpaceGray.Core.TreeMap;
using SpaceGray.Test.Util;
using System.Drawing;

namespace SpaceGray.Test.Layout.Algorithm;

[TestClass]
public class LayoutVerticalTest
{
    private static void LayoutVertical(IList<ITreeMapNode> line,
        RectangleF rect, RectangleF expectedRect, float side,
        float sizeCoef, params RectangleF[] expectedRects)
    {
        var type = typeof(LayoutSquarified<>).MakeGenericType(typeof(ITreeMapNode));
        var rects = new RectangleF[expectedRects.Length];
        var parameters = new object[] { line, rect, side, sizeCoef, rects };
        type.InvokeStatic(nameof(LayoutVertical), parameters);
        var actualRect = parameters[1];
        Assert.AreEqual(expectedRect, actualRect);
        for (var i = 0; i < expectedRects.Length; i++)
        { Assert.AreEqual(expectedRects[i], rects[i]); }
    }

    [TestMethod]
    public void OneNodeWholeArea()
    {
        var node = new Mock<ITreeMapNode>();
        var size = 4000L;
        node.SetupGet(node => node.Size).Returns(size);
        var line = new List<ITreeMapNode>() { node.Object };
        var rect = new RectangleF(10.0f, 10.0f, 20.0f, 20.0f);
        var expectedRect = new RectangleF(rect.X + rect.Width, rect.Y, 0.0f, rect.Height);
        var sizeCoef = rect.Width * rect.Height / size;
        var expectedRects = new RectangleF[] { rect };
        LayoutVertical(line, rect, expectedRect, rect.Width, sizeCoef, expectedRects);
    }

    [TestMethod]
    public void OneNodePartOfArea()
    {
        var node = new Mock<ITreeMapNode>();
        var size = 2000L;
        node.SetupGet(node => node.Size).Returns(size);
        var line = new List<ITreeMapNode>() { node.Object };
        var rect = new RectangleF(10.0f, 10.0f, 20.0f, 20.0f);
        var expectedRect = new RectangleF(rect.X + rect.Width / 2.0f, rect.Y, rect.Width / 2.0f, rect.Height);
        var sizeCoef = rect.Width * rect.Height / (2 * size);
        var layoutedExpectedRect = new RectangleF(rect.X, rect.Y, rect.Width / 2.0f, rect.Height);
        var expectedRects = new RectangleF[] { layoutedExpectedRect };
        LayoutVertical(line, rect, expectedRect, rect.Width / 2.0f, sizeCoef, expectedRects);
    }

    [TestMethod]
    public void TwoNodesWholeArea()
    {
        var node1 = new Mock<ITreeMapNode>();
        var size1 = 1500L;
        node1.SetupGet(node => node.Size).Returns(size1);
        var node2 = new Mock<ITreeMapNode>();
        var size2 = 2500L;
        node2.SetupGet(node => node.Size).Returns(size2);
        var line = new List<ITreeMapNode>() { node1.Object, node2.Object };
        var rect = new RectangleF(10.0f, 10.0f, 20.0f, 20.0f);
        var expectedRect = new RectangleF(rect.X + rect.Width, rect.Y, 0.0f, rect.Height);
        var sizeCoef = rect.Width * rect.Height / (size1 + size2);
        var layoutedExpectedRect1 = new RectangleF(rect.X, rect.Y, rect.Width,
            rect.Height / (size1 + size2) * size1);
        var layoutedExpectedRect2 = new RectangleF(rect.X,
            layoutedExpectedRect1.Y + layoutedExpectedRect1.Height, rect.Width,
            rect.Height - layoutedExpectedRect1.Height);
        var expectedRects = new RectangleF[] { layoutedExpectedRect1, layoutedExpectedRect2 };
        LayoutVertical(line, rect, expectedRect, rect.Width, sizeCoef, expectedRects);
    }

    [TestMethod]
    public void TwoNodesPartOfArea()
    {
        var node1 = new Mock<ITreeMapNode>();
        var size1 = 750L;
        node1.SetupGet(node => node.Size).Returns(size1);
        var node2 = new Mock<ITreeMapNode>();
        var size2 = 1250L;
        node2.SetupGet(node => node.Size).Returns(size2);
        var line = new List<ITreeMapNode>() { node1.Object, node2.Object };
        var rect = new RectangleF(10.0f, 10.0f, 20.0f, 20.0f);
        var expectedRect = new RectangleF(rect.X + rect.Width / 2.0f, rect.Y, rect.Width / 2.0f, rect.Height);
        var sizeCoef = rect.Width * rect.Height / (size1 + size2) / 2;
        var layoutedExpectedRect1 = new RectangleF(rect.X, rect.Y, rect.Width / 2.0f,
            rect.Height / (size1 + size2) * size1);
        var layoutedExpectedRect2 = new RectangleF(rect.X,
            layoutedExpectedRect1.Y + layoutedExpectedRect1.Height, rect.Width / 2.0f,
            rect.Height - layoutedExpectedRect1.Height);
        var expectedRects = new RectangleF[] { layoutedExpectedRect1, layoutedExpectedRect2 };
        LayoutVertical(line, rect, expectedRect, rect.Width / 2.0f, sizeCoef, expectedRects);
    }
}
