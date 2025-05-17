using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lab4.Class;
using Lab4.DTO;

namespace Lab4.Class
{
    public class Performer : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        private string surname;

        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged();
            }
        }

        public Performer(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public PerformerDTO ToDTO() => new PerformerDTO
        {
            Name = this.name,
            Surname = this.surname
        };
        public static Performer FromDTO(PerformerDTO dto)
        {
            return new Performer(dto.Name, dto.Surname);
        }

    }
}
