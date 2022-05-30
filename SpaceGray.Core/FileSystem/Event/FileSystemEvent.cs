using System.IO;

namespace SpaceGray.Core.FileSystem;

public class FileSystemEvent
{
    public WatcherChangeTypes ChangeType { get; }
    public bool IsRoot { get; }
    public string Path { get; }
    public string Name { get; }
    public string NewName { get; }

    public FileSystemEvent(WatcherChangeTypes changeType, bool isRoot, string path, string name, string newName)
    {
        ChangeType = changeType;
        IsRoot = isRoot;
        Path = path;
        Name = name;
        NewName = newName;
    }
    public FileSystemEvent(WatcherChangeTypes changeType, bool isRoot, string path, string name)
        : this(changeType, isRoot, path, name, null) { }
}
