using System;
using System.Collections.Generic;

namespace Training
{
    public class Schedule
    {
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

        public DaysOfWeekType DaysOfWeek { get; set; }
        
        public FrecuencyType FrecuencyType { get; set; }

        public int DailyFrecuencyEvery { get; set; }
        public int Every { get; set; }
        public int MonthyDay { get; set; }

        public MonthyType MonthyType { get; set; }

        public TimeSpan? DailyFrecuencyTime { get; set; }
        public TimeSpan? DailyFrecuencyEndTime { get; set; }
        public TimeSpan? DailyFrecuencyStartTime { get; set; }
    }
}
