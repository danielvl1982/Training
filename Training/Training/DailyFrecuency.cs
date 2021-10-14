using System;
using System.Collections.Generic;

namespace Training
{
    public class DailyFrecuency
    {
        public DailyFrecuency()
        {
        }

        public DailyFrecuencyType Type { get; set; }

        public TimeSpan? Time { get; set; }
        public TimeSpan? EndTime { get; set; }
        public TimeSpan? StartTime { get; set; }

        public List<TimeSpan> Gap
        {
            get
            {
                List<TimeSpan> times = new List<TimeSpan>();

                if (this.StartTime.HasValue == true &&
                    this.EndTime.HasValue == true)
                {
                    int hours = 0;
                    int minutes = 0;
                    int seconds = 0;

                    switch (this.Type.Occurs.Type)
                    {
                        case DailyOccurType.Hour:
                            hours = Every;
                            break;
                        case DailyOccurType.Minute:
                            minutes = Every;
                            break;
                        case DailyOccurType.Second:
                            seconds = Every;
                            break;
                    }

                    TimeSpan time = this.StartTime.Value;

                    do
                    {
                        times.Add(time);

                        time = time.Add(new TimeSpan(hours, minutes, seconds));
                    } while (time.CompareTo(this.EndTime.Value) <= 0);
                }

                return times;
            }
        }

        public int Every { get; set; }
    }
}
