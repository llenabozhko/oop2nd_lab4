namespace Lab4.DTO
{
    public class ConcertDTO
    {
        public string Organizer { get; set; }
        public DateTime Date { get; set; }
        public List<PerformanceDTO> Performances { get; set; } = new();
    }
}
