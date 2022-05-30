using SpaceGray.Core;
using SpaceGray.Core.FileSystem;
using SpaceGray.Core.Input;

namespace SpaceGray.Test.Input.Header;

[TestClass]
public class BarInputTest
{
    [TestMethod]
    public void MouseDownNoEffect()
    {
        var uiState = new SpaceGrayApplication(new()).UIState;
        Assert.IsFalse(uiState.Header.Bar.IsClicked);
        var actualMouseDown = BarInput.MouseDown(uiState);
        Assert.IsFalse(actualMouseDown);
        Assert.IsFalse(uiState.Header.Bar.IsClicked);
    }
    [TestMethod]
    public void MouseDown()
    {
        var uiState = new SpaceGrayApplication(new()).UIState;
        uiState.Tabs.ShowFileSystem();
        var actualMouseDown = BarInput.MouseDown(uiState);
        Assert.IsTrue(actualMouseDown);
        Assert.IsTrue(uiState.Header.Bar.IsClicked);
    }

    [TestMethod]
    public void DobuleClickNoEffect()
    {
        var application = new SpaceGrayApplication(new());
        var bar = application.UIState.Header.Bar;
        Assert.IsTrue(string.IsNullOrEmpty(bar.Text));
        var actualDoubleClick = BarInput.DobuleClick(application);
        Assert.IsFalse(actualDoubleClick);
        Assert.IsTrue(string.IsNullOrEmpty(bar.Text));
    }
    [TestMethod]
    public void DobuleClickFileSystemTabNoEffect()
    {
        var application = new SpaceGrayApplication(new());
        var uiState = application.UIState;
        uiState.Tabs.ShowFileSystem();
        var actualDoubleClick = BarInput.DobuleClick(application);
        Assert.IsFalse(actualDoubleClick);
        Assert.IsTrue(string.IsNullOrEmpty(uiState.Header.Bar.Text));
    }
    [TestMethod]
    public void DobuleClick()
    {
        var application = new SpaceGrayApplication(new());
        var uiState = application.UIState;
        var rootText = "root";
        var newRootText = "newRoot";
        application.FileSystemState.SetRoot(new FolderNode(rootText));
        application.FileSystemState.SetDisplayRoot(new FolderNode(newRootText));
        uiState.Tabs.ShowFileSystem();
        var actualDoubleClick = BarInput.DobuleClick(application);
        Assert.IsTrue(actualDoubleClick);
        Assert.AreEqual(newRootText, uiState.Header.Bar.Text);
    }

    [TestMethod]
    public void HoverNoEffect()
    {
        var application = new SpaceGrayApplication(new());
        var actualHover = BarInput.Hover(application);
        Assert.IsFalse(actualHover);
    }
    [TestMethod]
    public void HoverFileSystemTabNoPreviousDisplayRoot()
    {
        var application = new SpaceGrayApplication(new());
        var uiState = application.UIState;
        uiState.Tabs.ShowFileSystem();
        uiState.Header.Bar.Text = "text";
        var actualHover = BarInput.Hover(application);
        Assert.IsTrue(actualHover);
        Assert.IsTrue(uiState.Header.Bar.IsHovered);
        Assert.IsTrue(string.IsNullOrEmpty(uiState.Header.Bar.Text));
    }
    [TestMethod]
    public void HoverFileSystemTab()
    {
        var application = new SpaceGrayApplication(new());
        var rootText = "root";
        var newRootText = "newRoot";
        application.FileSystemState.SetRoot(new FolderNode(rootText));
        application.FileSystemState.SetDisplayRoot(new FolderNode(newRootText));
        var uiState = application.UIState;
        uiState.Tabs.ShowFileSystem();
        uiState.Header.Bar.Text = "text";
        var actualHover = BarInput.Hover(application);
        Assert.IsTrue(actualHover);
        Assert.IsTrue(uiState.Header.Bar.IsHovered);
        Assert.AreEqual(rootText, uiState.Header.Bar.Text);
    }
    [TestMethod]
    public void HoverErrorTab()
    {
        var application = new SpaceGrayApplication(new());
        var uiState = application.UIState;
        uiState.Tabs.ToggleError();
        uiState.Header.Bar.Text = "text";
        var actualHover = BarInput.Hover(application);
        Assert.IsTrue(actualHover);
        Assert.IsTrue(string.IsNullOrEmpty(uiState.Header.Bar.Text));
    }

    [TestMethod]
    public void LeaveNoEffect()
    {
        var bar = new SpaceGrayApplication(new()).UIState.Header.Bar;
        var actualLeave = BarInput.Leave(bar, false);
        Assert.IsFalse(actualLeave);
    }
    [TestMethod]
    public void LeaveHovered()
    {
        var bar = new SpaceGrayApplication(new()).UIState.Header.Bar;
        bar.IsHovered = true;
        var actualLeave = BarInput.Leave(bar, false);
        Assert.IsTrue(actualLeave);
        Assert.IsFalse(bar.IsHovered);
    }
    [TestMethod]
    public void LeaveForce()
    {
        var bar = new SpaceGrayApplication(new()).UIState.Header.Bar;
        bar.Text = "text";
        var actualLeave = BarInput.Leave(bar);
        Assert.IsTrue(actualLeave);
        Assert.IsTrue(string.IsNullOrEmpty(bar.Text));
    }

    [TestMethod]
    public void MouseUp()
    {
        var bar = new SpaceGrayApplication(new()).UIState.Header.Bar;
        bar.IsClicked = true;
        BarInput.MouseUp(bar);
        Assert.IsFalse(bar.IsClicked);
    }
}
