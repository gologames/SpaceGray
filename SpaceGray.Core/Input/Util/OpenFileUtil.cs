using System.Diagnostics;

namespace SpaceGray.Core.Input;

public static class OpenFileUtil
{
    private const string ExplorerApp = "explorer.exe";

    public static void OpenFile(string path)
    {
        var processInfo = new ProcessStartInfo()
        { FileName = ExplorerApp, Arguments = $"/select, \"{path}\"" };
        using var process = Process.Start(processInfo);
    }
}
