using System;

namespace Training
{
    public class Trigger
    {
        public Trigger() { }

        public bool Enabled { get; set; }

        public DateTime EndDate { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime StartDate { get; set; }

        public int Every
        {
            get;
            internal set;
        }

        public TriggerType Type { get; set; }
    }
}