using System.Drawing;
using System.Windows.Forms;

namespace SpaceGray.Core.UI
{
    public interface IBufferedGraphics
    {
        int OneSymbolWidth { get; }
        int OneSymbolHeight { get; }

        Size MeasureNodeText(string text);
        Size MeasureHeaderText(string text);

        void FillHatchedRectangle(string foreColorHex, string backColorGex, RectangleF rect);
        void FillHatchedRectangle(string foreColorHex, string backColorGex, float x, float y, float width, float height);

        void FillRectangle(string colorHex, RectangleF rect);
        void FillRectangle(string colorHex, float x, float y, float width, float height);

        void DrawFileRectangle(string colorHex, RectangleF rect, bool isHatched = false);
        void DrawFolderRectangle(string colorHex, RectangleF rect, bool isHatched = false);
        void DrawHeaderRectangle(string colorHex, RectangleF rect);

        void DrawLine(string colorHex, float x1, float y1, float x2, float y2);

        void DrawNodeText(string text, Rectangle rect, string colorHex, TextFormatFlags formatFlags);
        void DrawHeaderText(string text, Rectangle rect, string colorHex, TextFormatFlags formatFlags);

        void Render(Graphics graphics);
        void Clear();
    }
}