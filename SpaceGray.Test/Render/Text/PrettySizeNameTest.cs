using SpaceGray.Core.Localization;
using SpaceGray.Core.Render;

namespace SpaceGray.Test.Render.Text;

[TestClass]
public class PrettySizeNameTest
{
    [TestMethod]
    public void ZeroSize()
    {
        var expected = $"0{Resources.SizeByte}";
        var size = 0L;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);

    }

    [TestMethod]
    public void OneByte()
    {
        var expected = $"1{Resources.SizeByte}";
        var size = 1L;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BytesLessThenKilobyte()
    {
        var expected = $"1023{Resources.SizeByte}";
        var size = 1023L;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OneKilobyte()
    {
        var expected = $"1{Resources.SizeKilobyte}";
        var size = 1024L;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OneDotOneKilobyte()
    {
        var expected = $"1.1{Resources.SizeKilobyte}";
        var size = 1024L + 1024L / 10;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void KilobytesLessThenMegabyte()
    {
        var expected = $"1023{Resources.SizeKilobyte}";
        var size = 1024L * 1023;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OneMegabyte()
    {
        var expected = $"1{Resources.SizeMegabyte}";
        var size = 1024L * 1024;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TwoDotTwoMegabytes()
    {
        var expected = $"2.2{Resources.SizeMegabyte}";
        var megabyte = 1024L * 1024;
        var size = 2 * megabyte + 2 * megabyte / 10;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void MegabytesLessThenGigabyte()
    {
        var expected = $"1023{Resources.SizeMegabyte}";
        var megabyte = 1024L * 1024;
        var size = megabyte * 1023;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OneGigabyte()
    {
        var expected = $"1{Resources.SizeGigabyte}";
        var size = (long)Math.Pow(1024L, 3);
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ThreeDotThreeGigabytes()
    {
        var expected = $"3.3{Resources.SizeGigabyte}";
        var gigabyte = (long)Math.Pow(1024L, 3);
        var size = 3 * gigabyte + 3 * gigabyte / 10;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GigabytesLessThenTerabyte()
    {
        var expected = $"1023{Resources.SizeGigabyte}";
        var gigabyte = (long)Math.Pow(1024L, 3);
        var size = gigabyte * 1023;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OneTerabyte()
    {
        var expected = $"1{Resources.SizeTerabyte}";
        var size = (long)Math.Pow(1024L, 4);
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void FourDotFourTerabytes()
    {
        var expected = $"4.4{Resources.SizeTerabyte}";
        var terabyte = (long)Math.Pow(1024L, 4);
        var size = 4 * terabyte + 4 * terabyte / 10;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TerabytesLessThenPetabyte()
    {
        var expected = $"1023{Resources.SizeTerabyte}";
        var terabyte = (long)Math.Pow(1024L, 4);
        var size = terabyte * 1023;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OnePetabyte()
    {
        var expected = $"1{Resources.SizePetabyte}";
        var size = (long)Math.Pow(1024L, 5);
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void FiveDotFivePetabytes()
    {
        var expected = $"5.5{Resources.SizePetabyte}";
        var petabyte = (long)Math.Pow(1024L, 5);
        var size = 5 * petabyte + 5 * petabyte / 10;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PetabytesLessThenExabyte()
    {
        var expected = $"1023{Resources.SizePetabyte}";
        var petabyte = (long)Math.Pow(1024L, 5);
        var size = petabyte * 1023;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OneExabyte()
    {
        var expected = $"1{Resources.SizeExabyte}";
        var size = (long)Math.Pow(1024L, 6);
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SixDotSixOneExabytes()
    {
        var expected = $"6.6{Resources.SizeExabyte}";
        var exabyte = (long)Math.Pow(1024L, 6);
        var size = 6 * exabyte + 6 * exabyte / 10;
        var actual = RenderTextUtil.GetPrettySizeName(size);
        Assert.AreEqual(expected, actual);
    }
}
