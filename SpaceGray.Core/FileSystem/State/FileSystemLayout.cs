using SpaceGray.Core.Layout;
using SpaceGray.Core.UI;
using System.Collections.Generic;
using System.IO;

namespace SpaceGray.Core.FileSystem;

public class FileSystemLayout : LayoutState<FileSystemNode>
{
    private ColorGenerator extensionColorGenerator;
    private ColorGenerator nodeColorGenerator;
    private readonly Dictionary<string, string> extensionToColor;
    private readonly Dictionary<FileSystemNode, string> nodeToColor;
    public ISet<FileSystemNode> SelectedMarkedNodes { get; private set; }
    public ISet<string> SelectedBaseColoredHexes { get; private set; }

    public FileSystemLayout()
    {
        extensionToColor = new();
        nodeToColor = new();
        SelectedMarkedNodes = new HashSet<FileSystemNode>();
        SelectedBaseColoredHexes = new HashSet<string>();
    }

    public void SetSelectedMarkedNodes(ISet<FileSystemNode> selectedMarkedNodes) =>
        SelectedMarkedNodes = selectedMarkedNodes;
    public void AddSelectedBaseColoredHex(string baseColoredHex) =>
        SelectedBaseColoredHexes.Add(baseColoredHex);
    public void ResetSelectedBaseColoredHexes() =>
        SelectedBaseColoredHexes.Clear();

    public string GetColorHexByFilename(string filename)
    {
        var extension = Path.GetExtension(filename);
        if (extensionToColor.TryGetValue(extension, out var value))
        { return value; }
        var color = extensionColorGenerator.GetNextColorHex();
        extensionToColor[extension] = color;
        return color;
    }
    public string GetColorHexByNode(FileSystemNode node)
    {
        if (nodeToColor.TryGetValue(node, out var value))
        { return value; }
        var color = nodeColorGenerator.GetNextColorHex();
        nodeToColor[node] = color;
        return color;
    }

    public override void Reset()
    {
        base.Reset();
        extensionColorGenerator = new();
        nodeColorGenerator = new();
        extensionToColor.Clear();
        nodeToColor.Clear();
        extensionToColor[""] =
            KnownColors.ItemBackgroundColorHex(0, false, false, false);
    }
}
