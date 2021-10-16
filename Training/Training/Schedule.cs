using System;
using System.Collections.Generic;

namespace Training
{
    public class Schedule
    {
        private DailyFrecuency frecuency;

        private readonly List<DayOfWeek> days = new List<DayOfWeek>();

        public Schedule() { this.Enabled = true; }

        public bool Enabled { get; set; }

        public DateTime? DateTime { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }

        public List<DayOfWeek> DaysOfWeek { get => this.days; }

        public DailyFrecuency Frecuency
        {
            get
            {
                if (this.Type != null && this.Type.IsRecurring == false) { return null; }

                return this.frecuency;
            }
            set { this.frecuency = value; }
        }

        public int Every { get; set; }

        public string Description { get { return "Occurs " + this.Type.Description + "."; } }

        public FrecuencyToDeleteType Type { get; set; }

        public TimeSpan GetTime() { return this.DateTime.HasValue == true ? this.DateTime.Value.TimeOfDay : new TimeSpan(); }

        public void AddDay(DayOfWeek day)
        {
            if (this.days.Exists(d => d == day) == false)
            {
                this.days.Add(day);
            }
        }
        public void RemoveDay(DayOfWeek day)
        {
            if (this.days.Exists(d => d == day) == true)
            {
                this.days.Remove(day);
            }
        }
    }
}
