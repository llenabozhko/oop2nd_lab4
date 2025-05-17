using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

using Lab4.Class;
using Lab4.DTO;

namespace Lab4.Class
{
    public class Concert : INotifyPropertyChanged
    {
        private string organizer;

        public string Organizer
        {
            get => organizer;
            set
            {
                organizer = value;
                OnPropertyChanged();

            }
        }

        private DateTime date;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();

            }
        }
        public ObservableCollection<Performance> Performances { get; set; } = new();

        public Concert(string organizer, DateTime date)
        {
            this.organizer = organizer;
            this.date = date;
        }

        public override string ToString() => $"{Organizer} — {Date.ToShortDateString()}";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ConcertDTO ToDTO() => new ConcertDTO()
        {
            Organizer = organizer,
            Date = date,
            Performances = Performances.Select(p => p.ToDTO()).ToList()
        };
        public static Concert FromDTO(ConcertDTO dto)
        {
            var concert = new Concert(dto.Organizer, dto.Date);
            foreach (var perfDTO in dto.Performances)
            {
                concert.Performances.Add(Performance.FromDTO(perfDTO));
            }
            return concert;
        }

    }
    public enum Work
    {
        Instrumental,
        Vocal,
        Poetic,
        Prose
    }
}
