using System;

namespace Training
{
    public class Schedule
    {
        private DateTime? currentDate;
        private DateTime? executionTime;

        public Schedule() { }

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

        public Trigger Trigger { get; set; }

        public Execution GetNextExecution()
        {
            if (this.Enabled == false) { throw new Exception("El planificador está deshabilitado."); }
            if (this.CurrentDate.HasValue == false) { throw new Exception("Debe indicar la fecha actual."); }
            if (this.Trigger == null) { throw new Exception("El planificador no tiene asignado un desencadenador."); }

            return new Execution(this, this.executionTime.HasValue == true ? this.executionTime.Value : this.CurrentDate.Value);
        }
    }
}
