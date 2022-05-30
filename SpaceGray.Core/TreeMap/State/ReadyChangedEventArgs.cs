namespace SpaceGray.Core.TreeMap;

public class ReadyChangedEventArgs
{
    public bool IsReady { get; }

    public ReadyChangedEventArgs(bool isReady) => IsReady = isReady;
}
