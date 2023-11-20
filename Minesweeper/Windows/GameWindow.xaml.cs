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

        vm.RenderField(_fieldSize, GameField);
    }
}