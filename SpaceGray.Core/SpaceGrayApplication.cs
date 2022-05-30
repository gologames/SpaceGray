using SpaceGray.Core.Error;
using SpaceGray.Core.FileSystem;
using SpaceGray.Core.UI;
using System;
using System.Windows.Forms;

namespace SpaceGray.Core;

public class SpaceGrayApplication
{
    private const int OldestGCGeneration = 2;
    public UIState UIState { get; }
    public FileSystemState FileSystemState { get; }
    public FileSystemLayout FileSystemLayout { get; }
    public ErrorState ErrorState { get; }
    public ErrorLayout ErrorLayout { get; }

    public SpaceGrayApplication(Form form)
    {
        FileSystemState = new(ErrorState = new());
        FileSystemLayout = new();
        ErrorLayout = new();
        UIState = new(this, form);
        UIState.InitHeader();

        FileSystemState.ReadyChanged += (_, e) => UIState.Header.UpdateFileSystemReady(e.IsReady);
        ErrorState.ReadyChanged += (_, e) => UIState.Header.UpdateErrorReady(e.IsReady);
        ErrorState.ReadyChanged += (_, e) => UIState.Tabs.UpdateState(e.IsReady);
        ErrorState.ReadyChanged += (_, _) => UIState.Resize();
        UIState.Resize();
    }

    public void Run()
    {
        FileSystemState.Reset();
        FileSystemLayout.Reset();
        ErrorState.Reset();
        GC.Collect(OldestGCGeneration, GCCollectionMode.Forced);
        new FileSystemDiscoverWorker(this).Run();
        FileSystemState.WatchFileSystem();
        UIState.Reset();
    }
    public void Restart()
    {
        if (FileSystemState.Worker != null)
        {
            if (!FileSystemState.Worker.CancellationPending)
            {
                FileSystemState.Worker.CancelAsync();
                FileSystemState.FileSystemUpdater.WakeUp();
            }
        }
        else Run();
    }
}
