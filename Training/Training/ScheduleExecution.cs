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
                FrecuencyType.Day => "Occurs every day. Schedule will be used on " + timeExecution.GetDescription(),
                FrecuencyType.Once => "Occurs once. Schedule will be used on " + timeExecution.GetDescription(),
                FrecuencyType.Week => "Occurs every " + this.schedule.Every + " week on " + this.GetDescriptionDaysOfWeek(),
                FrecuencyType.Month => this.GetDescriptionMonthyType(),
                _ => string.Empty,
            };

            description += schedule.MonthyDay > 0
                ? " Day " + schedule.MonthyDay.ToString()
                : string.Empty;

            description += schedule.DailyFrecuencyTime.HasValue == true
                ? " on " + schedule.DailyFrecuencyTime.Value.ToString(@"hh\:mm\:ss")
                : string.Empty;

            description += schedule.DailyFrecuencyType.HasValue == true &&
                schedule.DailyFrecuencyType != DailyType.Once
                ? " every " + schedule.DailyFrecuencyEvery.ToString() + " " + schedule.DailyFrecuencyType.ToString().ToLower() +
                    " beetween " + schedule.DailyFrecuencyStartTime.Value.ToString(@"hh\:mm\:ss") +
                    " and " + schedule.DailyFrecuencyEndTime.Value.ToString(@"hh\:mm\:ss")
                : string.Empty;

            description += this.schedule.StartDate.HasValue == true
                ? " starting on " + this.schedule.StartDate.Value.GetDescription()
                : string.Empty;
            description += this.schedule.EndDate.HasValue == true
                ? " until " + this.schedule.EndDate.Value.GetDescription()
                : string.Empty;

            return description;
        }

        private DateTime GetDateTimeExecution()
        {
            DateTime nextExecution = this.schedule.CurrentDate;

            if (this.schedule.DateTime.HasValue == true &&
                nextExecution.CompareTo(this.schedule.DateTime.Value) < 0) { nextExecution = this.schedule.DateTime.Value; }

            if (this.schedule.StartDate.HasValue == true &&
                nextExecution.CompareTo(this.schedule.StartDate.Value) < 0) { nextExecution = this.schedule.StartDate.Value; }

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
                DateTime? dateOfMonth = dateTime.AddMonths(this.schedule.MonthyDay, 0);

                return dateOfMonth.HasValue == true
                    ? dateOfMonth.Value
                    : dateTime.AddMonths(this.schedule.MonthyDay, this.schedule.Every).Value;
            }
            else
            {
                DateTime? dateOfMonth = dateTime.AddMonths(this.schedule.MonthyType, this.schedule.DaysOfWeek, 0);

                return dateOfMonth.HasValue == true
                        ? dateOfMonth.Value
                        : dateTime.AddMonths(this.schedule.MonthyType, this.schedule.DaysOfWeek, this.schedule.Every).Value;
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
                dateTime.IsDayOfMonthValid(this.schedule.MonthyType, this.schedule.MonthyDay) == true &&
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

        private string GetDescriptionDaysOfWeek()
        {
            List<string> descriptionDays = new List<string>();

            if (this.schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Monday) == true) { descriptionDays.Add("monday"); }
            if (this.schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Tuesday) == true) { descriptionDays.Add("tuesday"); }
            if (this.schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Wednesday) == true) { descriptionDays.Add("wednesday"); }
            if (this.schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Thursday) == true) { descriptionDays.Add("thursday"); }
            if (this.schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Friday) == true) { descriptionDays.Add("friday"); }
            if (this.schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Saturday) == true) { descriptionDays.Add("saturday"); }
            if (this.schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Sunday) == true) { descriptionDays.Add("sunday"); }

            return string.Join(", ", descriptionDays.ToArray());
        }
        private string GetDescriptionMonthyType()
        {
            if (this.schedule.MonthyType == MonthyType.Day) { return "Occurs every " + this.schedule.Every.ToString() + " month."; }
            else { return "Occurs the " + this.schedule.MonthyType.ToString().ToLower() + " " + this.GetDescriptionDaysOfWeek() + " of the very " + this.schedule.Every.ToString() + " months"; }
        }
    }
}
