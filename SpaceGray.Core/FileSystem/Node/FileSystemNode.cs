using SpaceGray.Core.TreeMap;
using System;
using System.Collections.Generic;
using System.IO;

namespace SpaceGray.Core.FileSystem;

public abstract class FileSystemNode : ITreeMapNode
{
    private string name;
    private int depth;
    protected long size;
    protected readonly Dictionary<string, FileSystemNode> children;
    protected readonly BufferedFileSystemChildrenSorter childrenSorter;
    public FileSystemNode Parent { get; private set; }
    public string Text => name;
    public int Depth => depth;
    public long Size => size;
    public virtual bool HasContent => throw new NotImplementedException();

    public bool IsProcessed { get; protected set; }
    public bool IsCalculated { get; private set; }
    public bool IsMarked { get; private set; }

    public FileSystemNode(string name, long size)
    {
        childrenSorter = new(children = new());
        this.name = name;
        depth = 0;
        this.size = size;
        IsCalculated = false;
        IsMarked = false;
    }

    public string GetPath()
    {
        var path = name;
        var node = Parent;
        while (node != null)
        {
            path = Path.Combine(node.name, path);
            node = node.Parent;
        }
        return path.ToString();
    }
    public void Rename(string name) => this.name = name;
    public void SetSize(long size) => this.size = size;
    public void AddSize(long size)
    {
        this.size += size;
        childrenSorter.Update();
    }

    public void AddChild(FileSystemNode node)
    {
        node.Parent = this;
        node.depth = depth + 1;
        children[node.name] = node;
        childrenSorter.Update();
    }
    public void RemoveChild(FileSystemNode node)
    {
        node.Parent = null;
        node.depth = 0;
        children.Remove(node.name);
        childrenSorter.Update();
    }

    public void SetAsProcessed() => IsProcessed = true;
    public void SetAsCalculated() => IsCalculated = true;

    private void SetMark(bool isMarked)
    {
        IsMarked = isMarked;
        if (Parent != null) Parent.childrenSorter.Update();
    }
    public void Mark() => SetMark(true);
    public void Unmark() => SetMark(false);

    public bool ContainsChildByName(string childName) => children.ContainsKey(childName);
    public FileSystemNode GetChildByName(string childName) => children[childName];

    public virtual IEnumerable<FileSystemNode> GetChildren() => children.Values;
    public virtual (IEnumerable<FileSystemNode>, long) GetSortedChildren(bool isMarkMode) =>
        throw new NotImplementedException();

    public void ClearChildren()
    {
        size = 0L;
        children.Clear();
    }
}
