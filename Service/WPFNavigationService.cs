using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using Lab4.Class;
using Lab4.DTO;
using Lab4.Interface;
using Lab4.Forms;
using Lab4.ViewModel;

namespace Lab4.Service
{
    public class WPFNavigationService:INavigationService
    {

        public void OpenAddPerformanceForm(Action<Performance> OnSuccess, Action<string> ShowMessage)
        {
            var window = new PerfomanceForm();
            if (window.ShowDialog() == true)
            {
                OnSuccess?.Invoke(window.Result);
                ShowMessage?.Invoke("Performance додано: " + window.Result.Title);
            }
        }
        public void OpenEditPerformanceForm(Performance selectedPerformance, Action<Performance> Updated, Action<string> ShowMessage)
        {
            if (selectedPerformance == null)
            {
                ShowMessage?.Invoke("Please select a performance");
                return;
            }

            var window = new PerfomanceForm(selectedPerformance);
            if (window.ShowDialog() == true)
            {
                var updated = window.Result;
                Updated?.Invoke(updated);
            }
        }
        public void OpenAddConcertForm(Action<Concert> OnSucces, Action<string> ShowMessage)
        {
            var window = new ConcertForm();
            if (window.ShowDialog() == true)
            {
                OnSucces?.Invoke(window.Result);
                ShowMessage?.Invoke($"Concert created: {window.Result.Organizer}");
            }
        }
        public void OpenEditConcertForm(Concert selectedConcert,Action<Concert> Updated,Action<string> ShowMessage)
        {
            if (selectedConcert == null)
            {
                ShowMessage?.Invoke("Please select a concert");
                return;
            }

            var window = new ConcertForm(selectedConcert);
            if (window.ShowDialog() == true)
            {
                var updated = window.Result;
                Updated?.Invoke(updated);
            }
        }
    }

}
