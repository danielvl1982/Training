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
                if (this.schedule.IsRecurring == true)
                {
                    this.description = "Occurs every" + this.schedule.FrecuencyType switch
                    {
                        FrecuencyType.Day => " day.",
                        FrecuencyType.Week => " week.",
                        _ => string.Empty,
                    };
                }
                else
                {
                    this.description += "Occurs once.";
                }

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

            if (this.schedule.IsRecurring == true)
            {
                nextExecution = this.GetNextExecutionRecurring(nextExecution);
            }

            return nextExecution;
        }
        private DateTime GetDateTimeIncremented(DateTime dateTime)
        {
            switch (this.schedule.FrecuencyType)
            {
                case FrecuencyType.Day:
                    dateTime = dateTime.AddDays(this.schedule.Every);
                    break;
                case FrecuencyType.Week:
                    dateTime = this.GetNextExecutionByWeek(dateTime);
                    break;
            }

            return dateTime;
        }
        private DateTime GetNextExecutionRecurring(DateTime nextExecution)
        {
            if (this.schedule.DailyFrecuencyType.HasValue == true)
            {
                DateTime? nextExecutionDay = this.schedule.DailyFrecuencyType == DailyType.Once
                    ? this.GetNextExecutionDayOnce(nextExecution)
                    : this.GetNextExecutionDayRecurring(nextExecution);

                if (nextExecutionDay.HasValue == true) { return nextExecutionDay.Value; }

                return this.GetNextExecutionRecurring(this.GetDateTimeIncremented(nextExecution).Date);
            }
            else
            {
                return this.GetDateTimeIncremented(nextExecution);
            }
        }
        private DateTime GetNextExecutionByWeek(DateTime nextExecution)
        {
            DayOfWeek? nextDayOfWeek = nextExecution.NextDayOfWeek(this.schedule.DaysOfWeek);

            return nextDayOfWeek.HasValue == true
                ? nextExecution.GetDateTimeDayOfWeek(nextDayOfWeek.Value)
                : this.GetNextExecutionRecurring(nextExecution.AddWeeks(this.schedule.Every).Date);
        }

        private DateTime? GetNextExecutionDayOnce(DateTime dateTime)
        {
            return dateTime.TimeOfDay < this.schedule.DailyFrecuencyTime.Value
                ? (DateTime?)dateTime.Date.Add(this.schedule.DailyFrecuencyTime.Value)
                : null;
        }
        private DateTime? GetNextExecutionDayRecurring(DateTime dateTime)
        {
            if (this.schedule.DaysOfWeek.Exists(d => d == dateTime.DayOfWeek) == false ||
                dateTime.TimeOfDay > this.schedule.DailyFrecuencyEndTime.Value) { return null; }

            IEnumerable<TimeSpan> nextTime = GetNextExecutionTimeOfDay(dateTime);

            return nextTime.Count() == 0 ? null : (DateTime?)dateTime.Date.Add(nextTime.First());
        }

        private IEnumerable<TimeSpan> GetNextExecutionTimeOfDay(DateTime dateTime)
        {
            List<TimeSpan> timesGap = this.schedule.DailyFrecuencyStartTime.Value.GetTimesGap(
                    this.schedule.DailyFrecuencyEndTime.Value,
                    this.schedule.DailyFrecuencyEvery,
                    this.schedule.DailyFrecuencyType.Value);

            return (from time in timesGap
                    where time > dateTime.TimeOfDay
                    select time).OrderBy(t => t.Ticks);
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
