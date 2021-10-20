using System;
using System.Collections.Generic;

namespace Training
{
    public class Schedule
    {
        private readonly List<DayOfWeek> days = new List<DayOfWeek>();

        public Schedule(FrecuencyType frecuencyType)
        {
            this.Enabled = true;
            this.FrecuencyType = frecuencyType;
        }

        public bool Enabled { get; set; }

        public DailyType? DailyFrecuencyType { get; set; }

        public DateTime? DateTime { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }

        public DayOfMonth DayOfMonth { get; set; }
        
        public List<DayOfWeek> DaysOfWeek { get => this.days; }

        public FrecuencyType FrecuencyType { get; set; }

        public int DailyFrecuencyEvery { get; set; }
        public int Every { get; set; }
        public int MonthyDay { get; set; }
        public int MonthyFrecuencyEvery { get; set; }

        public MonthyType MonthyType { get; set; }

        public TimeSpan? DailyFrecuencyTime { get; set; }
        public TimeSpan? DailyFrecuencyEndTime { get; set; }
        public TimeSpan? DailyFrecuencyStartTime { get; set; }
    }
}
