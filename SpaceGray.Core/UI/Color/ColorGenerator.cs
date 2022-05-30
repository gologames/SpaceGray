using System;
using System.Collections.Generic;

namespace SpaceGray.Core.UI;

public partial class ColorGenerator
{
    private const string LightSkyBlueHex = "#87CEFA";
    private const string PastelGreenHex = "#77DD77";
    private const string PiggyPinkHex = "#FDD7E4";
    private const string YellowCrayolaHex = "#FBE870";
    private const string MiddleBlueGreenHex = "#88D0C0";
    private const string ExoticFuchsiaHex = "#DCACD3";
    private const string TopazHex = "#FFC475";
    private const string DarkTurquoiseHex = "#00D7D9";
    private const string LightPinkHex = "#FFB4B4";
    private const string LavenderBlueHex = "#C0C0FF";
    private const string YellowGreenHex = "#A3D94F";
    private const string NudeHex = "#DBC4A9";
    private const string AndroidGreenHex = "#00E390";
    private const string PaleCeruleanHex = "#A0CBEA";
    private const string CameoPinkHex = "#F4B8CC";
    private const string BrightCeruleanHex = "#32B3DC";
    private const string DarkYellowGreenHex = "#91D400";
    private const string LawnGreenHex = "#62E300";
    private const int ColorPartMinLimit = 127;
    private const int ColorPartMaxLimit = 256;

    private readonly Random random;
    private readonly IEnumerator<string> coloredEnumerator;

    public ColorGenerator()
    {
        random = new Random();
        coloredEnumerator = GetColoredEnumerator();
    }

    private string GetRandomPastelColorHex()
    {
        var pastelColor = "#";
        for (var i = 0; i < 3; i++)
        {
            var colorPartValue = random.Next(ColorPartMinLimit, ColorPartMaxLimit);
            pastelColor += colorPartValue.ToString("X2");
        }
        return pastelColor;
    }
    private IEnumerator<string> GetColoredEnumerator()
    {
        yield return LightSkyBlueHex;
        yield return PastelGreenHex;
        yield return PiggyPinkHex;
        yield return YellowCrayolaHex;
        yield return MiddleBlueGreenHex;
        yield return ExoticFuchsiaHex;
        yield return TopazHex;
        yield return DarkTurquoiseHex;
        yield return LightPinkHex;
        yield return LavenderBlueHex;
        yield return YellowGreenHex;
        yield return NudeHex;
        yield return AndroidGreenHex;
        yield return PaleCeruleanHex;
        yield return CameoPinkHex;
        yield return BrightCeruleanHex;
        yield return DarkYellowGreenHex;
        yield return LawnGreenHex;
        while (true) yield return GetRandomPastelColorHex();
    }
    public string GetNextColorHex()
    {
        coloredEnumerator.MoveNext();
        return coloredEnumerator.Current;
    }
}
