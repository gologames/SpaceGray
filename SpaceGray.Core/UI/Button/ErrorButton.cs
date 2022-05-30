using SpaceGray.Core.Localization;

namespace SpaceGray.Core.UI;

public class ErrorButton : HeaderButton
{
    protected override string Text => tabs.IsError ? Resources.ButtonBack : Resources.ButtonError;
    public override string ColorHex => tabs.IsError ? KnownColors.BackButtonHex : KnownColors.ErrorButtonHex;
    public override bool IsVisible => uiState.Header.IsErorReady;

    public ErrorButton(SpaceGrayApplication application) : base(application) { }

    public override void Execute() => tabs.ToggleError();
}
