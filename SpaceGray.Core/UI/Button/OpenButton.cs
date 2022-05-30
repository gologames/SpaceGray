using SpaceGray.Core.Localization;

namespace SpaceGray.Core.UI;

public class OpenButton : HeaderButton
{
    protected override string Text => Resources.ButtonOpen;
    public override string ColorHex => KnownColors.OpenButtonHex;

    public OpenButton(SpaceGrayApplication application) : base(application) {}

    public override void Execute()
    {
        if (UIState.OpenFolder(application.FileSystemState))
        { application.Restart(); }
    }
}
