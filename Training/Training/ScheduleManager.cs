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

    [Flags]
    public enum DaysOfWeekType
    {
        None = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 4,
        Thursday = 8,
        Friday = 16,
        Saturday = 32,
        Sunday = 64,
        Day = Monday + Tuesday + Wednesday + Thursday + Friday + Saturday + Sunday,
        Weekday = Monday + Tuesday + Wednesday + Thursday + Friday,
        Weekenday = Saturday + Sunday
    }

    public enum MonthyType
    {
        None = 0,
        First = 1,
        Second = 2,
        Thrid = 3,
        Fourth = 4,
        Last = 5,
        Day = 6
    }

    public enum FrecuencyType
    {
        Once = 0,
        Day = 1,
        Week = 2,
        Month = 3
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

            if (schedule.FrecuencyType == FrecuencyType.Once &&
                schedule.Every != 0) { throw new Exception("Every must be 0."); }

            if (schedule.FrecuencyType != FrecuencyType.Once &&
                schedule.Every <= 0) { throw new Exception("Every must be greater to 0."); }

            if (schedule.FrecuencyType == FrecuencyType.Week &&
                Convert.ToInt32(schedule.DaysOfWeek) == 0) { throw new Exception("Occurs weekly must indicate the days of the week."); }

            if (schedule.FrecuencyType == FrecuencyType.Month)
            {
                if (schedule.MonthyType == MonthyType.Day)
                {
                    if (Convert.ToInt32(schedule.MonthyDay) <= 0) { throw new Exception("Occurs monthy day must indicate the day of the month."); }
                    if (Convert.ToInt32(schedule.MonthyDay) > 31) { throw new Exception("Occurs monthy day mustn’t be greater to 31."); }
                    if (Convert.ToInt32(schedule.DaysOfWeek) > 0) { throw new Exception("Occurs monthy mustn’t indicate the days of the week."); }
                }
                else
                {
                    if (Convert.ToInt32(schedule.MonthyDay) != 0) { throw new Exception("Occurs monthy day mustn’t indicate the day of the month."); }
                    if (Convert.ToInt32(schedule.DaysOfWeek) == 0) { throw new Exception("Occurs monthy must indicate the days of the week."); }
                }
            }

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
