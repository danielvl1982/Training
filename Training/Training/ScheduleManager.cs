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
                schedule.StartDate.Value.CompareTo(schedule.EndDate.Value) > 0) { throw new ScheduleException(Messages.Get("compare_end_date_star_date", schedule.Culture).Value); }

            if (schedule.StartDate.HasValue == true &&
                schedule.DateTime.HasValue == true &&
                schedule.DateTime.Value.CompareTo(schedule.StartDate.Value) < 0) { throw new ScheduleException(Messages.Get("compare_datetime_star_date", schedule.Culture).Value); }
            
            if (schedule.EndDate.HasValue == true)
            {
                if (schedule.CurrentDate.CompareTo(schedule.EndDate.Value) > 0) { throw new ScheduleException(Messages.Get("compare_current_date_end_date", schedule.Culture).Value); }
                
                if (schedule.DateTime.HasValue == true &&
                    schedule.DateTime.Value.CompareTo(schedule.EndDate.Value) > 0) { throw new ScheduleException(Messages.Get("compare_datetime_end_date", schedule.Culture).Value); }
            }

            if (schedule.DateTime.HasValue == true &&
                schedule.CurrentDate.CompareTo(schedule.DateTime.Value) > 0) { throw new ScheduleException(Messages.Get("compare_current_date_datetime", schedule.Culture).Value); }

            if (schedule.FrecuencyType == FrecuencyType.Once)
            {
                if (schedule.DateTime.HasValue == false) { throw new ScheduleException(Messages.Get("occurs_once_datetime_null", schedule.Culture).Value); }
                if (schedule.Every != 0) { throw new ScheduleException(Messages.Get("every_must_be_0", schedule.Culture).Value); }
            }
            else
            {
                if (schedule.DateTime.HasValue == true) { throw new ScheduleException(Messages.Get("occurs_recurring_datetime", schedule.Culture).Value); }
                if (schedule.Every <= 0) { throw new ScheduleException(Messages.Get("every_must_greater_to_0", schedule.Culture).Value); }
            }

            if (schedule.FrecuencyType == FrecuencyType.Week &&
                schedule.DaysOfWeek == 0) { throw new ScheduleException(Messages.Get("occurs_weekly_must_indicate_the_days_of_the_week", schedule.Culture).Value); }

            if (schedule.FrecuencyType == FrecuencyType.Month)
            {
                if (schedule.MonthyType == MonthyType.Day)
                {
                    if (schedule.MonthyDay <= 0) { throw new ScheduleException(Messages.Get("occurs_monthy_day_must_indicate_the_day_of_the_week", schedule.Culture).Value); }
                    if (schedule.MonthyDay > 31) { throw new ScheduleException(Messages.Get("occurs_monthy_day_not_valid", schedule.Culture).Value); }
                    if (schedule.DaysOfWeek > 0) { throw new ScheduleException(Messages.Get("occurs_monthy_mustnt_indicate_the_days_of_the_week", schedule.Culture).Value); }
                }
                else
                {
                    if (schedule.MonthyDay != 0) { throw new ScheduleException(Messages.Get("occurs_monthy_mustnt_indicate_the_day_of_the_month", schedule.Culture).Value); }
                    if (schedule.DaysOfWeek == 0) { throw new ScheduleException(Messages.Get("occurs_monthy_must_indicate_the_days_of_the_week", schedule.Culture).Value); }
                }
            }

            if (schedule.DailyFrecuencyType.HasValue == true)
            {
                switch (schedule.DailyFrecuencyType)
                {
                    case DailyType.Once:
                        if (schedule.DailyFrecuencyTime.HasValue == false) { throw new ScheduleException(Messages.Get("must_indicate_occurs_once_at_time", schedule.Culture).Value); }
                        if (schedule.DailyFrecuencyEvery != 0) { throw new ScheduleException(Messages.Get("occurs_every_must_be_0", schedule.Culture).Value); }
                        break;
                    default:
                        if (schedule.DailyFrecuencyStartTime.HasValue == false) { throw new ScheduleException(Messages.Get("must_indicate_starting_at", schedule.Culture).Value); }
                        if (schedule.DailyFrecuencyEndTime.HasValue == false) { throw new ScheduleException(Messages.Get("must_indicate_end_at", schedule.Culture).Value); }
                        if (schedule.DailyFrecuencyStartTime.Value.CompareTo(schedule.DailyFrecuencyEndTime.Value) > 0) { throw new ScheduleException(Messages.Get("compare_end_at_starting_at", schedule.Culture).Value); }
                        if (schedule.DailyFrecuencyEvery <= 0) { throw new ScheduleException(Messages.Get("occurs_every_must_be_greater_to_0", schedule.Culture).Value); }
                        break;
                }
            }
        }
    }
}
