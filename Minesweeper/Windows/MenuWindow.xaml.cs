using System.Windows;

namespace Minesweeper.Windows;

public partial class MenuWindow : Window
{
    public MenuWindow ()
    {
        InitializeComponent();
    }

    private void ExitButtonClick (object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown ();
    }
}