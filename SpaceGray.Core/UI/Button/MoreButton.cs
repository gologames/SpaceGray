using SpaceGray.Core.Localization;

namespace SpaceGray.Core.UI;

public class MoreButton : HeaderButton
{
    protected override string Text => Resources.ButtonMore;
    public override string ColorHex => tabs.IsInfo ? KnownColors.BackButtonHex : KnownColors.MoreButtonHex;
    public override bool IsActive => tabs.IsFileSystem && uiState.Header.IsFileSystemReady || tabs.IsError && uiState.Header.IsErorReady;

    public MoreButton(SpaceGrayApplication application) : base(application) { }

    public override void Execute()
    {
        var depth = tabs.IsError ? application.ErrorLayout.Depth : application.FileSystemLayout.Depth;
        depth.IncreaseDepth();
    }
}
