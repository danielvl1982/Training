using System;

namespace Training
{
    public class ScheduleException : Exception
    {
        public const string ScheduleNull = "Must instantiate schedule.";
        public const string EndDateMustGreaterStartDate = "End date must be greater to start date.";
        public const string DateTimeMustGreaterStarDate = "DateTime must be greater to start date.";
        public const string CurrentDateMustLesserEndDate = "Current date must be lesser to end date.";
        public const string DateTimeMustLesserEndDate = "DateTime must be lesser to end date.";
        public const string CurrentDateMustLesserDateTime = "Current date must be lesser to datetime.";
        public const string DateTimeNotIndicate = "Occurs once must indicate the datetime.";
        public const string EveryIndicate = "Every must be 0.";
        public const string DateTimeIndicate = "Occurs recurring mustn’t indicate the datetime.";
        public const string EveryNotIndicate = "Every must be greater to 0.";
        public const string WeeklyMustIndicateDaysWeek = "Occurs weekly must indicate the days of the week.";
        public const string MonthyDayMustIndicateDayMonth = "Occurs monthy day must indicate the day of the month.";
        public const string MonthyDayMustGreater31 = "Occurs monthy day mustn’t be greater to 31.";
        public const string MonthyNotMustIndicateDayWeek = "Occurs monthy mustn’t indicate the days of the week.";
        public const string MonthyDayNotMustIndicateDayMonth = "Occurs monthy day mustn’t indicate the day of the month.";
        public const string MonthyMustIndicateDayWeek = "Occurs monthy must indicate the days of the week.";
        public const string IndicateOnceTime = "Must indicate Occurs once at time.";
        public const string OccursEvery0 = "Occurs every must be 0.";
        public const string IndicateStarting = "Must indicate starting at.";
        public const string IndicateEnd = "Must indicate end at.";
        public const string EndGreaterStarting = "End at must be greater to stating at.";
        public const string OccursMustGreater0 = "Occurs every must be greater to 0.";

        public ScheduleException(string message) : base(message) { }
    }
}
