using SpaceGray.Core.Error;
using SpaceGray.Core.TreeMap;
using System.ComponentModel;

namespace SpaceGray.Core.FileSystem;

public class FileSystemState : TreeMapState<FileSystemNode>
{
    private readonly ErrorState errorState;
    private string rootPath;
    private readonly BufferedFileSystemWatcher fileSystemWatcher;
    public BackgroundWorker Worker { get; private set; }
    public IBufferedFileSystemUpdater FileSystemUpdater { get; private set; }

    public FileSystemState(ErrorState errorState)
    {
        this.errorState = errorState;
        fileSystemWatcher = new(errorState);
    }

    public void SetRootPath(string path) => rootPath = path;
    public void InitRoot() => SetRoot(new FolderNode(rootPath));

    public void SetWorker(BackgroundWorker worker) => Worker = worker;
    public void WatchFileSystem()
    {
        string path;
        lock (Locker) path = rootPath;
        fileSystemWatcher.WatchToBuffer(path, FileSystemUpdater);
    }

    public override void Reset()
    {
        base.Reset();
        fileSystemWatcher.Reset();
        FileSystemUpdater = new BufferedFileSystemUpdater(this, errorState);
    }
}
