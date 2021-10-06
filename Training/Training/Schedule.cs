using System;

namespace Training
{
    public class Schedule
    {
        public Schedule(Trigger trigger) { this.Trigger = trigger; }

        public Trigger Trigger { get; private set; }

        public void CalculateNextDate(DateTime currentDate)
        {
            //Devolver objeto que contenga los datos necesarios para el output            
            //Aumentar la fecha para siguientes iteraciones sobre calculate nexr date
        }
    }
}
