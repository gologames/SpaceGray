using System;
using System.Globalization;

namespace SpaceGray.Core.UI;

public class KnownColors
{
    public const string TextDarkHex = "#000000";
    public const string TextLightHex = "#8B8B8B";
    public const string OpenButtonHex = "#18453B";
    public const string LessButtonHex = "#2D5970";
    public const string MoreButtonHex = "#304656";
    public const string ColorButtonHex = "#B44B34";
    public const string MarkButtonHex = "#660000";
    public const string InfoButtonHex = "#5C707E";
    public const string ErrorButtonHex = "#B0001A";
    public const string BackButtonHex = "#444A50";
    public const string ItemBorderHex = "#BFBFBF";
    public const string ItemBackgroundHex = "#E1E1E1";
    public const string GroupBorderHex = "#999999";
    public const string GroupBorderColoredHex = "#F4B400";
    public const string GroupBackgroundHex = "#CCCCCC";
    public const string GroupBackgroundColoredHex = "#FFE69A";
    public const string ErrorSeverityInfoHex = "#A4DDED";
    public const string ErrorSeverityWarningHex = "#F6CDA1";
    public const string ErrorSeverityErrorHex = "#FBB7B7";
    public const string HeaderBorderHex = "#808080";
    public const string HeaderBackgroundHex = "#F0F0F0";
    public const string InfoBackgroundHex = "#E1E1E1";
    public const double LuminanceStep = -0.07;
    public const int DarknessLimit = 69;

    private static string ColorHexWithLuminance(string hexColor, double luminance)
    {
        var resultColor = "#";
        for (var i = 0; i < 3; i++)
        {
            var colorPartValue = int.Parse(hexColor.Substring(1 + i * 2, 2), NumberStyles.HexNumber);
            var partWithLuminance = Math.Clamp(colorPartValue + colorPartValue * luminance, 0, 255);
            var roundValue = (int)Math.Round(partWithLuminance);
            resultColor += roundValue.ToString("X2");
        }
        return resultColor;
    }

    public static string ItemBorderColorHex(long depth, bool isColored, string baseColoredHex = null)
    {
        var fileBorderHex = isColored ? baseColoredHex : ItemBorderHex;
        return ColorHexWithLuminance(fileBorderHex, LuminanceStep * depth);
    }
    public static string ItemBackgroundColorHex(long depth, bool isHovered, bool isClicked,
        bool isColored, string baseColoredHex = null)
    {
        if (isHovered)
        {
            depth--;
            if (isClicked) depth--;
        }
        if (isColored) depth--;
        var fileBackgroundHex = isColored ? baseColoredHex : ItemBackgroundHex;
        return ColorHexWithLuminance(fileBackgroundHex, LuminanceStep * depth);
    }

    public static string GroupBorderColorHex(long depth, bool isColorized)
    {
        return ColorHexWithLuminance(isColorized ?
            GroupBorderColoredHex : GroupBorderHex, LuminanceStep * depth);
    }
    public static string GroupBackgroundColorHex(long depth, bool isHovered,
        bool isClicked, bool isColorized)
    {
        if (isHovered)
        {
            depth--;
            if (isClicked) depth--;
        }
        return ColorHexWithLuminance(isColorized ?
            GroupBackgroundColoredHex : GroupBackgroundHex, LuminanceStep * depth);
    }

    public static string HeaderBackgroundColorHex(bool isActive, bool isHovered, bool isClicked)
    {
        var stepCount = isActive ? isHovered ? isClicked ? 1 : -1 : 0 : 1;
        return ColorHexWithLuminance(HeaderBackgroundHex, LuminanceStep * stepCount);
    }

    public static string NodeTextColorHex(string backgroundColorHex)
    {
        var redPartValue = int.Parse(backgroundColorHex.Substring(1, 2), NumberStyles.HexNumber);
        var greenPartValue = int.Parse(backgroundColorHex.Substring(3, 2), NumberStyles.HexNumber);
        var bluePartValue = int.Parse(backgroundColorHex.Substring(5, 2), NumberStyles.HexNumber);
        var luminance = 0.2126 * redPartValue + 0.7152 * greenPartValue + 0.0722 * bluePartValue;
        return luminance < DarknessLimit ? TextLightHex : TextDarkHex;
    }
}
