using System;

namespace Training
{
    public enum DailyType
    {
        Hour = 0,
        Minute = 1,
        Second = 2
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

            if (schedule.Type == null) { throw new Exception("Must instantiate trigger type."); }

            if (schedule.StartDate.HasValue == true)
            {
                if (schedule.DateTime.HasValue == true &&
                    schedule.DateTime.Value.CompareTo(schedule.StartDate.Value) < 0) { throw new Exception("DateTime must be greater to start date."); }

                if (schedule.DateTime.HasValue == true &&
                    schedule.DateTime.Value.CompareTo(schedule.StartDate.Value) < 0) { throw new Exception("DateTime must be lower to end date."); }
            }

            if (schedule.Type.IsRecurring == true &&
                schedule.Every <= 0) { throw new Exception("Every must be greater to 0."); }

            if (schedule.Type.Type == FrecuencyType.Week &&
                schedule.DaysOfWeek.Count == 0) { throw new Exception("Occurs weekly must indicate the days of the week."); }

            if (Frecuency.GetItems().Exists(o => o.Name == schedule.Type.Name) == false) { throw new Exception("Must indicate to correct trigger occurs."); }

            if (schedule.Frecuency == null) { return; }

            if (schedule.Frecuency.DailyFrecuencyIsRecurring == true)
            {
                if (schedule.Frecuency.DailyFrecuencyStartTime.HasValue == false) { throw new Exception("Must indicate starting at."); }
                if (schedule.Frecuency.DailyFrecuencyEndTime.HasValue == false) { throw new Exception("Must indicate end at."); }
                if (schedule.Frecuency.DailyFrecuencyStartTime.Value.CompareTo(schedule.Frecuency.DailyFrecuencyEndTime.Value) > 0) { throw new Exception("End at must be greater to stating at."); }
                if (schedule.Frecuency.DailyFrecuencyEvery <= 0) { throw new Exception("Occurs every must be greater to 0."); }
            }
            else
            {
                if (schedule.Frecuency.DailyFrecuencyTime.HasValue == false) { throw new Exception("Must indicate Occurs once at time."); }
            }

            if (DailyFrecuency.GetItems().Exists(o => o.DailyFrecuencyName == schedule.Frecuency.DailyFrecuencyName) == false) { throw new Exception("Must indicate to correct daily occurs."); }
        }
    }
}
