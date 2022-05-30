namespace SpaceGray.Core.FileSystem;

public class FileNode : FileSystemNode
{
    public override bool HasContent => false;
    public FileNode(string name, long size) : base(name, size) => IsProcessed = true;
}
