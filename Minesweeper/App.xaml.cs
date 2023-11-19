using System.Windows;

namespace Minesweeper;

public partial class App : Application
{
    private void ApplicationStartup (object sender, StartupEventArgs e)
    {
        var window = new GameWindow (20);
        MainWindow = window;
        MainWindow.Show ();
    }
}