using System;

namespace Training
{
    public class Trigger
    {
        public Trigger() { }

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

        public TriggerType Type { get; set; }
    }
}