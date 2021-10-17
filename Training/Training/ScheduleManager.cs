using System;

namespace Training
{
    public enum DailyType
    {
        Once = 0,
        Hour = 1,
        Minute = 2,
        Second = 3
    }

    public enum FrecuencyType
    {
        Day = 0,
        Week = 1
    }

    public class ScheduleManager
    {
        public static void Validate(Schedule schedule)
        {
            if (schedule == null) { throw new Exception("Must instantiate schedule."); }

            if (schedule.StartDate.HasValue == true &&
                schedule.EndDate.HasValue == true &&
                schedule.StartDate.Value.CompareTo(schedule.EndDate.Value) > 0) { throw new Exception("End date must be greater to start date."); }

            if (schedule.StartDate.HasValue == true)
            {
                if (schedule.DateTime.HasValue == true &&
                    schedule.DateTime.Value.CompareTo(schedule.StartDate.Value) < 0) { throw new Exception("DateTime must be greater to start date."); }
            }

            if (schedule.EndDate.HasValue == true)
            {
                if (schedule.DateTime.HasValue == true &&
                    schedule.DateTime.Value.CompareTo(schedule.EndDate.Value) > 0) { throw new Exception("End date must be greater to dateTime."); }
            }

            if (schedule.IsRecurring == true &&
                schedule.Every <= 0) { throw new Exception("Every must be greater to 0."); }

            if (schedule.IsRecurring == false &&
                schedule.FrecuencyType != FrecuencyType.Day) { throw new Exception("Occurs must be Day."); }

            if (schedule.FrecuencyType == FrecuencyType.Week &&
                schedule.DaysOfWeek.Count == 0) { throw new Exception("Occurs weekly must indicate the days of the week."); }

            if (schedule.DailyFrecuencyType.HasValue == true)
            {
                switch (schedule.DailyFrecuencyType)
                {
                    case DailyType.Once:
                        if (schedule.DailyFrecuencyTime.HasValue == false) { throw new Exception("Must indicate Occurs once at time."); }
                        break;
                    default:
                        if (schedule.DailyFrecuencyStartTime.HasValue == false) { throw new Exception("Must indicate starting at."); }
                        if (schedule.DailyFrecuencyEndTime.HasValue == false) { throw new Exception("Must indicate end at."); }
                        if (schedule.DailyFrecuencyStartTime.Value.CompareTo(schedule.DailyFrecuencyEndTime.Value) > 0) { throw new Exception("End at must be greater to stating at."); }
                        if (schedule.DailyFrecuencyEvery <= 0) { throw new Exception("Occurs every must be greater to 0."); }
                        break;
                }
            }
        }
    }
}
