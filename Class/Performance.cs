
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

namespace Lab4.Class
{
    public class Performance : INotifyPropertyChanged
    {
        private Work the_work;

        public Work The_Work
        {
            get => the_work;
            set
            {
                the_work = value;
                OnPropertyChanged();
            }
        }
        private Performer the_performer;

        public Performer The_Performer
        {
            get => the_performer;
            set
            {
                the_performer = value;
                OnPropertyChanged();
            }
        }

        public string PerformerFullName
        {
            get => $"{The_Performer?.Name} {The_Performer?.Surname}";
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var parts = value.Split(' ', 2);
                    The_Performer.Name = parts[0];
                    The_Performer.Surname = parts.Length > 1 ? parts[1] : "";
                }
            }
        }


        private int duration;

        public int Duration
        {
            get => duration;
            set
            {
                duration = value;
                OnPropertyChanged();
            }
        }
        private string title;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }


        public Performance(Work work, Performer performer, int duration, string title)
        {
            the_work = work;
            the_performer = performer;
            this.duration = duration;
            this.title = title;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public PerformanceDTO ToDTO() => new PerformanceDTO
        {
            Work = this.the_work,
            Performer = this.the_performer.ToDTO(),
            Duration = this.duration,
            Title = this.title
        };

        public static Performance FromDTO(PerformanceDTO dto)
        {
            var performer = Performer.FromDTO(dto.Performer);
            return new Performance(dto.Work, performer, dto.Duration, dto.Title);
        }

    }
}
