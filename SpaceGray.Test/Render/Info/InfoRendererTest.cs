using Moq;
using SpaceGray.Core;
using SpaceGray.Core.Render;
using SpaceGray.Core.UI;
using SpaceGray.Test.Util;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceGray.Test.Render.Info;

[TestClass]
public class InfoRendererTest
{
    [TestMethod]
    public void DrawInfo()
    {
        var uiState = new SpaceGrayApplication(new()).UIState;
        var graphics = new Mock<IBufferedGraphics>();
        uiState.SetProperty(nameof(uiState.Graphics), graphics.Object);
        InfoRenderer.DrawInfo(uiState);
        graphics.Verify(graphics => graphics.FillRectangle(KnownColors.InfoBackgroundHex, uiState.ContentRect), Times.Once);
        graphics.Verify(graphics => graphics.FillRectangle(It.IsAny<string>(), It.IsAny<RectangleF>()), Times.Once);
        graphics.Verify(graphics => graphics.DrawNodeText(It.IsAny<string>(), It.IsAny<Rectangle>(), It.IsAny<string>(), It.IsAny<TextFormatFlags>()), Times.Once);
    }
}
