using SpaceGray.Core.UI;

namespace SpaceGray.Test.UI.State;

[TestClass]
public class ResolutionScalerTest
{
    [TestMethod]
    public void IdenticalScale()
    {
        var scaler = new ResolutionScaler(1920, 1080);
        var expectedIntScale = 7;
        var expectedFloatScale = 7.5f;
        Assert.AreEqual(expectedIntScale, scaler.Scale(7));
        Assert.AreEqual(expectedFloatScale, scaler.Scale(7.5f));
    }

    [TestMethod]
    public void DoubleScale()
    {
        var scaler = new ResolutionScaler(1920 * 2, 1080 * 2);
        var expectedIntScale = 7 * 2;
        var expectedFloatScale = 7.5f * 2;
        Assert.AreEqual(expectedIntScale, scaler.Scale(7));
        Assert.AreEqual(expectedFloatScale, scaler.Scale(7.5f));
    }

    [TestMethod]
    public void HalfScale()
    {
        var scaler = new ResolutionScaler(1920 / 2, 1080 / 2);
        var expectedIntScale = 7 / 2;
        var expectedFloatScale = 7.5f / 2;
        Assert.AreEqual(expectedIntScale, scaler.Scale(7));
        Assert.AreEqual(expectedFloatScale, scaler.Scale(7.5f));
    }
}
