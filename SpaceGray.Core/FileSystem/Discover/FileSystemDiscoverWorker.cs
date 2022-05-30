using SpaceGray.Core.TreeMap;
using System;
using System.ComponentModel;

namespace SpaceGray.Core.FileSystem;

public class FileSystemDiscoverWorker
{
    private readonly SpaceGrayApplication application;

    public FileSystemDiscoverWorker(SpaceGrayApplication application) => this.application = application;

    public void OnDoneChanged(object _, DoneChangedEventArgs e) => application.UIState.SetFormText(e.Path, e.IsDone);

    private void OnUpdated(object sender, EventArgs e)
    {
        lock (application.FileSystemState.Locker)
        { TreeMapLayoutUpdater.UpdateLayout(application); }
        application.UIState.Invalidate();
    }
    private void OnDoWork(object sender, DoWorkEventArgs e)
    {
        var fileSystemDiscover = new FileSystemDiscover(
            application.FileSystemState, application.ErrorState);
        fileSystemDiscover.DoneChanged += OnDoneChanged;
        application.FileSystemState.FileSystemUpdater.Updated += OnUpdated;
        fileSystemDiscover.DiscoverDirectory();
    }

    private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => application.Run();

    public void Run()
    {
        var fileSystemDiscoverWorker = new BackgroundWorker() { WorkerSupportsCancellation = true };
        fileSystemDiscoverWorker.DoWork += OnDoWork;
        fileSystemDiscoverWorker.RunWorkerCompleted += OnRunWorkerCompleted;
        application.FileSystemState.SetWorker(fileSystemDiscoverWorker);
        fileSystemDiscoverWorker.RunWorkerAsync();
    }
}
