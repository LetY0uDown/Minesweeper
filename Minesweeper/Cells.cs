using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Minesweeper;

public static class Cells
{
    private readonly static List<Cell> _cells = [];

    private static int _fieldSize;

    public static Cell? GetCell (int row, int col)
    {
        return _cells.FirstOrDefault(c => c.Row == row && c.Col == col);
    }

    public static List<Cell?> GetNeighbors (int row, int col)
    {
        var up = _cells.FirstOrDefault(c => c.Row == row - 1 && c.Col == col);
        var right = _cells.FirstOrDefault(c => c.Row == row && c.Col == col - 1);
        var down = _cells.FirstOrDefault(c => c.Row == row + 1 && c.Col == col);
        var left = _cells.FirstOrDefault(c => c.Row == row && c.Col == col + 1);

        var upLeft = _cells.FirstOrDefault(c => c.Row == row - 1 && c.Col == col - 1);
        var upRight = _cells.FirstOrDefault(c => c.Row == row - 1 && c.Col == col + 1);
        var downLeft = _cells.FirstOrDefault(c => c.Row == row + 1 && c.Col == col - 1);
        var downRight = _cells.FirstOrDefault(c => c.Row == row + 1 && c.Col == col + 1);

        return [up, right, down, left, upLeft, upRight, downLeft, downRight];
    }

    public static void GenerateCells (int fieldSize, Grid playGrid, MouseButtonEventHandler checkCallback)
    {
        _fieldSize = fieldSize;

        for (int x = 0; x < _fieldSize; x++) {
            for (int y = 0; y < _fieldSize; y++) {
                var border = new Border {
                    Background = Brushes.Transparent,
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Blue
                };

                var isBomb = Random.Shared.Next(1, 101) > 90;

                var cell = new Cell(isBomb, y, x, border);
                _cells.Add(cell);

                playGrid.Children.Add(border);
                Grid.SetRow(border, y);
                Grid.SetColumn(border, x);

                border.MouseLeftButtonDown += checkCallback;
                border.MouseRightButtonDown += MarkBomb;
            };
        }
    }

    public static void CalculateBombsCounts ()
    {
        for (int x = 0; x < _fieldSize; x++) {
            for (int y = 0; y < _fieldSize; y++) {
                var cell = GetCell(y, x);

                var neighbors = GetNeighbors(y, x);
                var bombs = 0;

                neighbors.ForEach(neighbor => {
                    if (neighbor is not null && neighbor.IsBomb) {
                        bombs++;
                    }
                });

                if (cell is not null) {
                    cell.BombsNearby = bombs;
                }
            }
        }
    }

    private static void MarkBomb (object sender, MouseButtonEventArgs e)
    {
        var border = sender as Border;
        var row = Grid.GetRow(border);
        var col = Grid.GetColumn(border);

        var cell = GetCell(row, col);

        cell?.MarkAsBomb();
    }
}