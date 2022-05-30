using SpaceGray.Core.Error;
using System;
using System.IO;
using System.Security;

namespace SpaceGray.Core.FileSystem;

public class FileSystemEventsHandler
{
    private readonly IBufferedFileSystemUpdater fileSystemUpdater;
    private readonly FileSystemState fileSystemState;
    private readonly ErrorState errorState;

    public FileSystemEventsHandler(IBufferedFileSystemUpdater fileSystemUpdater,
        FileSystemState fileSystemState, ErrorState errorState)
    {
        this.fileSystemUpdater = fileSystemUpdater;
        this.fileSystemState = fileSystemState;
        this.errorState = errorState;
    }

    private void ProcessFileSystemEvent(FileSystemEvent fileSystemEvent)
    {
        if (fileSystemEvent.IsRoot)
        {
            RootEventsHandler.ProcessRootEvent(fileSystemUpdater, fileSystemState, fileSystemEvent);
            return;
        }

        var nameParts = FileSystemPathUtil.SplitPathParts(fileSystemEvent.Name);
        var parentNode = FileSystemPathUtil.FindParentFolder(fileSystemState.Root, nameParts, errorState);
        if (parentNode == null) return;

        var path = fileSystemEvent.Path;
        var nodeName = nameParts[^1];
        var containsName = parentNode.ContainsChildByName(nodeName);
        try
        {
            switch (fileSystemEvent.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    if (!containsName)
                    { SubrootEventsHandlers.ProcessCreatedEvent(fileSystemUpdater, path, parentNode); }
                    break;
                case WatcherChangeTypes.Deleted:
                    if (containsName)
                    { SubrootEventsHandlers.ProcessDeletedEvent(fileSystemState, parentNode, nodeName); }
                    break;
                case WatcherChangeTypes.Changed:
                    if (containsName)
                    { SubrootEventsHandlers.ProcessChangedEvent(path, parentNode, nodeName); }
                    break;
                case WatcherChangeTypes.Renamed:
                    if (containsName)
                    { SubrootEventsHandlers.ProcessRenamedEvent(parentNode, nodeName, fileSystemEvent.NewName); }
                    break;
            }
        }
        catch (ArgumentException)
        { ErrorReportUtil.ReportError(errorState, new ArgumentReport(path)); }
        catch (PathTooLongException)
        { ErrorReportUtil.ReportError(errorState, new PathTooLongReport(path)); }
        catch (NotSupportedException)
        { ErrorReportUtil.ReportError(errorState, new FileNotSupportedReport(path)); }
        catch (FileNotFoundException)
        { ErrorReportUtil.ReportError(errorState, new FileNotFoundReport(path)); }
        catch (DirectoryNotFoundException)
        { ErrorReportUtil.ReportError(errorState, new DirectoryNotFoundReport(path)); }
        catch (IOException)
        { ErrorReportUtil.ReportError(errorState, new GetAttributesIOReport(path)); }
        catch (UnauthorizedAccessException)
        { ErrorReportUtil.ReportError(errorState, new UnauthorizedAccessReport(path)); }
        catch (SecurityException)
        { ErrorReportUtil.ReportError(errorState, new SecurityReport(path)); }
    }
    public void ProcessFileSystemEvents()
    {
        var count = fileSystemUpdater.GetCountOfFileSystemEvents();
        while (count-- > 0)
        { ProcessFileSystemEvent(fileSystemUpdater.GetNextFileSystemEvent()); }
    }
}
