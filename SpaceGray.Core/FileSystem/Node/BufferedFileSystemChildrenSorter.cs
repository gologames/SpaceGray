using System.Collections.Generic;
using System.Linq;

namespace SpaceGray.Core.FileSystem;

public class BufferedFileSystemChildrenSorter
{
    private bool isReadyForUpdate;
    private bool previousisMarkMode;
    private readonly Dictionary<string, FileSystemNode> children;
    private List<FileSystemNode> sortedChildren;

    public BufferedFileSystemChildrenSorter(Dictionary<string, FileSystemNode> children)
    {
        isReadyForUpdate = true;
        previousisMarkMode = false;
        this.children = children;
        sortedChildren = null;
    }

    public void Update() => isReadyForUpdate = true;

    private void SortChildren(bool isMarkMode)
    {
        sortedChildren = children.Values.ToList();
        sortedChildren.Sort((node1, node2) =>
        {
            if (!isMarkMode)
            {
                if (node1.IsMarked && node2.IsMarked) return 0;
                if (node1.IsMarked) return 1;
                if (node2.IsMarked) return -1;
            }
            var compareSize = node2.Size.CompareTo(node1.Size);
            return compareSize != 0 ? compareSize :
                node1.Text.CompareTo(node2.Text);
        });
    }
    public IList<FileSystemNode> GetSortedChildren(bool isMarkMode)
    {
        if (isReadyForUpdate || previousisMarkMode != isMarkMode)
        {
            SortChildren(isMarkMode);
            isReadyForUpdate = false;
            previousisMarkMode = isMarkMode;
        }
        return sortedChildren;
    }
}
