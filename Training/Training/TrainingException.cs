using System;

namespace Training
{
    public class TrainingException : Exception
    {
        public TrainingException() : base()
        {
        }
        public TrainingException(string? message) : base(message)
        {
        }
    }
}
