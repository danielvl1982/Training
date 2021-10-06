namespace Training
{
    public class TriggerType
    {
        public TriggerType() { }

        public bool IsRecurring { get; internal set; }

        public string Name { get; internal set; }

        public TriggerOccur Occurs { get; internal set; }
    }
}
