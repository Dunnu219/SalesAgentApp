namespace SalesAgentDomain.Models
{
    public class SalesAgentAvailability
    {
        public string AgentName { get; set; }

        public int AgentId { get; set; }

        public DateTime AvailableDateTime { get; set; }

        public DayOfWeek Day { get; set; }

        public int TimeSlotId { get; set; }

        public string StartTime => AvailableDateTime.ToLocalTime().ToString("dd/MM/yyyy HH:mm").Split(" ")[1];

        public string EndTime => AvailableDateTime.ToLocalTime().AddMinutes(30).ToString("dd/MM/yyyy HH:mm").Split(" ")[1];

        public DateOnly AvailableDate => DateOnly.FromDateTime(AvailableDateTime.ToLocalTime());

        public string DayValue => Day.ToString();
    }
}