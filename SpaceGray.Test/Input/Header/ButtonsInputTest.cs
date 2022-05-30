using SpaceGray.Core.Input;
using SpaceGray.Core.UI;
using SpaceGray.Test.Util;
using System.Drawing;

namespace SpaceGray.Test.Input.Header;

[TestClass]
public class ButtonsInputTest
{
    private static void FindButton(int index)
    {
        var buttons = new HeaderButtons(new(new()));
        var buttonWidth = buttons.GetField<int>("buttonWidth");
        buttons.Resize(0.0f, 0.0f, buttonWidth * buttons.Count());
        var point = new Point(buttonWidth * index, 0);
        var actualButton = typeof(ButtonsInput).InvokeStatic<HeaderButton>(nameof(FindButton), buttons, point);
        var expectedButton = buttons.ElementAt(index);
        Assert.AreSame(expectedButton, actualButton);
    }
    [TestMethod]
    public void FindButtons()
    {
        const int visibleButtonsCount = 6;
        for (var i = 0; i < visibleButtonsCount; i++) FindButton(i);
    }

    [TestMethod]
    public void MouseDown()
    {
        var buttons = new HeaderButtons(new(new()));
        buttons.Resize(0.0f, 0.0f, 1.0f);
        Assert.IsFalse(buttons.IsAnyButtonClicked);
        var actualMouseDown = ButtonsInput.MouseDown(buttons, new());
        Assert.IsTrue(actualMouseDown);
        Assert.IsTrue(buttons.IsAnyButtonClicked);
        Assert.AreSame(buttons.First(), buttons.ClickedButton);
    }

    [TestMethod]
    public void Hover()
    {
        var buttons = new HeaderButtons(new(new()));
        buttons.Resize(0.0f, 0.0f, 1.0f);
        Assert.IsFalse(buttons.IsAnyButtonHovered);
        var actualHover = ButtonsInput.Hover(buttons, new());
        Assert.IsTrue(actualHover);
        Assert.IsTrue(buttons.IsAnyButtonHovered);
        Assert.AreSame(buttons.First(), buttons.HoveredButton);
    }

    [TestMethod]
    public void LeaveNoEffect()
    {
        var buttons = new HeaderButtons(new(new()));
        buttons.Resize(0.0f, 0.0f, 1.0f);
        var actualLeave = ButtonsInput.Leave(buttons);
        Assert.IsFalse(actualLeave);
        Assert.IsFalse(buttons.IsAnyButtonHovered);
    }
    [TestMethod]
    public void Leave()
    {
        var buttons = new HeaderButtons(new(new()));
        buttons.SetHoveredButton(buttons.First());
        var actualLeave = ButtonsInput.Leave(buttons);
        Assert.IsTrue(actualLeave);
        Assert.IsFalse(buttons.IsAnyButtonHovered);
    }

    [TestMethod]
    public void MouseUp()
    {
        var buttons = new HeaderButtons(new(new()));
        buttons.SetClickedButton(buttons.First());
        ButtonsInput.MouseUp(buttons);
        Assert.IsFalse(buttons.IsAnyButtonClicked);
    }
}
