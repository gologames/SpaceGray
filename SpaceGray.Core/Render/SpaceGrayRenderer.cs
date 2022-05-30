namespace SpaceGray.Core.Render;

public static class SpaceGrayRenderer
{
    private static void DrawContent(SpaceGrayApplication application)
    {
        if (application.UIState.Tabs.IsInfo) InfoRenderer.DrawInfo(application.UIState);
        else if (application.UIState.Tabs.IsError) ErrorRenderer.DrawError(application);
        else FileSystemRenderer.DrawFileSystem(application);
    }

    public static void Draw(SpaceGrayApplication application)
    {
        DrawContent(application);
        HeaderRenderer.DrawHeader(application.UIState);
    }
}
