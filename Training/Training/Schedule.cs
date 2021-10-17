using System;
using System.Collections.Generic;

namespace Training
{
    public class Schedule
    {
        private readonly List<DayOfWeek> days = new List<DayOfWeek>();

        public Schedule(bool isRecurring, FrecuencyType frecuencyType)
        {
            this.Enabled = true;
            this.IsRecurring = isRecurring;
            this.FrecuencyType = frecuencyType;
        }

        public bool Enabled { get; set; }
        public bool IsRecurring { get; internal set; }

        public DailyType? DailyFrecuencyType { get; set; }

        public DateTime? DateTime { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }

        public List<DayOfWeek> DaysOfWeek { get => this.days; }

        public FrecuencyType FrecuencyType { get; set; }

        public int DailyFrecuencyEvery { get; set; }

        public int Every { get; set; }

        public TimeSpan? DailyFrecuencyTime { get; set; }
        public TimeSpan? DailyFrecuencyEndTime { get; set; }
        public TimeSpan? DailyFrecuencyStartTime { get; set; }

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
