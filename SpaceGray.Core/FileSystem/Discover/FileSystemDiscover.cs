using SpaceGray.Core.Error;
using System;
using System.IO;
using System.IO.Abstractions;
using System.Security;

namespace SpaceGray.Core.FileSystem;

public class FileSystemDiscover
{
    private readonly FileSystemState fileSystemState;
    private readonly ErrorState errorState;
    private readonly IFileSystem fileSystem;
    private bool isDone = true;
    public delegate void DoneChangedEventHandler(object sender, DoneChangedEventArgs e);
    public event DoneChangedEventHandler DoneChanged;

    public FileSystemDiscover(FileSystemState fileSystemState, ErrorState errorState)
    {
        this.fileSystemState = fileSystemState;
        this.errorState = errorState;
        fileSystem = new System.IO.Abstractions.FileSystem();
    }

    private void RaiseOnDoneChangedEvent(bool isDone)
    {
        string path;
        lock (fileSystemState.Locker) path = fileSystemState.Root.GetPath();
        DoneChanged?.Invoke(this, new DoneChangedEventArgs(path, isDone));
    }

    private void DiscoverFiles(FileSystemNode directoryNode, string directoryPath)
    {
        foreach (var filePath in fileSystem.Directory.EnumerateFiles(directoryPath))
        {
            if (fileSystemState.Worker.CancellationPending) break;
            try
            {
                var fileInfo = fileSystem.FileInfo.FromFileName(filePath);
                var fileNode = new FileNode(fileInfo.Name, fileInfo.Length);
                lock (fileSystemState.Locker) directoryNode.AddChild(fileNode);
                fileSystemState.FileSystemUpdater.AddFileSystemNode(fileNode);
            }
            catch (SecurityException)
            { ErrorReportUtil.ReportError(errorState, new SecurityReport(filePath)); }
            catch (ArgumentException)
            { ErrorReportUtil.ReportError(errorState, new ArgumentReport(filePath)); }
            catch (UnauthorizedAccessException)
            { ErrorReportUtil.ReportError(errorState, new UnauthorizedAccessReport(filePath)); }
            catch (PathTooLongException)
            { ErrorReportUtil.ReportError(errorState, new PathTooLongReport(filePath)); }
            catch (NotSupportedException)
            { ErrorReportUtil.ReportError(errorState, new FileNotSupportedReport(filePath)); }
        }
    }
    private void DiscoverSubDirectories(FileSystemNode directoryNode, string directoryPath)
    {
        foreach (var subDirectoryPath in fileSystem.Directory.EnumerateDirectories(directoryPath))
        {
            if (fileSystemState.Worker.CancellationPending) break;
            try
            {
                var subDirectoryInfo = fileSystem.DirectoryInfo.FromDirectoryName(subDirectoryPath);
                var subDirectoryNode = new FolderNode(subDirectoryInfo.Name);
                lock (fileSystemState.Locker) directoryNode.AddChild(subDirectoryNode);
                fileSystemState.FileSystemUpdater.AddFileSystemNode(subDirectoryNode);
            }
            catch (SecurityException)
            { ErrorReportUtil.ReportError(errorState, new SecurityReport(subDirectoryPath)); }
            catch (ArgumentException)
            { ErrorReportUtil.ReportError(errorState, new ArgumentReport(subDirectoryPath)); }
            catch (PathTooLongException)
            { ErrorReportUtil.ReportError(errorState, new PathTooLongReport(subDirectoryPath)); }
        }
    }
    public void DiscoverDirectory()
    {
        lock (fileSystemState.Locker)
        {
            fileSystemState.InitRoot();
            fileSystemState.FileSystemUpdater.AddFileSystemNode(fileSystemState.Root);
        }

        while (!fileSystemState.Worker.CancellationPending)
        {
            var directoryNode = fileSystemState.FileSystemUpdater.TryGetNextFileSystemNodeSync();
            if (isDone) RaiseOnDoneChangedEvent(isDone = false);

            if (directoryNode != null)
            {
                var directoryPath = directoryNode.GetPath();
                try
                {
                    DiscoverFiles(directoryNode, directoryPath);
                    DiscoverSubDirectories(directoryNode, directoryPath);
                }
                catch (ArgumentException)
                { ErrorReportUtil.ReportError(errorState, new ArgumentReport(directoryPath)); }
                catch (DirectoryNotFoundException)
                { ErrorReportUtil.ReportError(errorState, new DirectoryNotFoundReport(directoryPath)); }
                catch (PathTooLongException)
                { ErrorReportUtil.ReportError(errorState, new PathTooLongReport(directoryPath)); }
                catch (IOException)
                { ErrorReportUtil.ReportError(errorState, new EnumerateIOReport(directoryPath)); }
                catch (SecurityException)
                { ErrorReportUtil.ReportError(errorState, new SecurityReport(directoryPath)); }
                catch (UnauthorizedAccessException)
                { ErrorReportUtil.ReportError(errorState, new UnauthorizedAccessReport(directoryPath)); }
                directoryNode.SetAsProcessed();
            }

            fileSystemState.FileSystemUpdater.TryUpdate();
            if (fileSystemState.FileSystemUpdater.IsEmpty)
            { RaiseOnDoneChangedEvent(isDone = true); }
        }
    }
}
