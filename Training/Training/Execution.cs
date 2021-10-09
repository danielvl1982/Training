using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class Execution
    {
        private bool isCalculateNextExecution;

        private readonly DateTime inputDate;

        private DateTime? dateTime;

        private readonly Schedule schedule;

        private string description;

        internal Execution(Schedule schedule, DateTime inputDate)
        {
            this.inputDate = inputDate;

            this.schedule = schedule;
        }

        public DateTime? DateTime
        {
            get
            {
                if (this.isCalculateNextExecution == false) { this.CalculateNextExecution(); }

                return this.dateTime;
            }
            private set { this.dateTime = value; }
        }

        public string Description
        {
            get
            {
                if (this.isCalculateNextExecution == false) { this.CalculateNextExecution(); }

                return this.description;
            }
            private set { this.description = value; }
        }

        private void CalculateNextExecution()
        {
            this.isCalculateNextExecution = true;

            DateTime nextExecution = this.inputDate;

            if (this.schedule.StartDate.HasValue == true && inputDate.CompareTo(this.schedule.StartDate.Value) < 0) { nextExecution = schedule.StartDate.Value; }
            if (this.schedule.EndDate.HasValue == true && inputDate.CompareTo(this.schedule.EndDate.Value) > 0) { return; }

            //I must optimize the iterations query. How wich linq?.
            List<Trigger> triggers = (from trigger in this.schedule.GetTriggersEnabled()
                                      where trigger.GetNextExecution(nextExecution).HasValue == true
                                      select trigger).OrderByDescending(t => t.GetNextExecution(nextExecution).Value).ToList();

            if (triggers.Count == 0) { return; }

            this.DateTime = triggers[0].GetNextExecution(nextExecution).Value;

            this.Description = triggers[0].Description + "Schedule will be used on " + this.DateTime.Value.ToString("dd/MM/yyyy HH:mm:ss");
            this.Description += this.schedule.StartDate.HasValue == true ? " starting on " + this.schedule.StartDate.Value.ToString("dd/MM/yyy HH:mm:ss") : string.Empty;
            this.Description += this.schedule.EndDate.HasValue == true ? " until " + this.schedule.EndDate.Value.ToString("dd/MM/yyy HH:mm:ss") : string.Empty;
        }
    }
}
