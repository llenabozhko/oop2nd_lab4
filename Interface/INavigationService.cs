using Lab4.Class;

namespace Lab4.Interface
{
    public interface INavigationService
    {
        void OpenAddPerformanceForm(Action<Performance> OnSuccess, Action<string> ShowMessage);
        void OpenEditPerformanceForm(Performance performance, Action<Performance> Updated, Action<string> ShowMessage);

        void OpenAddConcertForm(Action<Concert> OnSuccess, Action<string> ShowMessage);
        void OpenEditConcertForm(Concert selectedConcert, Action<Concert> Updated, Action<string> ShowMessage);

    }
}
