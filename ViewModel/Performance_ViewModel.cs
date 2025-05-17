
using System;
using System.Collections.Generic;
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
    public class PerformanceViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private Performance performance;
        private INavigationService navigationService;
        public IEnumerable<Work> WorkTypes => Enum.GetValues(typeof(Work)).Cast<Work>();

        public Work The_Work { get; set; }

        public string PerformerFullName { get; set; }

        public int Duration { get; set; }

        public string Title { get; set; }

        public IRelayCommand OkCommand { get; set; }

        public Action<Performance> OnSuccess { get; set; }
        public Action<string> ShowMessage { get; set; }

        public PerformanceViewModel(CommandFactory createCommand, Performance? perf = null)
        {
            ShowMessage = _ => { };

            if (perf != null)
            {
                // 1) Підтягуємо поля у властивості для біндінгу
                The_Work = perf.The_Work;
                PerformerFullName = perf.PerformerFullName;
                Duration = perf.Duration;
                Title = perf.Title;

                // 2) Клонуємо внутрішню модель, щоб не мутувати оригінал
                performance = new Performance(
                    perf.The_Work,
                    perf.The_Performer,
                    perf.Duration,
                    perf.Title
                );
            }
            else
            {
                // Новий порожній об’єкт
                performance = new Performance(
                    Work.Instrumental,
                    new Performer("", ""),
                    0,
                    ""
                );
                The_Work = performance.The_Work;
                PerformerFullName = "";
                Duration = 0;
                Title = "";
            }

            // 3) Створюємо команду через фабрику
            OkCommand = createCommand(
                execute: _ =>
                {
                    if (!IsValid())
                    {
                        ShowMessage?.Invoke("Please fix all input errors before continuing.");
                        return;
                    }
                    OnSuccess?.Invoke(BuildPerformance());
                },
                canExecute: _ => true
            );
        }



        public string this[string name_field]
        {
            get
            {
                return name_field switch
                {
                    nameof(PerformerFullName) => (string.IsNullOrWhiteSpace(PerformerFullName)
                        || PerformerFullName.Trim().Split(' ').Length < 2)
                        ? "Input full name of the performer"
                        : null,
                    nameof(Duration) => Duration <= 0
                        ? "Input valid the performance duration"
                        : null,

                    nameof(Title) => string.IsNullOrWhiteSpace(Title)
                        ? "Input title of the performance"
                        : null
                };
            }
        }
        public string Error => null;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public Performance BuildPerformance()
        {
            string[] parts = PerformerFullName?.Split(' ', 2) ?? new[] { "", "" };
            var performer = new Performer(parts[0], parts.Length > 1 ? parts[1] : "");
            return new Performance(The_Work, performer, Duration, Title);
        }

        public bool IsValid()
        {
            return string.IsNullOrEmpty(this[nameof(PerformerFullName)])
                   && string.IsNullOrEmpty(this[nameof(Duration)])
                   && string.IsNullOrEmpty(this[nameof(Title)]);
        }
        public void SetNavigationService(INavigationService service)
        {
            navigationService = service;

        }
    }
}
