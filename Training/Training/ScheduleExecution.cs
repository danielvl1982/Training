using System;

namespace Training
{
    public class ScheduleExecution
    {
        private bool isCalculateNextExecution;

        private readonly DateTime currentDate;

        private DateTime dateTime;

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
                if (this.isCalculateNextExecution == false) { this.CalculateNextExecution(); }

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
                if (this.isCalculateNextExecution == false) { this.CalculateNextExecution(); }

                if (this.DateTime.HasValue == false) { return string.Empty; }

                if (string.IsNullOrEmpty(this.description) == true)
                {
                    this.description = this.schedule.Trigger.Description + "Schedule will be used on " + this.DateTime.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    this.description += this.schedule.StartDate.HasValue == true ? " starting on " + this.schedule.StartDate.Value.ToString("dd/MM/yyy HH:mm:ss") : string.Empty;
                    this.description += this.schedule.EndDate.HasValue == true ? " until " + this.schedule.EndDate.Value.ToString("dd/MM/yyy HH:mm:ss") : string.Empty;
                }

                return this.description;
            }
        }

        private void CalculateNextExecution()
        {
            this.isCalculateNextExecution = true;

            this.Validate();

            this.dateTime = this.GetExecutionDateTime();
        }
        internal DateTime GetExecutionDateTime()
        {
            DateTime nextExecution = this.currentDate.Add(this.schedule.Trigger.Time);

            if (this.schedule.Trigger.DateTime.HasValue == true &&
                nextExecution.CompareTo(this.schedule.Trigger.DateTime.Value) < 0) { nextExecution = this.schedule.Trigger.DateTime.Value; }

            if (this.schedule.StartDate.HasValue == true &&
                nextExecution.CompareTo(this.schedule.StartDate.Value) < 0) { nextExecution = this.schedule.StartDate.Value.Add(this.schedule.Trigger.Time); }

            if (this.schedule.Trigger.Type.IsRecurring == true)
            {
                switch (this.schedule.Trigger.Type.Occurs.Type)
                {
                    case RecurringType.day:
                        nextExecution = nextExecution.AddDays(this.schedule.Trigger.Every);
                        break;
                }
            }

            return nextExecution;
        }
        private void Validate()
        {
            if (this.schedule.Trigger == null) { throw new Exception("Must indicate trigger."); }
        }
    }
}
