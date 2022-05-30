using SpaceGray.Core;
using SpaceGray.Core.Error;
using SpaceGray.Core.FileSystem;
using SpaceGray.Core.TreeMap;

namespace SpaceGray.Test.UI.State;

[TestClass]
public class UpdateLayoutTest
{
    [TestMethod]
    public void UpdateLayoutNoEffect()
    {
        var application = new SpaceGrayApplication(new());
        application.FileSystemState.SetRoot(new FolderNode(""));
        application.ErrorState.SetRoot(new ErrorRootNode());
        Assert.IsFalse(application.FileSystemLayout.IsReady);
        Assert.IsFalse(application.ErrorLayout.IsReady);
        TreeMapLayoutUpdater.UpdateLayout(application);
        Assert.IsFalse(application.FileSystemLayout.IsReady);
        Assert.IsFalse(application.ErrorLayout.IsReady);
    }

    [TestMethod]
    public void UpdateFileSystemLayout()
    {
        var application = new SpaceGrayApplication(new());
        application.FileSystemState.SetRoot(new FolderNode(""));
        application.ErrorState.SetRoot(new ErrorRootNode());
        application.UIState.Tabs.ShowFileSystem();
        TreeMapLayoutUpdater.UpdateLayout(application);
        Assert.IsTrue(application.FileSystemLayout.IsReady);
        Assert.IsFalse(application.ErrorLayout.IsReady);
    }

    [TestMethod]
    public void UpdateErrorLayout()
    {
        var application = new SpaceGrayApplication(new());
        application.FileSystemState.SetRoot(new FolderNode(""));
        application.ErrorState.SetRoot(new ErrorRootNode());
        application.UIState.Tabs.ToggleError();
        TreeMapLayoutUpdater.UpdateLayout(application);
        Assert.IsFalse(application.FileSystemLayout.IsReady);
        Assert.IsTrue(application.ErrorLayout.IsReady);
    }
}
