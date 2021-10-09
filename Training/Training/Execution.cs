using System;

namespace Training
{
    public class Execution
    {
        private DateTime date;

        private Schedule schedule;

        internal Execution(Schedule schedule, DateTime date)
        {
            this.date = date;

            this.schedule = schedule;
        }

        public DateTime DateTime { get; private set; }

        public string Description { get; private set; }

        private void CalculateNextExecution()
        {
            //DateTime nextDate = this.date;

            //switch (schedule.Triggers.Type.Occurs.Type)
            //{
            //    case RecurringType.day:
            //        nextDate.AddDays(schedule.Triggers.Every);
            //        break;
            //}

            //if (schedule.Triggers.DateTime.HasValue == true && nextDate < schedule.Triggers.DateTime.Value)
            //{
            //    nextDate = schedule.Triggers.DateTime.Value;
            //}

            //if (schedule.StartDate.HasValue == true && nextDate < schedule.StartDate.Value)
            //{
            //    nextDate = schedule.StartDate.Value;
            //}

            //if (schedule.EndDate.HasValue == true && nextDate > schedule.EndDate.Value)
            //{
            //    nextDate = schedule.EndDate.Value;
            //}

            //this.DateTime = nextDate;

            //string message = this.schedule.Triggers.Description;
            //message += "Schedule will be used on " + this.DateTime.ToString("dd/MM/yyyy");
            //message += this.schedule.StartDate.HasValue == true ? " starting on " + this.schedule.StartDate.Value.ToString("dd/MM/yyy") : string.Empty;
            //message += this.schedule.EndDate.HasValue == true ? " until " + this.schedule.EndDate.Value.ToString("dd/MM/yyy") : string.Empty;

            //this.Description = message;
        }
    }
}
