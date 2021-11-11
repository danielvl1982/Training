using System;

namespace Training
{
    public class ScheduleManager
    {
        public static void Validate(Schedule schedule)
        {
            if (schedule == null) { throw new ScheduleException("Must instantiate schedule."); }

            if (schedule.StartDate.HasValue == true &&
                schedule.EndDate.HasValue == true &&
                schedule.StartDate.Value.CompareTo(schedule.EndDate.Value) > 0) { throw new ScheduleException("End date must be greater to start date."); }

            if (schedule.StartDate.HasValue == true &&
                schedule.DateTime.HasValue == true &&
                schedule.DateTime.Value.CompareTo(schedule.StartDate.Value) < 0) { throw new ScheduleException("DateTime must be greater to start date."); }
            
            if (schedule.EndDate.HasValue == true)
            {
                if (schedule.CurrentDate.CompareTo(schedule.EndDate.Value) > 0) { throw new ScheduleException("Current date must be lesser to end date."); }
                if (schedule.DateTime.HasValue == true &&
                    schedule.DateTime.Value.CompareTo(schedule.EndDate.Value) > 0) { throw new ScheduleException("DateTime must be lesser to end date."); }
            }

            if (schedule.DateTime.HasValue == true &&
                schedule.CurrentDate.CompareTo(schedule.DateTime.Value) > 0) { throw new ScheduleException("Current date must be lesser to datetime."); }
            
            if (schedule.FrecuencyType == FrecuencyType.Once &&
                schedule.Every != 0) { throw new ScheduleException("Every must be 0."); }

            if (schedule.FrecuencyType != FrecuencyType.Once &&
                schedule.Every <= 0) { throw new ScheduleException("Every must be greater to 0."); }

            if (schedule.FrecuencyType == FrecuencyType.Week &&
                schedule.DaysOfWeek == 0) { throw new ScheduleException("Occurs weekly must indicate the days of the week."); }

            if (schedule.FrecuencyType == FrecuencyType.Month)
            {
                if (schedule.MonthyType == MonthyType.Day)
                {
                    if (schedule.MonthyDay <= 0) { throw new ScheduleException("Occurs monthy day must indicate the day of the month."); }
                    if (schedule.MonthyDay > 31) { throw new ScheduleException("Occurs monthy day mustn’t be greater to 31."); }
                    if (schedule.DaysOfWeek > 0) { throw new ScheduleException("Occurs monthy mustn’t indicate the days of the week."); }
                }
                else
                {
                    if (schedule.MonthyDay != 0) { throw new ScheduleException("Occurs monthy day mustn’t indicate the day of the month."); }
                    if (schedule.DaysOfWeek == 0) { throw new ScheduleException("Occurs monthy must indicate the days of the week."); }
                }
            }

            if (schedule.DailyFrecuencyType.HasValue == true)
            {
                switch (schedule.DailyFrecuencyType)
                {
                    case DailyType.Once:
                        if (schedule.DailyFrecuencyTime.HasValue == false) { throw new ScheduleException("Must indicate Occurs once at time."); }
                        if (schedule.DailyFrecuencyEvery != 0) { throw new ScheduleException("Occurs every must be 0."); }
                        break;
                    default:
                        if (schedule.DailyFrecuencyStartTime.HasValue == false) { throw new ScheduleException("Must indicate starting at."); }
                        if (schedule.DailyFrecuencyEndTime.HasValue == false) { throw new ScheduleException("Must indicate end at."); }
                        if (schedule.DailyFrecuencyStartTime.Value.CompareTo(schedule.DailyFrecuencyEndTime.Value) > 0) { throw new ScheduleException("End at must be greater to stating at."); }
                        if (schedule.DailyFrecuencyEvery <= 0) { throw new ScheduleException("Occurs every must be greater to 0."); }
                        break;
                }
            }
        }
    }
}
