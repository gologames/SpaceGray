using Moq;
using SpaceGray.Core.Error;
using SpaceGray.Core.Localization;
using SpaceGray.Test.Util;

namespace SpaceGray.Test.Error.State;

[TestClass]
public class ErrorRootNodeTest
{
    [TestMethod]
    public void CountChildren()
    {
        var errorState = new ErrorState();
        Assert.IsFalse(errorState.IsReady);
        var report = new Mock<IErrorReport>();
        report.SetupGet(report => report.Title).Returns("");
        report.SetupGet(report => report.Severity).Returns(ErrorSeverity.Info);
        errorState.ReportError(report.Object);
        Assert.IsTrue(errorState.IsReady);
        errorState.RemoveError(errorState.Root);
        Assert.IsFalse(errorState.IsReady);
        errorState.ReportError(report.Object);
        Assert.IsTrue(errorState.IsReady);
        errorState.RemoveError(errorState.Root.GetSortedChildren().Item1.First());
        Assert.IsFalse(errorState.IsReady);
    }
}
