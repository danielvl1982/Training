using System;

namespace Training
{
    public struct ScheduleRunData
    {
        public ScheduleRunData(DateTime? runDateTime, string description)
        {
            this.RunDateTime = runDateTime;

            this.Description = description;
        }

        public DateTime? RunDateTime { get; set; }

        public string Description { get; set; }
    }
}
