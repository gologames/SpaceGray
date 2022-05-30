namespace SpaceGray.Core.TreeMap;

public abstract class TreeMapState<T> where T : ITreeMapNode
{
    public object Locker { get; }
    public T Root { get; private set; }
    public T DisplayRoot { get; private set; }
    public T PreviousDisplayRoot { get; private set; }
    public bool IsReady => DisplayRoot != null;
    public delegate void ReadyChangedEventHandler(object sender, ReadyChangedEventArgs e);
    public event ReadyChangedEventHandler ReadyChanged;

    public TreeMapState() => Locker = new();

    public void SetRoot(T root)
    {
        DisplayRoot = Root = root;
        ReadyChanged?.Invoke(this, new ReadyChangedEventArgs(true));
    }
    public void SetDisplayRoot(T displayRoot, T previousDisplayRoot)
    {
        DisplayRoot = displayRoot;
        PreviousDisplayRoot = previousDisplayRoot;
    }
    public void SetDisplayRoot(T displayRoot) => SetDisplayRoot(displayRoot, DisplayRoot);
    public bool DisplayPreviousRoot()
    {
        if (PreviousDisplayRoot != null)
        {
            SetDisplayRoot(PreviousDisplayRoot);
            return true;
        }
        return false;
    }
    public void ClearPreviousDisplayRoot() => PreviousDisplayRoot = default;

    public virtual void Reset()
    {
        Root = default;
        DisplayRoot = default;
        PreviousDisplayRoot = default;
        ReadyChanged?.Invoke(this, new ReadyChangedEventArgs(false));
    }
}
