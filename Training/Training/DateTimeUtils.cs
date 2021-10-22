using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public static class DateTimeUtils
    {
        public static DateTime AddWeeks(this DateTime dateTime, int incrementWeeks)
        {
            return dateTime.AddDays(incrementWeeks * 7).GetDateTimeDayOfWeek(DayOfWeek.Monday).Date;
        }
        public static DateTime GetDateTimeDayOfMonth(this DateTime dateTime, DayOfMonth dayOfMonth)
        {
            //while (dateTime.DayOfWeek != dayOfMonth)
            //{
            //    dateTime = dateTime.DayOfWeek > dayOfMonth && dayOfMonth != DayOfWeek.Sunday ? dateTime.AddDays(-1) : dateTime.AddDays(+1);
            //}

            return dateTime;
        }
        public static DateTime GetDateTimeDayOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            while (dateTime.DayOfWeek != dayOfWeek)
            {
                dateTime = dateTime.DayOfWeek > dayOfWeek && dayOfWeek != DayOfWeek.Sunday ? dateTime.AddDays(-1) : dateTime.AddDays(+1);
            }

            return dateTime;
        }

        public static DayOfWeek? NextDayOfWeek(this DateTime dateTime, List<DayOfWeek> daysOfWeek)
        {
            IEnumerable<DayOfWeek> nextDaysOfWeek = (from day in daysOfWeek
                                                     where ((int)day + 6) % 7 > ((int)dateTime.DayOfWeek + 6) % 7
                                                     select day).OrderBy(d => ((int)d + 6) % 7);

            return nextDaysOfWeek.Count() == 0 ? null : (DayOfWeek?)nextDaysOfWeek.First();
        }
        public static DayOfWeek? NextDayOfWeek(this DateTime dateTime, DayOfMonth dayOfMonth)
        {
            return dateTime.NextDayOfWeek(GetDayOfMonthToDaysOfWeek(dayOfMonth));
        }

        public static TimeSpan GetTime(this DateTime? dateTime) { return dateTime.HasValue == true ? dateTime.Value.TimeOfDay : new TimeSpan(); }

        public static List<TimeSpan> GetTimesGap(this TimeSpan startTime, TimeSpan endTime, int gap, DailyType dailyType)
        {
            List<TimeSpan> times = new List<TimeSpan>();

            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            switch (dailyType)
            {
                case DailyType.Hour:
                    hours = gap;
                    break;
                case DailyType.Minute:
                    minutes = gap;
                    break;
                case DailyType.Second:
                    seconds = gap;
                    break;
            }

            TimeSpan time = startTime;

            do
            {
                times.Add(time);

                time = time.Add(new TimeSpan(hours, minutes, seconds));
            } while (time.CompareTo(endTime) <= 0);

            return times;
        }

        private static List<DayOfWeek> GetDayOfMonthToDaysOfWeek(DayOfMonth dayOfMonth)
        {
            List<DayOfWeek> daysOfWeek = new List<DayOfWeek>();

            if (dayOfMonth == DayOfMonth.Monday) { daysOfWeek.Add(DayOfWeek.Monday); }
            if (dayOfMonth == DayOfMonth.Tuesday) { daysOfWeek.Add(DayOfWeek.Tuesday); }
            if (dayOfMonth == DayOfMonth.Wednesday) { daysOfWeek.Add(DayOfWeek.Wednesday); }
            if (dayOfMonth == DayOfMonth.Thursday) { daysOfWeek.Add(DayOfWeek.Thursday); }
            if (dayOfMonth == DayOfMonth.Friday) { daysOfWeek.Add(DayOfWeek.Friday); }
            if (dayOfMonth == DayOfMonth.Saturday) { daysOfWeek.Add(DayOfWeek.Saturday); }
            if (dayOfMonth == DayOfMonth.Sunday) { daysOfWeek.Add(DayOfWeek.Sunday); }

            return daysOfWeek;
        }
    }
}
