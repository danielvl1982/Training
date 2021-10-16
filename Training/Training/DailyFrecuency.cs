using System;

namespace Training
{
    public class DailyFrecuency
    {
        public DailyFrecuency()
        {
        }

        public FrecuencyToDeleteType Type { get; set; }

        public TimeSpan? Time { get; set; }
        public TimeSpan? EndTime { get; set; }
        public TimeSpan? StartTime { get; set; }

        public int Every { get; set; }
    }
}
