using SpaceGray.Core.Localization;

namespace SpaceGray.Core.UI;

public class InfoButton : HeaderButton
{
    protected override string Text => tabs.IsInfo ? Resources.ButtonBack : Resources.ButtonInfo;
    public override string ColorHex => tabs.IsInfo ? KnownColors.BackButtonHex : KnownColors.InfoButtonHex;
    public override bool IsActive => uiState.Header.IsFileSystemReady;

    public InfoButton(SpaceGrayApplication application) : base(application) { }

    public override void Execute() => tabs.ToggleInfo();
}
