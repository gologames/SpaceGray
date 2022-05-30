using System;
using System.Globalization;
using SpaceGray.Core.Localization;

namespace SpaceGray.Core.Render;

public static class RenderTextUtil
{
    private static readonly string[] sizePostfixes = { Resources.SizeByte, Resources.SizeKilobyte, Resources.SizeMegabyte,
        Resources.SizeGigabyte, Resources.SizeTerabyte, Resources.SizePetabyte, Resources.SizeExabyte };

    public static string GetPrettySizeName(long size)
    {
        var index = size != 0 ? Convert.ToInt32(Math.Floor(Math.Log(size, 1024))) : 0;
        var number = size / Math.Pow(1024, index);
        return number.ToString("0.#", CultureInfo.InvariantCulture) + sizePostfixes[index];
    }
}
