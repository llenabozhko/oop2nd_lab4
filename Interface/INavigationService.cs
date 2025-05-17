
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4.Class;
using Lab4.DTO;
using Lab4.Interface;

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
