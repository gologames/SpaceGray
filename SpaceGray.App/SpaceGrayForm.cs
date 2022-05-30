using SpaceGray.Core;
using SpaceGray.Core.Input;
using SpaceGray.Core.TreeMap;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceGray
{
    public partial class SpaceGrayForm : Form
    {
        private readonly SpaceGrayApplication application;

        public SpaceGrayForm()
        {
            application = new(this);
            InitializeComponent();
            DoubleBuffered = true;
            ResizeRedraw = true;
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void SpaceGrayForm_Resize(object sender, EventArgs e)
        {
            application.UIState.Resize();
            TreeMapLayoutUpdater.UpdateLayout(application);
        }

        private void SpaceGrayForm_Paint(object sender, PaintEventArgs e) =>
            application.UIState.Render(e.Graphics);

        private void SpaceGrayForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            { SpaceGrayInput.MouseDown(application, e.Location, e.Button == MouseButtons.Right); }
        }

        private void SpaceGrayForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) SpaceGrayInput.LeftClick(application, e.Location);
            else if (e.Button == MouseButtons.Right) SpaceGrayInput.RightClick(application, e.Location);
        }

        private void SpaceGrayForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) SpaceGrayInput.DoubleClick(application, e.Location);
            else if (e.Button == MouseButtons.Right) SpaceGrayInput.RightClick(application, e.Location);
        }

        private void SpaceGrayForm_MouseMove(object sender, MouseEventArgs e) =>
            SpaceGrayInput.Hover(application, e.Location);

        private void SpaceGrayForm_MouseLeave(object sender, EventArgs e) =>
            SpaceGrayInput.Leave(application);

        private void SpaceGrayForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            { SpaceGrayInput.MouseUp(application); }
        }
    }
}
