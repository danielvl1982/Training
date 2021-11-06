using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class ScheduleExecution
    {
        private readonly Schedule schedule;

        public ScheduleExecution(Schedule schedule)
        {
            this.schedule = schedule;
        }

        public DateTime? GetDateTime()
        {
            ScheduleManager.Validate(this.schedule);

            DateTime nextDateTime = this.GetDateTimeExecution();

            return this.schedule.EndDate.HasValue == true && nextDateTime.CompareTo(this.schedule.EndDate.Value) > 0
                ? null
                : (DateTime?)nextDateTime;
        }

        public string GetDescription(DateTime timeExecution)
        {
            string description = this.schedule.FrecuencyType switch
            {
                FrecuencyType.Day => "Occurs every day.",
                FrecuencyType.Month => "Occurs every month.",
                FrecuencyType.Once => "Occurs once.",
                FrecuencyType.Week => "Occurs every week.",
                _ => string.Empty,
            };

            description += " Schedule will be used on " + timeExecution.ToString("dd/MM/yyyy HH:mm:ss");
            description += this.schedule.StartDate.HasValue == true
                ? " starting on " + this.schedule.StartDate.Value.ToString("dd/MM/yyy HH:mm:ss")
                : string.Empty;
            description += this.schedule.EndDate.HasValue == true
                ? " until " + this.schedule.EndDate.Value.ToString("dd/MM/yyy HH:mm:ss")
                : string.Empty;

            return description;
        }

        private DateTime GetDateTimeExecution()
        {
            DateTime nextExecution = this.schedule.CurrentDate;

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
        private DateTime GetNextExecutionByMonth(DateTime dateTime)
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
                DayOfWeek? nextDayOfWeek = dateTime.IsWeekValid(this.schedule.MonthyType) == true
                    ? dateTime.NextDayOfWeek(this.schedule.DaysOfWeek)
                    : null;

                if (nextDayOfWeek.HasValue == true) { return dateTime.GetDayOfWeek(nextDayOfWeek.Value); }
                else
                {
                    DateTime dateOfMonth = dateTime.AddMonths(this.schedule.MonthyType, this.schedule.DaysOfWeek, 0);

                    return dateOfMonth > dateTime
                        ? dateOfMonth
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
    }
}
