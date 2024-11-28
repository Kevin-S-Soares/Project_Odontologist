using Server.Models.Attributes;

namespace Server.Models
{
    [DetailedTimeValidation]
    public class DetailedTime 
    {

        public long Id { get; set; }
        public long AppointmentId { get; set; }
        public int StartDay { get; set; }
        public DayOfWeek StartDayOfWeek { get; set; }
        public int StartMonth { get; set; }
        public int StartYear { get; set; }
        public TimeSpan StartTime { get; set; }
        public int EndDay { get; set; }
        public DayOfWeek EndDayOfWeek { get; set; }
        public int EndMonth { get; set; }
        public int EndYear { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}