using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4.Class;
using Lab4.DTO;

namespace Lab4.DTO
{
    public class PerformanceDTO
    {
        public Work Work { get; set; }
        public PerformerDTO Performer { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
    }
}
