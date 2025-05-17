using Lab4.Class;

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
