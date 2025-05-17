using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4.Class;
using Lab4.DTO;

namespace Lab4.DTO
{
    public class ConcertDTO
    {
        public string Organizer { get; set; }
        public DateTime Date { get; set; }
        public List<PerformanceDTO> Performances { get; set; } = new();
    }
}
