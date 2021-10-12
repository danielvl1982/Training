using System;

namespace Training
{
    public class DailyFrecuency
    {
        public DailyFrecuency()
        {
        }

        public DailyFrecuencyType Type { get; set; }

        public DateTime? Time { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? StartTime { get; set; }

        public int Every { get; set; }
    }
}
