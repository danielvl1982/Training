using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class Schedule
    {
        private DateTime? currentDate;
        private DateTime? executionTime;

        public Schedule()
        {
            this.Enabled = true;
            this.Triggers = new List<Trigger>();
        }

        public bool Enabled { get; set; }

        public DateTime? CurrentDate
        {
            get
            {
                return this.currentDate;
            }
            set
            {
                this.currentDate = value;

                this.executionTime = null;
            }
        }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }

        public List<Trigger> Triggers { get; set; }

        public Execution GetNextExecution()
        {
            this.Validate();

            if (this.Enabled == false) { throw new Exception("El planificador está deshabilitado."); }
            if (this.CurrentDate.HasValue == false) { throw new Exception("Debe indicar la fecha actual."); }

            if (GetTriggersEnabled().Count == 0) { throw new Exception("El planificador tiene sus desecadenadores deshabilitados."); }

            return new Execution(this, this.executionTime.HasValue == true ? this.executionTime.Value : this.CurrentDate.Value);
        }

        public void AddTrigger(Trigger trigger)
        {
            this.Triggers.Add(trigger);
        }
        public void RemoveTrigger(Trigger trigger)
        {
            this.Triggers.Remove(trigger);
        }
        public void Validate()
        {
            if (this.Triggers == null || this.Triggers.Count == 0) { throw new Exception("Debe indicar al menos un desencadenador."); }
        }

        internal List<Trigger> GetTriggersEnabled()
        {
            return (from trigger in this.Triggers
                    where trigger.Enabled == true
                    select trigger).ToList();
        }
    }
}
