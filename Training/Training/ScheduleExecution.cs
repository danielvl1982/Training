using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class ScheduleExecution
    {
        private DateTime currentDate;

        private DateTime? dateTime;

        private readonly Schedule schedule;

        private string description;

        public ScheduleExecution(Schedule schedule, DateTime currentDate)
        {
            this.currentDate = currentDate;

            this.schedule = schedule;
        }

        public DateTime? GetDateTime()
        {
            if (this.dateTime.HasValue == false) { this.LoadDateTime(); }

            return this.dateTime;
        }

        public string GetDescription()
        {
            if (this.GetDateTime().HasValue == false) { return string.Empty; }

            if (string.IsNullOrEmpty(this.description) == true)
            {
                this.description = this.schedule.FrecuencyType switch
                {
                    FrecuencyType.Day => "Occurs every day.",
                    FrecuencyType.Month => "Occurs every month.",
                    FrecuencyType.Once => "Occurs once.",
                    FrecuencyType.Week => "Occurs every week.",
                    _ => string.Empty,
                };

                this.description += " Schedule will be used on " + this.GetDateTime().Value.ToString("dd/MM/yyyy HH:mm:ss");
                this.description += this.schedule.StartDate.HasValue == true
                    ? " starting on " + this.schedule.StartDate.Value.ToString("dd/MM/yyy HH:mm:ss")
                    : string.Empty;
                this.description += this.schedule.EndDate.HasValue == true
                    ? " until " + this.schedule.EndDate.Value.ToString("dd/MM/yyy HH:mm:ss")
                    : string.Empty;
            }

            return this.description;
        }

        public void SetCurrentDate(DateTime dateTime)
        {
            this.currentDate = dateTime;

            this.dateTime = null;

            this.description = null;
        }

        private DateTime GetDateTimeExecution()
        {
            DateTime nextExecution = this.currentDate;

            if (this.schedule.StartDate.HasValue == true &&
                nextExecution.CompareTo(this.schedule.StartDate.Value) < 0) { nextExecution = this.schedule.StartDate.Value; }

            if (this.schedule.DateTime.HasValue == true &&
                nextExecution.CompareTo(this.schedule.DateTime.Value) < 0) { nextExecution = this.schedule.DateTime.Value; }

            if (this.schedule.DailyFrecuencyType.HasValue == false &&
                this.schedule.DateTime.GetTime().Ticks > 0) { nextExecution = nextExecution.Date.Add(this.schedule.DateTime.GetTime()); }            

            return this.schedule.FrecuencyType == FrecuencyType.Once
                ? nextExecution
                : this.GetNextExecutionRecurring(nextExecution);
        }
        private DateTime GetDateTimeIncremented(DateTime dateTime)
        {
            switch (this.schedule.FrecuencyType)
            {
                case FrecuencyType.Day:
                    dateTime = dateTime.AddDays(this.schedule.Every);
                    break;
                case FrecuencyType.Month:
                    dateTime = this.GetNextExecutionByMonth(dateTime);
                    break;
                case FrecuencyType.Week:
                    dateTime = this.GetNextExecutionByWeek(dateTime);
                    break;
            }

            return dateTime;
        }
        private DateTime GetNextExecutionRecurring(DateTime dateTime)
        {
            if (this.schedule.DailyFrecuencyType.HasValue == true &&
                dateTime.IsDayOfWeekValid(this.schedule.DaysOfWeek) == true &&
                dateTime.IsMonthyDayValid(this.schedule.MonthyType, this.schedule.MonthyDay) == true &&
                dateTime.IsWeekValid(this.schedule.MonthyType) == true)
            {
                DateTime? nextExecutionDay = this.schedule.DailyFrecuencyType == DailyType.Once
                    ? this.GetNextExecutionDayOnce(dateTime)
                    : this.GetNextExecutionDayRecurring(dateTime);

                if (nextExecutionDay.HasValue == true) { return nextExecutionDay.Value; }
            }

            if (this.schedule.DailyFrecuencyType.HasValue == true) { return this.GetNextExecutionRecurring(this.GetDateTimeIncremented(dateTime).Date); }
            else { return this.GetDateTimeIncremented(dateTime); }
        }
        private DateTime GetNextExecutionByMonth(DateTime dateTime)
        {
            DayOfWeek? nextDayOfWeek = dateTime.IsWeekValid(this.schedule.MonthyType) == true
                ? dateTime.NextDayOfWeek(this.schedule.DaysOfWeek)
                : null;

            if (nextDayOfWeek.HasValue == true) { return dateTime.GetDayOfWeek(nextDayOfWeek.Value); }
            else
            {
                if (this.schedule.MonthyType == MonthyType.Day)
                {
                    DateTime dateMonth = dateTime.AddMonths(0, this.schedule.MonthyDay);

                    return dateMonth > dateTime
                        ? dateMonth
                        : dateTime.AddMonths(this.schedule.Every, this.schedule.MonthyDay);
                }
                else
                {
                    DateTime dateMonth = dateTime.AddMonths(this.schedule.MonthyType, this.schedule.DaysOfWeek, this.schedule.Every);

                    return dateMonth > dateTime
                        ? dateMonth
                        : dateTime.AddMonths(this.schedule.MonthyType, this.schedule.DaysOfWeek, this.schedule.Every);
                }
            }
        }
        private DateTime GetNextExecutionByWeek(DateTime dateTime)
        {
            DayOfWeek? nextDayOfWeek = dateTime.NextDayOfWeek(this.schedule.DaysOfWeek);

            return nextDayOfWeek.HasValue == true
                ? dateTime.GetDayOfWeek(nextDayOfWeek.Value)
                : dateTime.AddWeeks(this.schedule.Every);
        }

        private DateTime? GetNextExecutionDayOnce(DateTime dateTime)
        {
            return dateTime.TimeOfDay >= this.schedule.DailyFrecuencyTime.Value
                ? null
                : (DateTime?)dateTime.Date.Add(this.schedule.DailyFrecuencyTime.Value);
        }
        private DateTime? GetNextExecutionDayRecurring(DateTime dateTime)
        {
            IEnumerable<TimeSpan> nextTimes = this.GetNextExecutionTimeOfDay(dateTime);

            return nextTimes.Count() == 0 ? null : (DateTime?)dateTime.Date.Add(nextTimes.First());
        }

        private IEnumerable<TimeSpan> GetNextExecutionTimeOfDay(DateTime dateTime)
        {
            List<TimeSpan> timesGap = this.schedule.DailyFrecuencyStartTime.Value.GetTimesGap(
                    this.schedule.DailyFrecuencyEndTime.Value,
                    this.schedule.DailyFrecuencyEvery,
                    this.schedule.DailyFrecuencyType.Value);

            return (from time in timesGap
                    where time > dateTime.TimeOfDay
                    orderby time.Ticks
                    select time);
        }

        private void LoadDateTime()
        {
            ScheduleManager.Validate(this.schedule);

            DateTime nextDateTime = this.GetDateTimeExecution();

            this.dateTime = this.schedule.EndDate.HasValue == true && nextDateTime.CompareTo(this.schedule.EndDate.Value) > 0 
                ? null 
                : (DateTime?)nextDateTime;
        }
    }
}
