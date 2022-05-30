using Moq;
using SpaceGray.Core.Error;
using SpaceGray.Core.Localization;
using SpaceGray.Test.Util;

namespace SpaceGray.Test.Error.Node;

[TestClass]
public class ErrorRootNodeTest
{
    [TestMethod]
    public void CountChildren()
    {
        var rootNode = new ErrorRootNode();
        Assert.IsTrue(rootNode.IsEmpty());
        var report1 = new Mock<IErrorReport>();
        report1.SetupGet(report => report.Title).Returns("1");
        report1.SetupGet(report => report.Severity).Returns(ErrorSeverity.Warning);
        rootNode.AddErrorItem(report1.Object);
        Assert.AreEqual(1, rootNode.GetSortedChildren().Item1.Count());
        rootNode.AddErrorItem(report1.Object);
        Assert.AreEqual(1, rootNode.GetSortedChildren().Item1.Count());
        var report2 = new Mock<IErrorReport>();
        report2.SetupGet(report => report.Title).Returns("2");
        report2.SetupGet(report => report.Severity).Returns(ErrorSeverity.Info);
        rootNode.AddErrorItem(report2.Object);
        Assert.AreEqual(2, rootNode.GetSortedChildren().Item1.Count());
        var groupNodesChildren = rootNode.GetSortedChildren().Item1;
        rootNode.RemoveErrorNode(groupNodesChildren.First().GetSortedChildren().Item1.First());
        Assert.AreEqual(2, rootNode.GetSortedChildren().Item1.Count());
        rootNode.RemoveErrorNode(rootNode.GetSortedChildren().Item1.First());
        Assert.AreEqual(1, rootNode.GetSortedChildren().Item1.Count());
        rootNode.RemoveErrorNode(rootNode.GetSortedChildren().Item1.First());
        Assert.IsTrue(rootNode.IsEmpty());
    }

    [TestMethod]
    public void GetSortedChildren()
    {
        var rootNode = new ErrorRootNode();
        var report1 = new Mock<IErrorReport>();
        report1.SetupGet(report => report.Title).Returns("2");
        report1.SetupGet(report => report.Severity).Returns(ErrorSeverity.Warning);
        rootNode.AddErrorItem(report1.Object);
        var report2 = new Mock<IErrorReport>();
        report2.SetupGet(report => report.Title).Returns("3");
        report2.SetupGet(report => report.Severity).Returns(ErrorSeverity.Info);
        rootNode.AddErrorItem(report2.Object);
        var report3 = new Mock<IErrorReport>();
        report3.SetupGet(report => report.Title).Returns("1");
        report3.SetupGet(report => report.Severity).Returns(ErrorSeverity.Error);
        rootNode.AddErrorItem(report3.Object);
        (var children, var size) = rootNode.GetSortedChildren();
        Assert.AreEqual(report3.Object.Title, children.First().Text);
        Assert.AreEqual(report1.Object.Title, children.ElementAt(1).Text);
        Assert.AreEqual(report2.Object.Title, children.Last().Text);
        var size1 = (long)report1.Object.Severity;
        var size2 = (long)report2.Object.Severity;
        var size3 = (long)report3.Object.Severity;
        Assert.AreEqual(size1 + size2 + size3, size);
    }

    [TestMethod]
    public void GetText()
    {
        var rootNode = new ErrorRootNode();
        var report1 = new Mock<IErrorReport>();
        report1.SetupGet(report => report.Title).Returns("2");
        report1.SetupGet(report => report.Severity).Returns(ErrorSeverity.Warning);
        rootNode.AddErrorItem(report1.Object);
        var report2 = new Mock<IErrorReport>();
        report2.SetupGet(report => report.Title).Returns("3");
        report2.SetupGet(report => report.Severity).Returns(ErrorSeverity.Info);
        rootNode.AddErrorItem(report2.Object);
        var report3 = new Mock<IErrorReport>();
        report3.SetupGet(report => report.Title).Returns("1");
        report3.SetupGet(report => report.Severity).Returns(ErrorSeverity.Error);
        rootNode.AddErrorItem(report3.Object);
        var errorText = ErrorLocalization.GetSeverityText(report3.Object.Severity, 1);
        var warningText = ErrorLocalization.GetSeverityText(report1.Object.Severity, 1);
        var infoText = ErrorLocalization.GetSeverityText(report2.Object.Severity, 1);
        var expected = $"{errorText}, {warningText}, {infoText}";
        Assert.AreEqual(expected, rootNode.Invoke<string>(nameof(GetText)));
    }
}
