using System.Windows.Input;
using Lab4.Interface;

namespace Lab4.Service
{
  public class RelayCommand : ICommand, IRelayCommand
  {
    private readonly Action<object> _execute;
    private readonly Predicate<object> _canExecute;
    public RelayCommand(Action<object> exec, Predicate<object> can = null)
    {
      _execute = exec;
      _canExecute = can;
    }
    public bool CanExecute(object p) => _canExecute == null || _canExecute(p);
    public void Execute(object p) => _execute(p);
    public event EventHandler CanExecuteChanged
    {
      add => CommandManager.RequerySuggested += value;
      remove => CommandManager.RequerySuggested -= value;
    }
    public void RaiseCanExecuteChanged()
      => CommandManager.InvalidateRequerySuggested();
  }
}
