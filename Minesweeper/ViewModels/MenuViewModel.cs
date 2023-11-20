using Minesweeper.Tools;
using Minesweeper.Windows;

namespace Minesweeper.ViewModels;

public sealed class MenuViewModel : ViewModel
{
    public MenuViewModel()
    {
        PlayCommand = new(() => {
            App.ChangeMainWindow(new GameWindow(FieldSize));
        });        
    }

    public UICommand PlayCommand { get; private init; }

    public int FieldSize { get; set; } = 5;
}