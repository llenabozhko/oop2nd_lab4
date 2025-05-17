namespace Lab4.Interface
{
    public interface IRelayCommand
    {
        bool CanExecute(object parameter);
        void Execute(object parameter);
        event EventHandler CanExecuteChanged;
        void RaiseCanExecuteChanged();
    }
    public delegate IRelayCommand CommandFactory(Action<object> execute,
                                               Predicate<object> canExecute = null);
}
