using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Minesweeper;

public partial class GameWindow : Window
{
    private readonly int _fieldSize;

    private static bool _canPlay;

    public GameWindow (int fieldSize)
    {
        InitializeComponent();

        _fieldSize = fieldSize;
        Width = _fieldSize * CELL_SIZE;
        Height = _fieldSize * CELL_SIZE + 100;
    }

    public const int CELL_SIZE = 50;

    protected override void OnContentRendered (EventArgs e)
    {
        var playGrid = new Grid();
        GameField.Child = playGrid;

        for (int i = 0; i < _fieldSize; i++) {
            playGrid.RowDefinitions.Add(new RowDefinition());
            playGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        Cells.GenerateCells(_fieldSize, playGrid, CheckCell);
        Cells.CalculateBombsCounts();

        _canPlay = true;
    }

    private static void CheckCell (object sender, MouseButtonEventArgs e)
    {
     //   if (!_canPlay) { return; }

        var border = sender as Border;
        var row = Grid.GetRow(border);
        var col = Grid.GetColumn(border);

        var cell = Cells.GetCell(row, col);

        var res = cell?.Check();

        if (res is not null and CheckResult.Bomb) {
            _canPlay = false;
        }
    }
}