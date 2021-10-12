using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class ScheduleExecution
    {
        private bool isLoadFields;

        private DateTime dateTime;

        private readonly DateTime currentDate;

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
                if (this.isLoadFields == false) { this.LoadFields(); }

                if (this.schedule.Trigger.Enabled == false ||
                   (this.schedule.EndDate.HasValue == true &&
                    this.dateTime.CompareTo(this.schedule.EndDate.Value) > 0)) { return null; }

                return this.dateTime;
            }
        }

        public string Description
        {
            get
            {
                if (this.isLoadFields == false) { this.LoadFields(); }

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

        private DateTime GetExecution()
        {
            DateTime nextExecution = this.currentDate.Add(this.schedule.Trigger.Time);

            if (this.schedule.Trigger.DateTime.HasValue == true &&
                nextExecution.CompareTo(this.schedule.Trigger.DateTime.Value) < 0) { nextExecution = this.schedule.Trigger.DateTime.Value; }

            if (this.schedule.StartDate.HasValue == true &&
                nextExecution.CompareTo(this.schedule.StartDate.Value) < 0) { nextExecution = this.schedule.StartDate.Value.Add(this.schedule.Trigger.Time); }

            return this.GetNextExecution(nextExecution);
        }
        private DateTime GetNextExecution(DateTime nextExecution)
        {
            if (this.schedule.Trigger.Type.IsRecurring == false) { return nextExecution; }

            switch (this.schedule.Trigger.Type.Occurs.Type)
            {
                case TriggerOccurType.Day:
                    nextExecution = nextExecution.AddDays(this.schedule.Trigger.Every);
                    break;
                case TriggerOccurType.Week:
                    nextExecution = this.GetNextExecutionByWeek(nextExecution);
                    break;
            }

            return this.GetNextExecutionByFrecuency(nextExecution);
        }
        private DateTime GetNextExecutionByFrecuency(DateTime nextExecution)
        {
            if (this.schedule.Trigger.Frecuency == null) { return nextExecution; }

            if (this.schedule.Trigger.Frecuency.Type.IsRecurring == true)
            {

            }
            else
            {
                nextExecution = nextExecution.TimeOfDay > this.schedule.Trigger.Frecuency.Time.Value.TimeOfDay
                    ? this.GetNextExecution(nextExecution)
                    : nextExecution.Date.Add(this.schedule.Trigger.Frecuency.Time.Value.TimeOfDay);
            }

            return nextExecution;
        }
        private DateTime GetNextExecutionByWeek(DateTime nextExecution)
        {
            DayOfWeek? nextDayOfWeek = nextExecution.NextDayOfWeek(this.schedule.Trigger.Days);

            return nextDayOfWeek.HasValue == false
                ? this.GetNextExecutionByWeek(nextExecution.AddDays(this.schedule.Trigger.Every * 7))
                : nextExecution.DateTimeDayOfWeek(nextDayOfWeek.Value);
        }

        private void LoadFields()
        {
            this.isLoadFields = true;

            ScheduleManager.Validate(this.schedule);

            this.dateTime = this.GetExecution();
        }
    }
}
