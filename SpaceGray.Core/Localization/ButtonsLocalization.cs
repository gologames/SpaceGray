using System.Collections.Generic;

namespace SpaceGray.Core.Localization;

public static class ButtonsLocalization
{
    public static IEnumerable<string> GetAllButtonsTexts()
    {
        yield return Resources.ButtonOpen;
        yield return Resources.ButtonLess;
        yield return Resources.ButtonMore;
        yield return Resources.ButtonColor;
        yield return Resources.ButtonGray;
        yield return Resources.ButtonMark;
        yield return Resources.ButtonSeek;
        yield return Resources.ButtonInfo;
        yield return Resources.ButtonError;
        yield return Resources.ButtonBack;
    }
}
