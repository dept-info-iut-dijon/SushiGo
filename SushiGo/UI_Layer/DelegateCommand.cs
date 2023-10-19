using System;
using System.Windows.Input;

public class DelegateCommand : ICommand
{
    private readonly Action execute;
    private readonly Func<bool>? canExecute;

    public DelegateCommand(Action execute, Func<bool>? canExecute = null)
    {
        this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        this.canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return this.canExecute == null || this.canExecute();
    }

    public void Execute(object parameter)
    {
        this.execute();
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
