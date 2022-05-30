
namespace SpaceGray
{
    partial class SpaceGrayForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SpaceGrayForm
            // 
            this.Name = "SpaceGrayForm";
            this.Text = "SpaceGray";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SpaceGrayForm_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SpaceGrayForm_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SpaceGrayForm_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SpaceGrayForm_MouseDown);
            this.MouseLeave += new System.EventHandler(this.SpaceGrayForm_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SpaceGrayForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SpaceGrayForm_MouseUp);
            this.Resize += new System.EventHandler(this.SpaceGrayForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

