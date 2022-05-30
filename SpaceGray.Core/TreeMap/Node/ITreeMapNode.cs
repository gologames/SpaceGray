namespace SpaceGray.Core.TreeMap;

public interface ITreeMapNode
{
    string Text { get; }
    int Depth { get; }
    long Size { get; }
    bool HasContent { get; }
}
