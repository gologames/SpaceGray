using Moq;
using SpaceGray.Core.Layout;
using SpaceGray.Core.TreeMap;
using SpaceGray.Test.Util;
using System.Drawing;

namespace SpaceGray.Test.Layout.Algorithm;

[TestClass]
public class LayoutHorizontalTest
{
    private static void LayoutHorizontal(IList<ITreeMapNode> line,
        RectangleF rect, RectangleF expectedRect, float side,
        float sizeCoef, params RectangleF[] expectedRects)
    {
        var type = typeof(LayoutSquarified<>).MakeGenericType(typeof(ITreeMapNode));
        var rects = new RectangleF[expectedRects.Length];
        var parameters = new object[] { line, rect, side, sizeCoef, rects };
        type.InvokeStatic(nameof(LayoutHorizontal), parameters);
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
        var expectedRect = new RectangleF(rect.X, rect.Y + rect.Height, rect.Width, 0.0f);
        var sizeCoef = rect.Width * rect.Height / size;
        var expectedRects = new RectangleF[] { rect };
        LayoutHorizontal(line, rect, expectedRect, rect.Height, sizeCoef, expectedRects);
    }

    [TestMethod]
    public void OneNodePartOfArea()
    {
        var node = new Mock<ITreeMapNode>();
        var size = 2000L;
        node.SetupGet(node => node.Size).Returns(size);
        var line = new List<ITreeMapNode>() { node.Object };
        var rect = new RectangleF(10.0f, 10.0f, 20.0f, 20.0f);
        var expectedRect = new RectangleF(rect.X, rect.Y + rect.Height / 2.0f, rect.Width, rect.Height / 2.0f);
        var sizeCoef = rect.Width * rect.Height / (2 * size);
        var layoutedExpectedRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height / 2.0f);
        var expectedRects = new RectangleF[] { layoutedExpectedRect };
        LayoutHorizontal(line, rect, expectedRect, rect.Height / 2.0f, sizeCoef, expectedRects);
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
        var expectedRect = new RectangleF(rect.X, rect.Y + rect.Height, rect.Width, 0.0f);
        var sizeCoef = rect.Width * rect.Height / (size1 + size2);
        var layoutedExpectedRect1 = new RectangleF(rect.X, rect.Y,
            rect.Width / (size1 + size2) * size1, rect.Height);
        var layoutedExpectedRect2 = new RectangleF(rect.X + layoutedExpectedRect1.Width,
            layoutedExpectedRect1.Y, rect.Width - layoutedExpectedRect1.Width, rect.Height);
        var expectedRects = new RectangleF[] { layoutedExpectedRect1, layoutedExpectedRect2 };
        LayoutHorizontal(line, rect, expectedRect, rect.Height, sizeCoef, expectedRects);
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
        var expectedRect = new RectangleF(rect.X, rect.Y + rect.Height / 2.0f, rect.Width, rect.Height / 2.0f);
        var sizeCoef = rect.Width * rect.Height / (size1 + size2) / 2;
        var layoutedExpectedRect1 = new RectangleF(rect.X, rect.Y,
            rect.Width / (size1 + size2) * size1, rect.Height / 2.0f);
        var layoutedExpectedRect2 = new RectangleF(rect.X + layoutedExpectedRect1.Width,
            layoutedExpectedRect1.Y, rect.Width - layoutedExpectedRect1.Width, rect.Height / 2.0f);
        var expectedRects = new RectangleF[] { layoutedExpectedRect1, layoutedExpectedRect2 };
        LayoutHorizontal(line, rect, expectedRect, rect.Height / 2.0f, sizeCoef, expectedRects);
    }
}
