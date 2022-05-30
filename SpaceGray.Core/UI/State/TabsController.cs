namespace SpaceGray.Core.UI;

public class TabsController
{
    private enum Tabs
    {
        FileSystem = 0,
        Info = 1,
        Error = 2
    }

    private Tabs previousTab;
    private Tabs tab;
    private bool isMark;
    public bool IsMark => isMark;
    public bool IsFileSystem => tab == Tabs.FileSystem;
    public bool IsInfo => tab == Tabs.Info;
    public bool IsError => tab == Tabs.Error;

    public TabsController()
    {
        previousTab = tab = Tabs.Info;
        isMark = false;
    }

    public void ShowFileSystem()
    {
        previousTab = tab;
        tab = Tabs.FileSystem;
        isMark = !isMark;
    }

    private void ToggleTab(Tabs targetTab)
    {
        if (tab == targetTab)
        {
            tab = previousTab;
            previousTab = targetTab;
        }
        else
        {
            previousTab = tab;
            tab = targetTab;
        }
    }
    public void ToggleInfo() => ToggleTab(Tabs.Info);
    public void ToggleError() => ToggleTab(Tabs.Error);

    public void UpdateState(bool isErrorReady)
    {
        if (IsError && !isErrorReady)
        {
            tab = Tabs.FileSystem;
            previousTab = Tabs.Info;
        }
    }

    public void Reset()
    {
        tab = Tabs.FileSystem;
        isMark = false;
    }
}
