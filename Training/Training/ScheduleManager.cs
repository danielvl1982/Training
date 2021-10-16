using System;

namespace Training
{
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

            if (schedule.Type.Occurs.Type == FrecuencyOccurType.Week &&
                schedule.DaysOfWeek.Count == 0) { throw new Exception("Occurs weekly must indicate the days of the week."); }

            if (schedule.Type.Occurs == null) { throw new Exception("Must indicate occurs."); }

            if (FrecuencyOccur.GetTriggerItems().Exists(o => o.Name == schedule.Type.Occurs.Name) == false) { throw new Exception("Must indicate to correct trigger occurs."); }

            if (schedule.Frecuency == null) { return; }
            if (schedule.Frecuency.Type == null) { throw new Exception("Must instantiate frecuency type."); }

            if (schedule.Frecuency.Type.IsRecurring == true)
            {
                if (schedule.Frecuency.StartTime.HasValue == false) { throw new Exception("Must indicate starting at."); }
                if (schedule.Frecuency.EndTime.HasValue == false) { throw new Exception("Must indicate end at."); }
                if (schedule.Frecuency.StartTime.Value.CompareTo(schedule.Frecuency.EndTime.Value) > 0) { throw new Exception("End at must be greater to stating at."); }
                if (schedule.Frecuency.Every <= 0) { throw new Exception("Occurs every must be greater to 0."); }
            }
            else
            {
                if (schedule.Frecuency.Time.HasValue == false) { throw new Exception("Must indicate Occurs once at time."); }
            }

            if (schedule.Frecuency.Type.Occurs == null) { throw new Exception("Must indicate frecuency occurs."); }

            if (FrecuencyOccur.GetDailyItems().Exists(o => o.Name == schedule.Frecuency.Type.Occurs.Name) == false) { throw new Exception("Must indicate to correct daily occurs."); }
        }
    }
}
