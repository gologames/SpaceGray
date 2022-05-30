using System.Collections.Generic;
using System.Linq;

namespace SpaceGray.Core.FileSystem;

public class FolderNode : FileSystemNode
{
    public override bool HasContent => true;
    public FolderNode(string name) : base(name, 0L) => IsProcessed = false;

    private static (IEnumerable<FileSystemNode>, long) SubtractMarkedSize(
        IList<FileSystemNode> childrenList, long size)
    {
        var index = childrenList.Count - 1;
        while (index >= 0 && childrenList[index].IsMarked)
        { size -= childrenList[index--].Size; }
        return (childrenList.Take(index + 1), size);
    }
    public override (IEnumerable<FileSystemNode>, long) GetSortedChildren(bool isMarkMode)
    {
        var sortedChildren = childrenSorter.GetSortedChildren(isMarkMode);
        if (isMarkMode) return (sortedChildren, Size);
        else return SubtractMarkedSize(sortedChildren, Size);
    }
}
