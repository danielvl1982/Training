namespace Training
{
    public class TriggerType
    {
        public TriggerType(string name, bool isRecurring)
        {
            this.IsRecurring = isRecurring;
            this.Name = name;
        }

        public bool IsRecurring { get; internal set; }

        public string Description
        {
            get
            {
                return this.IsRecurring == true ? "once" : "every " + this.Occurs.Description;
            }
        }
        public string Name { get; internal set; }

        public TriggerOccur Occurs { get; set; }
    }
}
