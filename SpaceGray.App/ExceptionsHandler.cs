using System;
using System.Windows.Forms;

namespace SpaceGray.App;

public class ExceptionsHandler
{
    private static void Handler(object sender, UnhandledExceptionEventArgs e)
    {
        var exception = (Exception)e.ExceptionObject;
        MessageBox.Show(exception.StackTrace, exception.Message);
    }
    public static void HandleAllExceptions() =>
        AppDomain.CurrentDomain.UnhandledException += Handler;
}
