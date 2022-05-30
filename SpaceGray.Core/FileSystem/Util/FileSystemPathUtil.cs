using SpaceGray.Core.Error;
using System;
using System.IO;

namespace SpaceGray.Core.FileSystem;

public static class FileSystemPathUtil
{
    public static bool IsRootFolder(string path, string rootFolder)
    {
        return path.StartsWith(rootFolder) && path.Length > rootFolder.Length &&
            (path[rootFolder.Length] == Path.DirectorySeparatorChar ||
            path[rootFolder.Length] == Path.AltDirectorySeparatorChar);
    }

    public static string RemoveRootFolder(string path, string rootFolder)
    {
        var index = rootFolder.Length;
        while (index < path.Length && path[index] == Path.DirectorySeparatorChar || path[index] == Path.AltDirectorySeparatorChar)
        { index++; }
        return path[index..];
    }

    public static string[] SplitPathParts(string path) => path.Split(
        new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar },
        StringSplitOptions.RemoveEmptyEntries);

    private static bool IsEightDotThreeName(string name) => name.Length == 12 && name[8] == '.';
    public static FileSystemNode FindParentFolder(FileSystemNode root, string[] pathParts, ErrorState errorState)
    {
        var node = root;
        for (var i = 0; i < pathParts.Length - 1; i++)
        {
            if (!node.IsProcessed) return null;
            var name = pathParts[i];
            if (!node.ContainsChildByName(name))
            {
                var path = Path.Combine(root.GetPath(), Path.Combine(pathParts));
                if (IsEightDotThreeName(name))
                { ErrorReportUtil.ReportError(errorState, new EightDotThreeNameReport(path)); }
                else ErrorReportUtil.ReportError(errorState, new ChangeEventOnUnaccessedPathReport(path));
                return null;
            }
            node = node.GetChildByName(name);
        }
        if (!node.IsProcessed) return null;
        return node;
    }
}
