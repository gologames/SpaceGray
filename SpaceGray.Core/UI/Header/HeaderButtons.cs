using SpaceGray.Core.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpaceGray.Core.UI;

public class HeaderButtons : IEnumerable<HeaderButton>
{
    private const int ButtonTextPadding = 9;
    private readonly int symbolWidth;
    private readonly int buttonWidth;
    private readonly int buttonHeight;
    private readonly List<HeaderButton> buttons;
    public RectangleF Rect { get; private set; }
    public bool IsAnyButtonHovered => HoveredButton != null;
    public bool IsAnyButtonClicked => ClickedButton != null;
    public HeaderButton HoveredButton { get; private set; }
    public HeaderButton ClickedButton { get; private set; }

    public HeaderButtons(SpaceGrayApplication application)
    {
        buttons = new()
        {
            new OpenButton(application),
            new LessButton(application),
            new MoreButton(application),
            new ColorButton(application),
            new MarkButton(application),
            new InfoButton(application),
            new ErrorButton(application)
        };

        var graphics = application.UIState.Graphics;
        foreach (var text in ButtonsLocalization.GetAllButtonsTexts())
        {
            var symbolBounds = graphics.MeasureHeaderText(text[..1]);
            symbolWidth = Math.Max(symbolWidth, symbolBounds.Width);
            var buttonBounds = graphics.MeasureHeaderText(text);
            buttonWidth = Math.Max(buttonWidth, buttonBounds.Width);
            buttonHeight = Math.Max(buttonHeight, buttonBounds.Height);
        }
        var textPadding = application.UIState.Scaler.Scale(ButtonTextPadding);
        symbolWidth += textPadding;
        buttonWidth += textPadding;
    }

    private bool IsButtonWide(int countVisible, int index, float fullWidth, float x) =>
        buttonWidth + (countVisible - index - 1) * symbolWidth <= fullWidth - x;
    public void Resize(float x, float y, float fullWidth)
    {
        var countVisile = buttons.Count(buttons => buttons.IsVisible);
        float currX = x;
        for (var i = 0; i < buttons.Count; i++)
        {
            if (!buttons[i].IsVisible) continue;
            var currentButtonWidth = buttonWidth;
            buttons[i].IsWide = IsButtonWide(countVisile, i, fullWidth, currX);
            if (!buttons[i].IsWide) currentButtonWidth = symbolWidth;
            buttons[i].Rect = new(currX, Rect.Y, currentButtonWidth, buttonHeight);
            currX += currentButtonWidth;
        }
        Rect = new(x, y, currX, buttonHeight);
    }

    public void SetHoveredButton(HeaderButton hoveredButton) => HoveredButton = hoveredButton;
    public void ClearHoveredButton() => HoveredButton = null;
    public void SetClickedButton(HeaderButton clickedButton) => ClickedButton = clickedButton;
    public void ClearClickedButton() => ClickedButton = null;

    public IEnumerator<HeaderButton> GetEnumerator() => buttons.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
