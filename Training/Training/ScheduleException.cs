using System;

namespace Training
{
    public class ScheduleException : Exception
    {
        public ScheduleException() : base()
        {
        }
        public ScheduleException(string? message) : base(message)
        {
        }
    }
}
