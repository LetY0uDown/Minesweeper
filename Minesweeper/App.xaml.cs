using Minesweeper.Windows;
using System.Windows;

namespace Minesweeper;

public partial class App : Application
{
    private void ApplicationStartup (object sender, StartupEventArgs e)
    {
        var window = new MenuWindow();
        window.Show ();
    }

    public static void ChangeMainWindow(Window window)
    {
        Current.MainWindow?.Hide ();

        Current.MainWindow = window;
        Current.MainWindow?.Show ();
    }
}