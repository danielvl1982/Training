using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class ScheduleRun
    {
        private readonly Schedule schedule;

        public ScheduleRun(Schedule schedule)
        {
            this.schedule = schedule;
        }

        public ScheduleRunData GetScheduleRun()
        {
            ScheduleManager.Validate(this.schedule);

            DateTime? runDateTime = this.GetDateTimeRun();

            string description = this.GetDescription(runDateTime);

            return new ScheduleRunData(runDateTime, description);
        }

        private DateTime GetDateTimeIncremented(DateTime dateTime)
        {
            switch (this.schedule.FrecuencyType)
            {
                case FrecuencyType.Day:
                    dateTime = this.GetNextRunByDay(dateTime);
                    break;
                case FrecuencyType.Month:
                    dateTime = this.GetNextRunByMonth(dateTime);
                    break;
                case FrecuencyType.Week:
                    dateTime = this.GetNextRunByWeek(dateTime);
                    break;
            }

            return dateTime;
        }        
        private DateTime GetNextRunByDay(DateTime dateTime)
        {
            return this.schedule.StartDate.HasValue == true &&
                dateTime.CompareTo(this.schedule.StartDate.Value) < 0
                ? this.schedule.StartDate.Value
                : dateTime.AddDays(this.schedule.Every);
        }
        private DateTime GetNextRunByMonth(DateTime dateTime)
        {
            if (this.schedule.MonthyType == MonthyType.Day)
            {
                if (this.schedule.StartDate.HasValue == true &&
                    this.schedule.StartDate.Value.CompareTo(dateTime) > 0)
                {
                    dateTime = this.schedule.StartDate.Value;

                    if (dateTime.IsDayOfMonthValid(this.schedule.MonthyType, this.schedule.MonthyDay) == true) { return dateTime; }
                }

                DateTime? dateOfMonth = dateTime.AddMonths(this.schedule.MonthyDay, 0);

                return dateOfMonth.HasValue == true
                    ? dateOfMonth.Value
                    : dateTime.AddMonths(this.schedule.MonthyDay, this.schedule.Every).Value;
            }
            else
            {
                if (this.schedule.StartDate.HasValue == true &&
                    this.schedule.StartDate.Value.CompareTo(dateTime) > 0)
                {
                    dateTime = this.schedule.StartDate.Value;

                    if (dateTime.IsOccurrenceMonthValid(this.schedule.MonthyType) == true) { return dateTime; }
                }

                DateTime? dateOfMonth = dateTime.AddMonths(this.schedule.MonthyType, this.schedule.DaysOfWeek, 0);

                return dateOfMonth.HasValue == true
                        ? dateOfMonth.Value
                        : dateTime.AddMonths(this.schedule.MonthyType, this.schedule.DaysOfWeek, this.schedule.Every).Value;
            }
        }
        private DateTime GetNextRunByWeek(DateTime dateTime)
        {
            if (this.schedule.StartDate.HasValue == true &&
                this.schedule.StartDate.Value.CompareTo(dateTime) > 0)
            {
                dateTime = this.schedule.StartDate.Value;

                if (dateTime.IsDayOfWeekValid(this.schedule.DaysOfWeek) == true) { return dateTime; }
            }

            DayOfWeek? nextDayOfWeek = dateTime.NextDayOfWeek(this.schedule.DaysOfWeek);

            return nextDayOfWeek.HasValue == true
                ? dateTime.GetDayOfWeek(nextDayOfWeek.Value)
                : dateTime.AddWeeks(this.schedule.Every);
        }
        private DateTime GetNextRunRecurring(DateTime dateTime)
        {
            DateTime? nextRun = this.GetNextRunDay(dateTime);

            return nextRun.HasValue == true
                ? nextRun.Value
                : this.schedule.DailyFrecuencyType.HasValue == true
                    ? this.GetNextRunRecurring(this.GetDateTimeIncremented(dateTime).Date)
                    : this.GetDateTimeIncremented(dateTime);
        }

        private DateTime? GetDateTimeRun()
        {
            DateTime runDateTime = this.schedule.FrecuencyType == FrecuencyType.Once
                ? this.schedule.DateTime.Value
                : this.GetNextRunRecurring(this.schedule.CurrentDate);

            return this.schedule.EndDate.HasValue == true && runDateTime.CompareTo(this.schedule.EndDate.Value) > 0
                ? null
                : (DateTime?)runDateTime;
        }
        private DateTime? GetNextRunDay(DateTime dateTime)
        {
            if (this.schedule.StartDate.HasValue == true &&
                this.schedule.StartDate.Value.CompareTo(dateTime) > 0) { return null; }

            if (this.schedule.DailyFrecuencyType.HasValue == false ||
                dateTime.IsDayOfWeekValid(this.schedule.DaysOfWeek) == false ||
                dateTime.IsDayOfMonthValid(this.schedule.MonthyType, this.schedule.MonthyDay) == false ||
                dateTime.IsOccurrenceMonthValid(this.schedule.MonthyType) == false) { return null; }

            return this.schedule.DailyFrecuencyType == DailyType.Once
                ? this.GetNextRunDayOnce(dateTime)
                : this.GetNextRunDayRecurring(dateTime);
        }
        private DateTime? GetNextRunDayOnce(DateTime dateTime)
        {
            return dateTime.TimeOfDay >= this.schedule.DailyFrecuencyTime.Value
                ? null
                : (DateTime?)dateTime.Date.Add(this.schedule.DailyFrecuencyTime.Value);
        }
        private DateTime? GetNextRunDayRecurring(DateTime dateTime)
        {
            IEnumerable<TimeSpan> nextTimes = this.GetNextRunTimeOfDay(dateTime);

            return nextTimes.Count() == 0 ? null : (DateTime?)dateTime.Date.Add(nextTimes.First());
        }

        private IEnumerable<TimeSpan> GetNextRunTimeOfDay(DateTime dateTime)
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

        private string GetDescription(DateTime? runDateTime)
        {
            if (runDateTime == null) { return string.Empty; }

            string description = this.schedule.FrecuencyType switch
            {
                FrecuencyType.Day => "Occurs every day. Schedule will be used on " + runDateTime.Value.GetDescription(),
                FrecuencyType.Once => "Occurs once. Schedule will be used on " + runDateTime.Value.GetDescription(),
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
