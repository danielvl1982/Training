using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class ScheduleExecution
    {
        private List<DateTime> dateTimes;

        private readonly DateTime currentDate;

        private readonly Schedule schedule;

        private string description;

        public ScheduleExecution(Schedule schedule, DateTime currentDate)
        {
            this.currentDate = currentDate;

            this.schedule = schedule;
        }

        public List<DateTime> DateTimes
        {
            get
            {
                if (this.dateTimes == null)
                {
                    this.dateTimes = new List<DateTime>();

                    this.LoadDateTimes();
                }

                return this.dateTimes;
            }
        }

        public string Description
        {
            get
            {
                if (this.DateTimes.Count == 0) { return string.Empty; }

                if (string.IsNullOrEmpty(this.description) == true)
                {
                    this.description = this.schedule.Trigger.Description + "Schedule will be used on " + this.DateTimes[0].ToString("dd/MM/yyyy HH:mm:ss");
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

        private DateTime GetInitialDateTime()
        {
            DateTime dateTime = this.currentDate;

            if (this.schedule.Trigger.Frecuency == null)
            {
                if (this.schedule.Trigger.Time.Ticks > 0) { dateTime.Date.Add(this.schedule.Trigger.Time); }
            }
            else
            {
                if (this.schedule.Trigger.Frecuency.Type.IsRecurring == true) { dateTime.Date.Add(this.schedule.Trigger.Frecuency.StartTime.Value); }
                else { dateTime.Date.Add(this.schedule.Trigger.Frecuency.Time.Value); }
            }

            return dateTime;
        }
        private DateTime GetNextExecutionByWeek(DateTime nextExecution)
        {
            DayOfWeek? nextDayOfWeek = nextExecution.NextDayOfWeek(this.schedule.Trigger.Days);

            return nextDayOfWeek.HasValue == false
                ? this.GetNextExecutionByWeek(nextExecution.AddDays(this.schedule.Trigger.Every * 7))
                : nextExecution.DateTimeDayOfWeek(nextDayOfWeek.Value);
        }

        private List<DateTime> GetExecutions()
        {
            DateTime nextExecution = this.GetInitialDateTime();

            if (this.schedule.Trigger.DateTime.HasValue == true &&
                nextExecution.CompareTo(this.schedule.Trigger.DateTime.Value) < 0) { nextExecution = this.schedule.Trigger.DateTime.Value; }

            if (this.schedule.StartDate.HasValue == true &&
                nextExecution.CompareTo(this.schedule.StartDate.Value) < 0) { nextExecution = this.schedule.StartDate.Value.Add(this.schedule.Trigger.Time); }

            return this.GetNextExecutions(nextExecution);
        }
        private List<DateTime> GetNextExecutions(DateTime nextExecution)
        {
            if (this.schedule.Trigger.Type.IsRecurring == true)
            {
                switch (this.schedule.Trigger.Type.Occurs.Type)
                {
                    case TriggerOccurType.Day:
                        nextExecution = nextExecution.AddDays(this.schedule.Trigger.Every);
                        break;
                    case TriggerOccurType.Week:
                        nextExecution = this.GetNextExecutionByWeek(nextExecution);
                        break;
                }

                if (this.schedule.Trigger.Frecuency != null &&
                    this.schedule.Trigger.Frecuency.Type.IsRecurring == true) { return this.GetNextExecutionsByFrecuency(nextExecution.Date); }
            }

            return new List<DateTime> { nextExecution };
        }
        private List<DateTime> GetNextExecutionsByFrecuency(DateTime nextExecution)
        {
            List<DateTime> executions = new List<DateTime>();

            do
            {
                executions.Add(nextExecution);

                switch (this.schedule.Trigger.Frecuency.Type.Occurs.Type)
                {
                    case DailyOccurType.Hour:
                        nextExecution.AddHours(this.schedule.Trigger.Frecuency.Every);
                        break;
                    case DailyOccurType.Minute:
                        nextExecution.AddMinutes(this.schedule.Trigger.Frecuency.Every);
                        break;
                    case DailyOccurType.Second:
                        nextExecution.AddSeconds(this.schedule.Trigger.Frecuency.Every);
                        break;
                }

            } while (nextExecution.TimeOfDay >= this.schedule.Trigger.Frecuency.EndTime.Value);

            return executions;
        }

        private void LoadDateTimes()
        {
            ScheduleManager.Validate(this.schedule);

            this.dateTimes = this.schedule.EndDate.HasValue == true
                ? (from datetime in this.GetExecutions()
                   where datetime.CompareTo(this.schedule.EndDate.Value) > 0
                   select datetime).ToList()
                : (from datetime in this.GetExecutions()
                   select datetime).ToList();
        }
    }
}
