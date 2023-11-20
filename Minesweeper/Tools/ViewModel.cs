using System.ComponentModel;

namespace Minesweeper.Tools;

public abstract class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
}