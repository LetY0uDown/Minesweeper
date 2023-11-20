using FontAwesome.WPF;
using Minesweeper.Tools;
using System.Windows.Controls;
using System.Windows.Media;

namespace Minesweeper.Models;

public class Cell
{
    private readonly Border _border;
    private readonly TimeSpan _animDuration = TimeSpan.FromMilliseconds(250);

    public Cell (bool isBomb, int row, int col, Border border)
    {
        IsOpen = false;
        IsBomb = isBomb;

        Row = row;
        Col = col;
        _border = border;
    }

    public bool IsBomb { get; private init; }
    public bool IsOpen { get; private set; }

    public int Row { get; private set; }
    public int Col { get; private set; }

    public int BombsNearby { get; set; }

    public bool MarkedAsBomb { get; private set; }

    public void Reveal()
    {
        IsOpen = true;
        if (IsBomb) {
            Animator.AnimateBackground(_border, _border.Background, Brushes.Red,
                                       _animDuration);
            return;
        }

        Animator.AnimateBackground(_border, _border.Background, Brushes.SteelBlue,
                                   _animDuration);

        if (BombsNearby > 0) {
            DisplayBombs();
        }
    }

    public void Open ()
    {
        if (IsOpen || MarkedAsBomb || IsBomb) { return; }

        IsOpen = true;
        
        Animator.AnimateBackground(_border, _border.Background, Brushes.SteelBlue,
                                   _animDuration);

        if (BombsNearby > 0) {
            DisplayBombs();

            return;
        }

        var neighbors = Cells.GetNeighbors(Row, Col);

        foreach (var neighbor in neighbors) {
            neighbor?.Open();
        }
    }

    void DisplayBombs ()
    {
        var box = new Viewbox {
            Margin = new(0)
        };

        _border.Child = box;

        box.Child = new TextBlock {
            Text = BombsNearby.ToString(),
            Foreground = Brushes.Black,
            FontSize = 12
        };
    }

    public CheckResult Check ()
    {
        if (IsOpen) { return CheckResult.Ok; }

        IsOpen = true;
        
        if (IsBomb) {
            Animator.AnimateBackground(_border, _border.Background, Brushes.Red,
                                       _animDuration);
            return CheckResult.Bomb;
        }

        Animator.AnimateBackground(_border, _border.Background, Brushes.SteelBlue,
                                   _animDuration);
        _border.Child = null;

        if (BombsNearby > 0) {
            DisplayBombs();

            return CheckResult.Ok;
        }

        var neighbors = Cells.GetNeighbors(Row, Col);

        foreach (var neighbor in neighbors) {
            neighbor?.Open();
        }

        return CheckResult.Ok;
    }

    public void MarkAsBomb ()
    {
        if (IsOpen) { return; }

        MarkedAsBomb = true;

        Animator.AnimateBackground(_border, _border.Background, Brushes.Pink,
                                   _animDuration);
        
        var box = new Viewbox { Margin = new(5) };

        _border.Child = box;
        box.Child = new Label {
            Content = new ImageAwesome {
                Icon = FontAwesomeIcon.Bomb,
                Foreground = Brushes.DarkRed
            }
        };
    }
}