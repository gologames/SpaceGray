using System;
using System.Windows.Forms;

namespace SpaceGray.App;

static class Program
{
    [STAThread]
    static void Main()
    {
        ExceptionsHandler.HandleAllExceptions();
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new SpaceGrayForm());
    }
}
