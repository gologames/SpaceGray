using Moq;
using SpaceGray.Core.Error;

namespace SpaceGray.Test.Error.Node;

[TestClass]
public class ErrorGroupNodeTest
{
    [TestMethod]
    public void CountChildren()
    {
        var report = new Mock<IErrorReport>();
        report.SetupGet(report => report.Title).Returns("");
        report.SetupGet(report => report.Severity).Returns(ErrorSeverity.Info);
        var groupNode = new ErrorGroupNode(null, report.Object);
        Assert.AreEqual(0, groupNode.GetChildrenCount());
        groupNode.AddErrorItem(report.Object);
        Assert.AreEqual(1, groupNode.GetChildrenCount());
        groupNode.AddErrorItem(report.Object);
        Assert.AreEqual(2, groupNode.GetChildrenCount());
        (var children, _) = groupNode.GetSortedChildren();
        groupNode.RemoveErrorItem(children.First() as ErrorItemNode);
        Assert.AreEqual(1, groupNode.GetChildrenCount());
        groupNode.RemoveErrorItem(children.First() as ErrorItemNode);
        Assert.AreEqual(0, groupNode.GetChildrenCount());
    }
}
