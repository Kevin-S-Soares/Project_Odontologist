using Server.Models.Attributes;

namespace Server.Models
{
    [AppointmentValidation]
    public class Appointment : ITimeRepresentation
    {
        public long Id { get; set; }
        public long ScheduleId { get; set; }
        public Schedule? Schedule { get; set; }
        public DetailedTime Details { get; set; } = new();
        public string PatientName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DayOfWeek GetStartDay() => Details.StartDayOfWeek;

        public DayOfWeek GetEndDay() => Details.EndDayOfWeek;

        public TimeSpan GetStartTime() => Details.StartTime;

        public TimeSpan GetEndTime() => Details.EndTime;

        public override bool Equals(object? obj)
        {
            return obj is Appointment appointment &&
                   Id == appointment.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}