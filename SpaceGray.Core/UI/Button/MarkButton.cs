using SpaceGray.Core.Localization;

namespace SpaceGray.Core.UI;

public class MarkButton : HeaderButton
{
    protected override string Text => tabs.IsMark ? Resources.ButtonSeek : Resources.ButtonMark;
    public override string ColorHex => tabs.IsMark || !uiState.Header.IsFileSystemReady ? KnownColors.BackButtonHex : KnownColors.MarkButtonHex;
    public override bool IsActive => uiState.Header.IsFileSystemReady;

    public MarkButton(SpaceGrayApplication application) : base(application) { }

    public override void Execute() => tabs.ShowFileSystem();
}
