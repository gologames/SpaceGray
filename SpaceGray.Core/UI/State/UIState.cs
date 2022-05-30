using SpaceGray.Core.FileSystem;
using SpaceGray.Core.Localization;
using SpaceGray.Core.Render;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceGray.Core.UI;

public class UIState
{
    private const int DefaultFormWidth = 800;
    private const int DefaultFormHeight = 450;
    private readonly SpaceGrayApplication application;
    private string formText;
    private readonly Form form;
    public ResolutionScaler Scaler { get; }
    public IBufferedGraphics Graphics { get; private set; }
    public Header Header { get; private set; }
    public bool IsColored { get; private set; }
    public TabsController Tabs { get; }
    public RectangleF ContentRect { get; private set; }

    public UIState(SpaceGrayApplication application, Form form)
    {
        this.application = application;
        formText = null;
        this.form = form;
        Scaler = new(form);
        Graphics = new BufferedGraphics(Scaler);
        Tabs = new();
        IsColored = false;
        form.ClientSize = new(Scaler.Scale(DefaultFormWidth), Scaler.Scale(DefaultFormHeight));
        form.StartPosition = FormStartPosition.Manual;
        form.Left = (Scaler.ScreenWidth - form.Width) / 2;
        form.Top = (Scaler.ScreenHeight - form.Height) / 2;
    }
    public void InitHeader() => Header = new Header(application);

    public void SetFormText(string path, bool isDone) =>
        formText = path + (isDone ? "" : $" - {Resources.FormTextProcessing}");
    private void UpdateFormText()
    {
        if (formText != null)
        {
            form.Text = formText;
            formText = null;
        }
    }

    public void ToggleColored() => IsColored = !IsColored;

    public void Resize()
    {
        var borderWidth = RenderGroupUtil.GetUnscaledBorderWidth();
        Header.Resize(0.0f, 0.0f, form.ClientSize.Width - borderWidth);
        var headerHeight = Header.Rect.Height;
        ContentRect = new(0.0f, headerHeight,
            form.ClientSize.Width - borderWidth,
            form.ClientSize.Height - headerHeight - borderWidth);
    }

    public static bool OpenFolder(FileSystemState fileSystemState)
    {
        var folderDialog = new FolderBrowserDialog();
        var isOpened = folderDialog.ShowDialog() == DialogResult.OK;
        if (isOpened)
        {
            lock (fileSystemState.Locker)
            { fileSystemState.SetRootPath(folderDialog.SelectedPath); }
        }
        return isOpened;
    }

    public void Render(Graphics graphics)
    {
        UpdateFormText();
        Graphics.Clear();
        SpaceGrayRenderer.Draw(application);
        Graphics.Render(graphics);
    }
    public void Invalidate() => form.Invoke((MethodInvoker)form.Invalidate);
    public void Refresh() => form.Invoke((MethodInvoker)form.Refresh);

    public void Reset()
    {
        Header.Reset();
        Tabs.Reset();
    }
}
