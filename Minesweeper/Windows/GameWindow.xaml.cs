using Minesweeper.Tools;
using Minesweeper.ViewModels;
using System.Windows;

namespace Minesweeper.Windows;

public partial class GameWindow : Window
{
    private readonly int _fieldSize;

    public GameWindow (int fieldSize)
    {
        InitializeComponent();

        _fieldSize = fieldSize;
    }

    protected override void OnContentRendered (EventArgs e)
    {
        var vm = DataContext as GameViewModel;

        vm!.GameOverEvent += (string msg) => {
            Animator.AnimateBlur(GameField, 0, 5, TimeSpan.FromMilliseconds(500));
            GameOverTextBlock.Text = msg;
        };

        vm!.RenderField(_fieldSize, GameField);
    }

    private void Grid_MouseLeftButtonDown (object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void ExitButtonClick (object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}