
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lab4.Class;
using Lab4.DTO;
using Lab4.Interface;

namespace Lab4.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Concert> Concerts { get; set; } = new();
        const string FilePath = "concerts.json";
        public IRelayCommand AddConcertCommand { get; }
        public IRelayCommand EditConcertCommand { get; }
        public IRelayCommand LoadConcertsCommand { get; }
        public IRelayCommand SaveConcertsCommand { get; }

        public Action<string> ShowMessage { get; set; }
        private INavigationService navigationService;

        private Concert selectedConcert;
        public Concert SelectedConcert
        {
            get
            {
                return this.selectedConcert;
            }
            set
            {
                this.selectedConcert = value;
            }
        }
        public MainViewModel(CommandFactory createCommand, INavigationService navigationService)
        {
            this.navigationService=navigationService;

            LoadConcertsCommand = createCommand(_ => LoadConcertsFromFile());

            AddConcertCommand = createCommand(_ =>
            {
                var show = ShowMessage ?? (_ => { });
                navigationService.OpenAddConcertForm(concert =>
                {
                    Concerts.Add(concert);
                    OnPropertyChanged(nameof(Concerts));
                }, show);
            });
            EditConcertCommand = createCommand(

                _ => navigationService.OpenEditConcertForm(
                SelectedConcert, updated =>
                {
                    SelectedConcert.Organizer = updated.Organizer;
                    SelectedConcert.Performances = updated.Performances;
                    SelectedConcert.Date = updated.Date;
                }, ShowMessage ?? (_ => { })));
            SaveConcertsCommand = createCommand(_ => SaveConcertsToFile());
            LoadConcertsFromFile();
        }

        public void LoadConcertsFromFile()
        {
            if (!File.Exists(FilePath)) return;

            string json = File.ReadAllText(FilePath);
            var dtoList = JsonSerializer.Deserialize<List<ConcertDTO>>(json);
            if (dtoList == null) return;

            Concerts.Clear();
            foreach (var dto in dtoList)
            {
                Concerts.Add(Concert.FromDTO(dto));
            }
        }

        public void SaveConcertsToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var dtoList = Concerts.Select(c => c.ToDTO()).ToList();
            string json = JsonSerializer.Serialize(dtoList, options);
            File.WriteAllText(FilePath, json);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
