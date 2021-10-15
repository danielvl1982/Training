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

        public DateTime? DateTime
        {
            get
            {
                if (this.dateTime.HasValue == false) { this.LoadDateTime(); }

                return this.dateTime;
            }
        }

        public string Description
        {
            get
            {
                if (this.DateTime.HasValue == false) { return string.Empty; }

                if (string.IsNullOrEmpty(this.description) == true)
                {
                    this.description = this.schedule.Trigger.Description + "Schedule will be used on " + this.DateTime.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    this.description += this.schedule.StartDate.HasValue == true
                        ? " starting on " + this.schedule.StartDate.Value.ToString("dd/MM/yyy HH:mm:ss")
                        : string.Empty;
                    this.description += this.schedule.EndDate.HasValue == true
                        ? " until " + this.schedule.EndDate.Value.ToString("dd/MM/yyy HH:mm:ss")
                        : string.Empty;
                }

                return this.description;
            }
        }

        public void SetCurrentDate(DateTime dateTime)
        {
            this.currentDate = dateTime;

            this.dateTime = null;
        }

        private DateTime GetDateTimeExecution()
        {
            DateTime nextExecution = this.currentDate;

            if (this.schedule.StartDate.HasValue == true &&
                nextExecution.CompareTo(this.schedule.StartDate.Value) < 0) { nextExecution = this.schedule.StartDate.Value; }

            if (this.schedule.Trigger.DateTime.HasValue == true &&
                nextExecution.CompareTo(this.schedule.Trigger.DateTime.Value) < 0) { nextExecution = this.schedule.Trigger.DateTime.Value; }

            if (this.schedule.Trigger.Frecuency == null &&
                this.schedule.Trigger.Time.Ticks > 0) { nextExecution = nextExecution.Date.Add(this.schedule.Trigger.Time); }            

            if (this.schedule.Trigger.Type.IsRecurring == true)
            {
                nextExecution = this.GetNextExecutionRecurring(nextExecution);
            }

            return nextExecution;
        }
        private DateTime GetDateTimeIncremented(DateTime dateTime)
        {
            switch (this.schedule.Trigger.Type.Occurs.Type)
            {
                case FrecuencyOccurType.Day:
                    dateTime = dateTime.AddDays(this.schedule.Trigger.Every);
                    break;
                case FrecuencyOccurType.Week:
                    dateTime = this.GetNextExecutionByWeek(dateTime);
                    break;
            }

            return dateTime;
        }
        private DateTime GetNextExecutionRecurring(DateTime nextExecution)
        {
            if (this.schedule.Trigger.Frecuency == null)
            {
                return this.GetDateTimeIncremented(nextExecution);
            }
            else
            {
                DateTime? nextExecutionDay = this.schedule.Trigger.Frecuency.Type.IsRecurring == true
                    ? this.GetNextExecutionDayRecurring(nextExecution)
                    : this.GetNextExecutionDayOnce(nextExecution);

                if (nextExecutionDay.HasValue == true) { return nextExecutionDay.Value; }

                return this.GetNextExecutionRecurring(this.GetDateTimeIncremented(nextExecution).Date);
            }
        }
        private DateTime GetNextExecutionByWeek(DateTime nextExecution)
        {
            DayOfWeek? nextDayOfWeek = nextExecution.NextDayOfWeek(this.schedule.Trigger.DaysOfWeek);

            return nextDayOfWeek.HasValue == false
                ? this.GetNextExecutionByWeek(nextExecution.AddDays(this.schedule.Trigger.Every * 7))
                : nextExecution.GetDateTimeDayOfWeek(nextDayOfWeek.Value);
        }

        private DateTime? GetNextExecutionDayOnce(DateTime dateTime)
        {
            return dateTime.TimeOfDay < this.schedule.Trigger.Frecuency.Time.Value
                ? (DateTime?)dateTime.Date.Add(this.schedule.Trigger.Frecuency.Time.Value)
                : null;
        }
        private DateTime? GetNextExecutionDayRecurring(DateTime dateTime)
        {
            if (this.schedule.Trigger.DaysOfWeek.Exists(d => d == dateTime.DayOfWeek) == false ||
                dateTime.TimeOfDay > this.schedule.Trigger.Frecuency.EndTime.Value) { return null; }

            IEnumerable<TimeSpan> nextTime = GetNextExecutionTimeOfDay(dateTime);

            return nextTime.Count() == 0 ? null : (DateTime?)dateTime.Date.Add(nextTime.First());
        }

        private IEnumerable<TimeSpan> GetNextExecutionTimeOfDay(DateTime dateTime)
        {
            List<TimeSpan> timesGap = this.schedule.Trigger.Frecuency.StartTime.Value.GetTimesGap(
                    this.schedule.Trigger.Frecuency.EndTime.Value,
                    this.schedule.Trigger.Frecuency.Every,
                    this.schedule.Trigger.Frecuency.Type.Occurs);

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
