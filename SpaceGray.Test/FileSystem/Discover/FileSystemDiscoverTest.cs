using Moq;
using SpaceGray.Core.Error;
using SpaceGray.Core.FileSystem;
using SpaceGray.Test.Util;
using System.ComponentModel;
using System.IO.Abstractions.TestingHelpers;

namespace SpaceGray.Test.Input.Header;

[TestClass]
public class FileSystemDiscoverTest
{
    [TestMethod]
    public void DiscoverDirectoryNoEffect()
    {
        var errorState = new ErrorState();
        var fileSystemState = new FileSystemState(errorState);
        fileSystemState.SetRootPath("/");
        var worker = new BackgroundWorker() { WorkerSupportsCancellation = true };
        fileSystemState.SetWorker(worker);
        var fileSystemUpdater = new Mock<IBufferedFileSystemUpdater>();
        fileSystemState.SetProperty(nameof(fileSystemState.FileSystemUpdater), fileSystemUpdater.Object);
        var fileSystemDiscover = new FileSystemDiscover(fileSystemState, errorState);
        worker.CancelAsync();
        fileSystemDiscover.DiscoverDirectory();
        Assert.AreEqual(0, fileSystemState.Root.GetChildren().Count());
    }

    [TestMethod]
    public void DiscoverOneFolderAndOneFile()
    {
        var errorState = new ErrorState();
        var fileSystemState = new FileSystemState(errorState);
        fileSystemState.SetRootPath(@"C:\");
        var worker = new BackgroundWorker() { WorkerSupportsCancellation = true };
        fileSystemState.SetWorker(worker);
        var fileSystemUpdater = new Mock<IBufferedFileSystemUpdater>();
        fileSystemUpdater.SetupSequence(updater => updater.TryGetNextFileSystemNodeSync())
            .Returns(() => fileSystemState.Root)
            .Returns(() => fileSystemState.Root.GetChildren().First())
            .Returns(() =>
            {
                worker.CancelAsync();
                return null!;
            });
        fileSystemState.SetProperty(nameof(fileSystemState.FileSystemUpdater), fileSystemUpdater.Object);
        var fileSystemDiscover = new FileSystemDiscover(fileSystemState, errorState);
        var mockFileSystem = new MockFileSystem();
        var mockInputFile = new MockFileData("text");
        mockFileSystem.AddFile(@"C:\temp\file.txt", mockInputFile);
        fileSystemDiscover.SetField("fileSystem", mockFileSystem);
        fileSystemDiscover.DiscoverDirectory();
        var rootChildren = fileSystemState.Root.GetChildren();
        Assert.AreEqual(1, rootChildren.Count());
        var subRoot = rootChildren.First();
        Assert.IsTrue(subRoot.HasContent);
        Assert.AreEqual(@"temp", subRoot.Text);
        var subRootChildren = subRoot.GetChildren();
        Assert.AreEqual(1, subRootChildren.Count());
        var file = subRootChildren.First();
        Assert.IsFalse(file.HasContent);
        Assert.AreEqual(@"file.txt", file.Text);
        Assert.AreEqual(0, file.GetChildren().Count());
    }
}
