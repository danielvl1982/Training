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

        public DateTime DateTime
        {
            get
            {
                DateTime nextDate = this.date;

                switch (schedule.Trigger.Type.Occurs.Type)
                {
                    case RecurringType.day:
                        nextDate.AddDays(schedule.Trigger.Every);
                        break;
                }

                if (schedule.Trigger.DateTime.HasValue == true && nextDate < schedule.Trigger.DateTime.Value)
                {
                    nextDate = schedule.Trigger.DateTime.Value;
                }

                if (schedule.StartDate.HasValue == true && nextDate < schedule.StartDate.Value)
                {
                    nextDate = schedule.StartDate.Value;
                }

                if (schedule.EndDate.HasValue == true && nextDate > schedule.EndDate.Value)
                {
                    nextDate = schedule.EndDate.Value;
                }

                return nextDate;
            }
        }

        public string Description
        {
            get
            {
                string message = this.schedule.Trigger.Description;
                message += "Schedule will be used on " + this.DateTime.ToString("dd/MM/yyyy");
                message += this.schedule.StartDate.HasValue == true ? " starting on " + this.schedule.StartDate.Value.ToString("dd/MM/yyy") : string.Empty;
                message += this.schedule.EndDate.HasValue == true ? " until " + this.schedule.EndDate.Value.ToString("dd/MM/yyy") : string.Empty;

                return message;
            }
        }
    }
}
