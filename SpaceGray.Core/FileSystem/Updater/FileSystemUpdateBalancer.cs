namespace SpaceGray.Core.FileSystem;

public class FileSystemUpdateBalancer
{
    private const double ChangeCoef = 1.05;
    
    public static bool IsReadyForUpdateSize(long totalSize, long rootSize) =>
        totalSize / (double)rootSize > ChangeCoef;

    public static bool IsReadyForUpdateCount(long totalCount, long rootCount) =>
        totalCount / (double)rootCount > ChangeCoef;
}
