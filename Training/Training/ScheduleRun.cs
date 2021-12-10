using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class ScheduleRun
    {
        public ScheduleRunData GetScheduleRun(Schedule schedule)
        {
            ScheduleManager.Validate(schedule);

            DateTime? runDateTime = ScheduleRun.GetDateTimeRun(schedule);

            string description = ScheduleRun.GetDescription(schedule, runDateTime);

            return new ScheduleRunData(runDateTime, description);
        }

        private static DateTime GetDateTimeIncremented(Schedule schedule, DateTime dateTime)
        {
            switch (schedule.FrecuencyType)
            {
                case FrecuencyType.Day:
                    dateTime = ScheduleRun.GetNextRunByDay(schedule, dateTime);
                    break;
                case FrecuencyType.Month:
                    dateTime = ScheduleRun.GetNextRunByMonth(schedule, dateTime);
                    break;
                case FrecuencyType.Week:
                    dateTime = ScheduleRun.GetNextRunByWeek(schedule, dateTime);
                    break;
            }

            return dateTime;
        }        
        private static DateTime GetNextRunByDay(Schedule schedule, DateTime dateTime)
        {
            return schedule.StartDate.HasValue == true &&
                dateTime.CompareTo(schedule.StartDate.Value) < 0
                ? schedule.StartDate.Value
                : dateTime.AddDays(schedule.Every);
        }
        private static DateTime GetNextRunByMonth(Schedule schedule, DateTime dateTime)
        {
            if (schedule.MonthyType == MonthyType.Day)
            {
                return ScheduleRun.GetNextRunByMonthDay(schedule, dateTime);
            }
            else
            {
                return ScheduleRun.GetNextRunByMonthRecurring(schedule, dateTime);
            }
        }
        private static DateTime GetNextRunByMonthDay(Schedule schedule, DateTime dateTime)
        {
            if (schedule.StartDate.HasValue == true &&
                schedule.StartDate.Value.CompareTo(dateTime) > 0)
            {
                dateTime = schedule.StartDate.Value;

                if (dateTime.IsDayOfMonthValid(schedule.MonthyType, schedule.MonthyDay) == true) { return dateTime; }
            }

            DateTime? dateOfMonth = dateTime.AddMonths(schedule.MonthyDay, 0);

            return dateOfMonth.HasValue == true
                ? dateOfMonth.Value
                : dateTime.AddMonths(schedule.MonthyDay, schedule.Every).Value;
        }
        private static DateTime GetNextRunByMonthRecurring(Schedule schedule, DateTime dateTime)
        {
            if (schedule.StartDate.HasValue == true &&
                schedule.StartDate.Value.CompareTo(dateTime) > 0)
            {
                dateTime = schedule.StartDate.Value;

                if (dateTime.IsOccurrenceMonthValid(schedule.MonthyType) == true) { return dateTime; }
            }

            DateTime? dateOfMonth = dateTime.AddMonths(schedule.MonthyType, schedule.DaysOfWeek, 0);

            return dateOfMonth.HasValue == true
                    ? dateOfMonth.Value
                    : dateTime.AddMonths(schedule.MonthyType, schedule.DaysOfWeek, schedule.Every).Value;
        }
        private static DateTime GetNextRunByWeek(Schedule schedule, DateTime dateTime)
        {
            if (schedule.StartDate.HasValue == true &&
                schedule.StartDate.Value.CompareTo(dateTime) > 0)
            {
                dateTime = schedule.StartDate.Value;

                if (dateTime.IsDayOfWeekValid(schedule.DaysOfWeek) == true) { return dateTime; }
            }

            DayOfWeek? nextDayOfWeek = dateTime.NextDayOfWeek(schedule.DaysOfWeek);

            return nextDayOfWeek.HasValue == true
                ? dateTime.GetDayOfWeek(nextDayOfWeek.Value)
                : dateTime.AddWeeks(schedule.Every);
        }
        private static DateTime GetNextRunRecurring(Schedule schedule, DateTime dateTime)
        {
            DateTime? nextRun = ScheduleRun.GetNextRunDay(schedule, dateTime);

            return nextRun.HasValue == true
                ? nextRun.Value
                : schedule.DailyFrecuencyType.HasValue == true
                    ? ScheduleRun.GetNextRunRecurring(schedule, ScheduleRun.GetDateTimeIncremented(schedule, dateTime).Date)
                    : ScheduleRun.GetDateTimeIncremented(schedule, dateTime);
        }

        private static DateTime? GetDateTimeRun(Schedule schedule)
        {
            DateTime runDateTime = schedule.FrecuencyType == FrecuencyType.Once
                ? schedule.DateTime.Value
                : ScheduleRun.GetNextRunRecurring(schedule, schedule.CurrentDate);

            return schedule.EndDate.HasValue == true && runDateTime.CompareTo(schedule.EndDate.Value) > 0
                ? null
                : (DateTime?)runDateTime;
        }
        private static DateTime? GetNextRunDay(Schedule schedule, DateTime dateTime)
        {
            if (schedule.StartDate.HasValue == true &&
                schedule.StartDate.Value.CompareTo(dateTime) > 0) { return null; }

            if (schedule.DailyFrecuencyType.HasValue == false ||
                dateTime.IsDayOfWeekValid(schedule.DaysOfWeek) == false ||
                dateTime.IsDayOfMonthValid(schedule.MonthyType, schedule.MonthyDay) == false ||
                dateTime.IsOccurrenceMonthValid(schedule.MonthyType) == false) { return null; }

            return schedule.DailyFrecuencyType == DailyType.Once
                ? ScheduleRun.GetNextRunDayOnce(schedule, dateTime)
                : ScheduleRun.GetNextRunDayRecurring(schedule, dateTime);
        }
        private static DateTime? GetNextRunDayOnce(Schedule schedule, DateTime dateTime)
        {
            return dateTime.TimeOfDay >= schedule.DailyFrecuencyTime.Value
                ? null
                : (DateTime?)dateTime.Date.Add(schedule.DailyFrecuencyTime.Value);
        }
        private static DateTime? GetNextRunDayRecurring(Schedule schedule, DateTime dateTime)
        {
            IEnumerable<TimeSpan> nextTimes = ScheduleRun.GetNextRunTimeOfDay(schedule, dateTime);

            return nextTimes.Count() == 0 ? null : (DateTime?)dateTime.Date.Add(nextTimes.First());
        }

        private static IEnumerable<TimeSpan> GetNextRunTimeOfDay(Schedule schedule, DateTime dateTime)
        {
            List<TimeSpan> timesGap = schedule.DailyFrecuencyStartTime.Value.GetTimesGap(
                    schedule.DailyFrecuencyEndTime.Value,
                    schedule.DailyFrecuencyEvery,
                    schedule.DailyFrecuencyType.Value);

            return (from time in timesGap
                    where time > dateTime.TimeOfDay
                    orderby time.Ticks
                    select time);
        }

        private static string GetDescription(Schedule schedule, DateTime? runDateTime)
        {
            if (runDateTime == null) { return string.Empty; }

            string description = schedule.FrecuencyType switch
            {
                FrecuencyType.Day => "Occurs every day. Schedule will be used on " + runDateTime.Value.GetDescription(),
                FrecuencyType.Once => "Occurs once. Schedule will be used on " + runDateTime.Value.GetDescription(),
                FrecuencyType.Week => "Occurs every " + schedule.Every + " week on " + ScheduleRun.GetDescriptionDaysOfWeek(schedule),
                FrecuencyType.Month => ScheduleRun.GetDescriptionMonthyType(schedule),
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

            description += schedule.StartDate.HasValue == true
                ? " starting on " + schedule.StartDate.Value.GetDescription()
                : string.Empty;
            description += schedule.EndDate.HasValue == true
                ? " until " + schedule.EndDate.Value.GetDescription()
                : string.Empty;

            return description;
        }
        private static string GetDescriptionDaysOfWeek(Schedule schedule)
        {
            List<string> descriptionDays = new List<string>();

            if (schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Monday) == true) { descriptionDays.Add("monday"); }
            if (schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Tuesday) == true) { descriptionDays.Add("tuesday"); }
            if (schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Wednesday) == true) { descriptionDays.Add("wednesday"); }
            if (schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Thursday) == true) { descriptionDays.Add("thursday"); }
            if (schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Friday) == true) { descriptionDays.Add("friday"); }
            if (schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Saturday) == true) { descriptionDays.Add("saturday"); }
            if (schedule.DaysOfWeek.HasFlag(DaysOfWeekType.Sunday) == true) { descriptionDays.Add("sunday"); }

            return string.Join(", ", descriptionDays.ToArray());
        }
        private static string GetDescriptionMonthyType(Schedule schedule)
        {
            if (schedule.MonthyType == MonthyType.Day) { return "Occurs every " + schedule.Every.ToString() + " month."; }
            else { return "Occurs the " + schedule.MonthyType.ToString().ToLower() + " " + ScheduleRun.GetDescriptionDaysOfWeek(schedule) + " of the very " + schedule.Every.ToString() + " months"; }
        }
    }
}
