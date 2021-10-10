using System;

namespace Training
{
    public class Trigger
    {
        public Trigger() { this.Enabled = true; }

        public bool Enabled { get; set; }

        public DateTime? DateTime { get; set; }

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
    }
}