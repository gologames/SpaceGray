using SpaceGray.Core.Layout;
using SpaceGray.Core.Localization;
using SpaceGray.Core.TreeMap;

namespace SpaceGray.Core.UI;

public class LessButton : HeaderButton
{
    protected override string Text => Resources.ButtonLess;
    public override string ColorHex => tabs.IsInfo ? KnownColors.BackButtonHex : KnownColors.LessButtonHex;
    public override bool IsActive => tabs.IsFileSystem && uiState.Header.IsFileSystemReady || tabs.IsError && uiState.Header.IsErorReady;

    public LessButton(SpaceGrayApplication application) : base(application) { }
    
    private static void DecreaseDepth<T>(TreeMapState<T> treeMapState, LayoutState<T> layoutState) where T : ITreeMapNode
    {
        lock (treeMapState.Locker)
        { layoutState.Depth.DecreaseDepth(layoutState.Root); }
    }
    public override void Execute()
    {
        if (tabs.IsFileSystem) DecreaseDepth(application.FileSystemState, application.FileSystemLayout);
        else if (tabs.IsError) DecreaseDepth(application.ErrorState, application.ErrorLayout);
    }
}
