using SpaceGray.Core.Localization;

namespace SpaceGray.Core.UI;

public class ColorButton : HeaderButton
{
    protected override string Text => uiState.IsColored ? Resources.ButtonGray : Resources.ButtonColor;
    public override string ColorHex => tabs.IsInfo || uiState.IsColored ? KnownColors.BackButtonHex : KnownColors.ColorButtonHex;
    public override bool IsActive => tabs.IsFileSystem && uiState.Header.IsFileSystemReady || tabs.IsError && uiState.Header.IsErorReady;

    public ColorButton(SpaceGrayApplication application) : base(application) { }

    public override void Execute() => uiState.ToggleColored();
}
