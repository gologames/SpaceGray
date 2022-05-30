using SpaceGray.Core.Layout;
using SpaceGray.Core.TreeMap;
using SpaceGray.Test.Util;
using System.Drawing;

namespace SpaceGray.Test.Layout.Algorithm;

[TestClass]
public class StretchHorizontalTest
{
    [TestMethod]
    public void StretchHorizontal()
    {
        var type = typeof(LayoutSquarified<>).MakeGenericType(typeof(ITreeMapNode));
        var rect = new RectangleF(0.0f, 10.0f, 20.0f, 10.0f);
        var expectedRect = new RectangleF(rect.X, rect.Y + rect.Height, rect.Width, 0.0f);
        var layoutRect1 = new RectangleF(0.0f, 0.0f, 10.0f, 10.0f);
        var layoutRect2 = new RectangleF(10.0f, 0.0f, 10.0f, 10.0f);
        var rects = new RectangleF[] { layoutRect1, layoutRect2 };
        var layoutedExpectedRect1 = new RectangleF(layoutRect1.X, layoutRect1.Y,
            layoutRect1.Width, layoutRect1.Height + rect.Height);
        var layoutedExpectedRect2 = new RectangleF(layoutRect2.X, layoutRect2.Y,
            layoutRect2.Width, layoutRect2.Height + rect.Height);
        var expectedRects = new RectangleF[] { layoutedExpectedRect1, layoutedExpectedRect2 };
        var parameters = new object[] { rect, rects };
        type.InvokeStatic(nameof(StretchHorizontal), parameters);
        var actualRect = parameters[0];
        Assert.AreEqual(expectedRect, actualRect);
        for (var i = 0; i < expectedRects.Length; i++)
        { Assert.AreEqual(expectedRects[i], rects[i]); }
    }
}
