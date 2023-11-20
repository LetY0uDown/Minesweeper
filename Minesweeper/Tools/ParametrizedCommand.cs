using System.Windows.Input;

namespace Minesweeper.Tools;

public sealed class ParametrizedCommand<T> : ICommand where T : class
{
    private readonly Action<T?> _execute = null!;
    private readonly Func<T?, bool>? _canExecute = null;

    public ParametrizedCommand (Action<T?> execute, Func<T?, bool> canExecute) : this (execute)
    {
        _canExecute = canExecute;
    }

    public ParametrizedCommand (Action<T?> execute)
    {
        _execute = execute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute (object? parameter)
    {
        return _canExecute is null || _canExecute (parameter as T);
    }

    public void Execute (object? parameter)
    {
        _execute (parameter as T);
    }
}