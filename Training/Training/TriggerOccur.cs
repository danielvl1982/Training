using System;

namespace Training
{
    public enum RecurringType
    {
        day = 0
    }

    public class TriggerOccur
    {
        public TriggerOccur(string name, RecurringType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public RecurringType Type { get; private set; }

        public string Description
        {
            get
            {
                string message = string.Empty;

                switch (this.Type)
                {
                    case RecurringType.day:
                        message = "day";
                        break;
                }

                return message;
            }
        }

        public string Name { get; private set; }
    }
}