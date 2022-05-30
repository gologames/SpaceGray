using SpaceGray.Core.UI;
using System.Drawing;

namespace SpaceGray.Core.Render;

public static class RenderGroupUtil
{
    private const float GroupHeaderHeightCoef = 2.0f;
    private const int GroupHeaderTextPadding = 1;
    private const float DefaultBorderWidth = 1.0f;

    public static int GetGroupHeaderTextPadding(ResolutionScaler scaler) => scaler.Scale(GroupHeaderTextPadding);

    public static float GetGroupHeaderHeight(ResolutionScaler scaler, float oneSymbolHeight) =>
        oneSymbolHeight + 2 * GetGroupHeaderTextPadding(scaler);
    
    public static bool IsGroupExpandable(ResolutionScaler scaler, RectangleF rect, float oneSymbolHeight) =>
        rect.Height >= GroupHeaderHeightCoef * GetGroupHeaderHeight(scaler, oneSymbolHeight);

    public static float GetUnscaledBorderWidth() => DefaultBorderWidth;
    public static float GetBorderWidth(ResolutionScaler scaler) => scaler.Scale(DefaultBorderWidth);
}
