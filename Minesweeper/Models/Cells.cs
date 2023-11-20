using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Minesweeper.Models;

public static class Cells
{
    private readonly static List<Cell> _cells = [];

    private static int _fieldSize;

    public static int BombsCount { get; set; } = 0;

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

    public static void RevealField()
    {
        _cells.ForEach(c => {
            if (!c.IsOpen) {
                c.Reveal();
            }
        });
    }

    public static void GenerateCells (int fieldSize, Grid playGrid,
                                      MouseButtonEventHandler checkCellEvent,
                                      MouseButtonEventHandler markBombEvent)
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

                if (isBomb) { BombsCount++; }

                var cell = new Cell(isBomb, y, x, border);
                _cells.Add(cell);

                playGrid.Children.Add(border);
                Grid.SetRow(border, y);
                Grid.SetColumn(border, x);

                border.MouseLeftButtonDown += checkCellEvent;
                border.MouseRightButtonDown += markBombEvent;
            };
        }
    }

    public static int CalculateBombsCounts ()
    {
        for (int x = 0; x < _fieldSize; x++) {
            for (int y = 0; y < _fieldSize; y++) {
                var cell = GetCell(y, x);

                var neighbors = GetNeighbors(y, x);
                var count = neighbors.Count(cell => cell is not null && cell.IsBomb);

                if (cell is not null) {
                    cell.BombsNearby = count;
                }
            }
        }

        return _cells.Count(cell => cell.IsBomb);
    }
}