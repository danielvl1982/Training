using System;

namespace Training
{
    public class Schedule
    {
        public Schedule() { }

        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }

        public Trigger Trigger { get; set; }
    }
}
