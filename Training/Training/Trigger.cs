using System;
using System.Collections.Generic;

namespace Training
{
    public class Trigger
    {
        private readonly List<DayOfWeek> days = new List<DayOfWeek>();

        public Trigger() { this.Enabled = true; }

        public bool Enabled { get; set; }

        public DateTime? DateTime { get; set; }

        public List<DayOfWeek> Days { get => this.days; }

        public DailyFrecuency Frecuency { get; set; }

        public int Every { get; set; }

        public string Description
        {
            get
            {
                string message = "Occurs " + this.Type.Description + ".";

                return message;
            }
        }

        public TimeSpan Time { get { return this.DateTime.HasValue == true ? this.DateTime.Value.TimeOfDay : new TimeSpan(); } }

        public TriggerType Type { get; set; }

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