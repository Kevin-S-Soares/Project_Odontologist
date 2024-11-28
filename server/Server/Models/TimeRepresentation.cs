namespace Server.Models
{
    public class TimeRepresentation
    {
        public static bool IsPartiallyInserted(
            ITimeRepresentation contained, ITimeRepresentation contains)
        {
            long startTicksContains, endTicksContains, startTicksContained, endTicksContained;
            startTicksContains = GetStartTimeTicks(contains);
            endTicksContains = GetEndTimeTicks(contains);
            startTicksContained = GetStartTimeTicks(contained);
            endTicksContained = GetEndTimeTicks(contained);

            bool condition1 = startTicksContains <= startTicksContained
                && endTicksContains >= startTicksContained;

            bool condition2 = startTicksContains <= endTicksContained
                && endTicksContains >= endTicksContained;

            return condition1 || condition2;
        }

        public static bool IsCompletelyInserted(
            ITimeRepresentation contained, ITimeRepresentation contains)
        {
            long startTicksContains, endTicksContains, startTicksContained, endTicksContained;
            startTicksContains = GetStartTimeTicks(contains);
            endTicksContains = GetEndTimeTicks(contains);
            startTicksContained = GetStartTimeTicks(contained);
            endTicksContained = GetEndTimeTicks(contained);

            bool condition1 = startTicksContains <= startTicksContained
                && endTicksContains >= startTicksContained;

            bool condition2 = startTicksContains <= endTicksContained
                && endTicksContains >= endTicksContained;

            return condition1 && condition2;
        }

        public static bool IsAppointmentPartiallyInserted(
    Appointment contained, Appointment contains)
        {
            long startTicksContains, endTicksContains, startTicksContained, endTicksContained;
            startTicksContains = contains.Details.StartTime.Ticks;
            endTicksContains = contains.Details.EndTime.Ticks;
            startTicksContained = contained.Details.StartTime.Ticks;
            endTicksContained = contained.Details.EndTime.Ticks;

            bool condition1 = startTicksContains <= startTicksContained
                && endTicksContains >= startTicksContained;

            bool condition2 = startTicksContains <= endTicksContained
                && endTicksContains >= endTicksContained;

            return condition1 || condition2;
        }

        public static bool IsAppointmentCompletelyInserted(
            Appointment contained, Appointment contains)
        {
            long startTicksContains, endTicksContains, startTicksContained, endTicksContained;
            startTicksContains = contains.Details.StartTime.Ticks;
            endTicksContains = contains.Details.EndTime.Ticks;
            startTicksContained = contained.Details.StartTime.Ticks;
            endTicksContained = contained.Details.EndTime.Ticks;

            bool condition1 = startTicksContains <= startTicksContained
                && endTicksContains >= startTicksContained;

            bool condition2 = startTicksContains <= endTicksContained
                && endTicksContains >= endTicksContained;

            return condition1 && condition2;
        }

        private static long GetStartTimeTicks(ITimeRepresentation obj)
        {
            return Convert.ToInt64(obj.GetStartDay()) * TimeSpan.TicksPerDay + obj.GetStartTime().Ticks;
        }

        private static long GetEndTimeTicks(ITimeRepresentation obj)
        {
            return Convert.ToInt64(obj.GetEndDay()) * TimeSpan.TicksPerDay + obj.GetEndTime().Ticks;
        }
    }
}