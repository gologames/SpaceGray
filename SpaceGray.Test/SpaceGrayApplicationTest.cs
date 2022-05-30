using SpaceGray.Core;

namespace SpaceGray.Test;

[TestClass]
public class SpaceGrayApplicationTest
{
    [TestMethod]
    public void Initialization()
    {
        var application = new SpaceGrayApplication(new());
        Assert.IsNotNull(application.UIState);
        Assert.IsNotNull(application.FileSystemState);
        Assert.IsNotNull(application.FileSystemLayout);
        Assert.IsNotNull(application.ErrorState);
        Assert.IsNotNull(application.ErrorLayout);
    }
}
