using SpaceGray.Core.UI;
using SpaceGray.Test.Util;

namespace SpaceGray.Test.UI.State;

[TestClass]
public class ColorLuminanceTest
{
    private static void ColorHexWithLuminance(string hexColor, double luminance, string expectedColorHex)
    {
        var actualColorHex = typeof(KnownColors).InvokeStatic<string>(nameof(ColorHexWithLuminance), hexColor, luminance);
        Assert.AreEqual(expectedColorHex, actualColorHex);
    }

    [TestMethod]
    public void IdenticalLuminance() => ColorHexWithLuminance("#FACADE", 0.0, "#FACADE");

    [TestMethod]
    public void IncreaseLuminance() => ColorHexWithLuminance("#FACADE", -KnownColors.LuminanceStep, "#FFD8EE");

    [TestMethod]
    public void DecreaseLuminance() => ColorHexWithLuminance("#FACADE", KnownColors.LuminanceStep, "#E8BCCE");

    [TestMethod]
    public void MaxLuminance() => ColorHexWithLuminance("#FACADE", 1.0, "#FFFFFF");

    [TestMethod]
    public void MinLuminance() => ColorHexWithLuminance("#FACADE", -1.0, "#000000");
}
