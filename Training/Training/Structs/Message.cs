namespace Training
{
    public struct Message
    {
        public string CultureName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
