using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lab4.Class;
using Lab4.DTO;
using Lab4.Interface;

namespace Lab4.ViewModel
{
    public class ConcertViewModel : INotifyPropertyChanged
    {
        private Concert concert;
        private INavigationService navigationService;

        public IRelayCommand OkCommand { get; set; }
        public IRelayCommand AddPerformanceCommand { get; set; }
        public IRelayCommand EditPerformanceCommand { get; set; }


        public Action<Concert> OnSuccess { get; set; }
        public Action<Performance> OnAddPerformance { get; set; }
        public Action<Performance> OnEditPerformance { get; set; }
        public Action<string> ShowMessage { get; set; }


        private Performance selectedPerformance;
        public Performance SelectedPerformance
        {
            get => selectedPerformance;
            set
            {
                selectedPerformance = value;
                OnPropertyChanged();
            }
        }

        public ConcertViewModel(CommandFactory createCommand,Concert? existingConcert = null)
        {
            concert = existingConcert ?? new Concert("Default", DateTime.Today);

            ShowMessage = _ => { };
            OkCommand = createCommand(
                execute: _ =>
                {
                    OnSuccess?.Invoke(BuildConcert());
                }
            );
            AddPerformanceCommand = createCommand(
                execute: _ =>
                {

                    navigationService?.OpenAddPerformanceForm(performance =>
                    {
                        AddPerformance(performance);
                    }
                    , ShowMessage);
                }
            );
            EditPerformanceCommand = createCommand(
                execute: _ =>
                {
                    navigationService.OpenEditPerformanceForm(
                    SelectedPerformance,
                    updated =>
                    {
                        SelectedPerformance.The_Work = updated.The_Work;
                        SelectedPerformance.The_Performer = updated.The_Performer;
                        SelectedPerformance.Title = updated.Title;
                        SelectedPerformance.Duration = updated.Duration;
                    },
                    ShowMessage
                    );
                }
            );

        }
        public void SetNavigationService(INavigationService service)
        {
            navigationService = service;

        }

        public string Organizer
        {
            get => concert.Organizer;
            set
            {
                concert.Organizer = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => concert.Date;
            set
            {
                concert.Date = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Performance> Performances => concert.Performances;

        public void AddPerformance(Performance perf)
        {
            Performances.Add(perf);
            OnPropertyChanged(nameof(Performances));
        }


        public Concert BuildConcert() => concert;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }





    }
}
