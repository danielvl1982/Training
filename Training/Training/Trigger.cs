using System;

namespace Training
{
    public class Trigger
    {
        public Trigger() { this.Enabled = true; }

        public bool Enabled { get; set; }

        public DateTime? DateTime { get; set; }

        public int Every { get; set; }

        public string Description
        {
            get
            {
                string message = "Occurs " + this.Type.Description + ".";

                return message;
            }
        }

        public TriggerType Type { get; set; }

        public void Validate()
        {
            if (this.Type.IsRecurring == false &&
                this.DateTime.HasValue == false)
            {
                throw new Exception("Debe indicar la fecha de ejecución del del desencadenador.");
            }
        }

        internal DateTime? GetNextExecution(DateTime inputDate)
        {
            if (this.Enabled == false) { throw new Exception("The trigger is disable."); }

            this.Validate();

            DateTime? nextExecution = null;

            if (this.DateTime.HasValue == true && inputDate.CompareTo(this.DateTime.Value) <= 0) { return this.DateTime; }

            if (this.Type.IsRecurring == true)
            {
                nextExecution = inputDate;

                switch (this.Type.Occurs.Type)
                {
                    case RecurringType.day:
                        nextExecution = nextExecution.Value.AddDays(this.Every);
                        break;
                    case RecurringType.week:
                        nextExecution = nextExecution.Value.AddDays(7 * this.Every);
                        break;
                }
            }

            return nextExecution;
        }
    }
}