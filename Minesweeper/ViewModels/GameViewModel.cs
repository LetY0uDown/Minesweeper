using Minesweeper.Models;
using Minesweeper.Tools;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Minesweeper.ViewModels;

public sealed class GameViewModel : ViewModel
{
    private TimeSpan _time = TimeSpan.Zero;

    private readonly DispatcherTimer _timer;

    private readonly TimeSpan _interval = TimeSpan.FromMilliseconds(100);

    public GameViewModel ()
    {
        _timer = new(DispatcherPriority.Background) {
            Interval = _interval
        };

        _timer.Tick += TimerTick;
    }

    public int BombsLeft { get; private set; }
    
    public string Time { get; private set; } = null!;

    private void TimerTick (object? sender, EventArgs e)
    {
        Time = $"Time: {_time:mm':'ss'.'f}";
        _time += _interval;
    }

    public void RenderField(int fieldSize, Border gameField)
    {
        var playGrid = new Grid();
        gameField.Child = playGrid;

        for (int i = 0; i < fieldSize; i++) {
            playGrid.RowDefinitions.Add(new RowDefinition());
            playGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        Cells.GenerateCells(fieldSize, playGrid, CheckCell, MarkBomb);
        BombsLeft = Cells.CalculateBombsCounts();

        _timer.Start();
    }

    private void MarkBomb (object sender, MouseButtonEventArgs e)
    {
        var border = sender as Border;
        var row = Grid.GetRow(border);
        var col = Grid.GetColumn(border);

        var cell = Cells.GetCell(row, col);

        if (cell is not null) {
            cell.MarkAsBomb();
            BombsLeft--;
        }
    }

    private void CheckCell (object sender, MouseButtonEventArgs e)
    {
        var border = sender as Border;
        var row = Grid.GetRow(border);
        var col = Grid.GetColumn(border);

        var cell = Cells.GetCell(row, col);

        var res = cell?.Check();

        if (res is not null and CheckResult.Bomb) {
            Cells.RevealField();
        }
    }
}