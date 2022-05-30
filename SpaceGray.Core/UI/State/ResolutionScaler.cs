using System.Windows.Forms;

namespace SpaceGray.Core.UI;

public class ResolutionScaler
{
    private const int DefaultScreenWidth = 1920;
    private const int DefaultScreenHeight = 1080;
    private float coef;
    public int ScreenWidth { get; private set; }
    public int ScreenHeight { get; private set; }

    public ResolutionScaler(Form form)
    {
        var screen = Screen.FromControl(form);
        Init(screen.Bounds.Width, screen.Bounds.Height);
    }
    public ResolutionScaler(int screenWidth, int screenHeight) =>
        Init(screenWidth, screenHeight);
    private void Init(int screenWidth, int screenHeight)
    {
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
        var widthCoef = (float)screenWidth / DefaultScreenWidth;
        var heightCoef = (float)screenHeight / DefaultScreenHeight;
        coef = (widthCoef + heightCoef) / 2.0f;
    }

    public int Scale(int value) => (int)(coef * value);
    public float Scale(float value) => coef * value;
}
