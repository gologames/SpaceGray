namespace SpaceGray.Core.FileSystem;

public class DoneChangedEventArgs
{
    public string Path { get; }
    public bool IsDone { get; }

    public DoneChangedEventArgs(string path, bool isDone)
    {
        Path = path;
        IsDone = isDone;
    }
}
