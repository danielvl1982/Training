using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public static class DateTimeUtils
    {
        public static bool IsDayOfWeekValid(this DateTime dateTime, DaysOfWeekType daysOfWeekType)
        {
            List<DayOfWeek> daysOfWeeek = DateTimeUtils.GetDaysOfWeek(daysOfWeekType);

            return daysOfWeeek.Count == 0 || daysOfWeeek.Exists(d => d == dateTime.DayOfWeek);
        }
        public static bool IsMonthyDayValid(this DateTime dateTime, MonthyType monthyType, int day)
        {
            return monthyType != MonthyType.Day || (monthyType == MonthyType.Day && dateTime.GetDayOfMonth(day) == dateTime.Day);
        }
        public static bool IsWeekValid(this DateTime dateTime, MonthyType monthyType)
        {
            return monthyType == MonthyType.None || monthyType == MonthyType.Day || dateTime.GetDayOfMonth(monthyType, dateTime.DayOfWeek) == dateTime.Date;
        }

        public static DateTime AddWeeks(this DateTime dateTime, int incrementWeeks)
        {
            return dateTime.AddDays(incrementWeeks * 7).GetDayOfWeek(DayOfWeek.Monday).Date;
        }
        public static DateTime GetDayOfMonth(this DateTime dateTime, MonthyType monthyType, DayOfWeek dayOfWeek)
        {
            var days = Enumerable.Range(1, DateTime.DaysInMonth(dateTime.Year, dateTime.Month)).Select(day => new DateTime(dateTime.Year, dateTime.Month, day));

            var weekdays = from day in days
                           where day.DayOfWeek == dayOfWeek
                           select day;

            return monthyType switch
            {
                MonthyType.Last => weekdays.OrderByDescending(d => d.Day).ElementAt(0),
                _ => weekdays.OrderBy(d => d.Day).ElementAt((int)monthyType - 1),
            };
        }
        public static DateTime GetDayOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            while (dateTime.DayOfWeek != dayOfWeek)
            {
                dateTime = dateTime.DayOfWeek > dayOfWeek && dayOfWeek != DayOfWeek.Sunday ? dateTime.AddDays(-1) : dateTime.AddDays(+1);
            }

            return dateTime;
        }

        public static DateTime? AddMonths(this DateTime dateTime, int dayOfMonth, int increment)
        {
            DateTime dateTimeIncremented = dateTime.AddMonths(increment);

            DateTime date = new DateTime(dateTimeIncremented.Year, dateTimeIncremented.Month, dateTimeIncremented.GetDayOfMonth(dayOfMonth));

            return date <= dateTime
                ? null
                : (DateTime?)date;
        }
        public static DateTime? AddMonths(this DateTime dateTime, MonthyType monthyType, DaysOfWeekType daysOfWeek, int increment)
        {
            List<DayOfWeek> days = DateTimeUtils.GetDaysOfWeek(daysOfWeek);

            DateTime dateTimeIncremented = dateTime.AddMonths(increment);

            List<DateTime> dates = new List<DateTime>();

            days.ForEach(d => dates.Add(dateTimeIncremented.GetDayOfMonth(monthyType, d)));

            var validDates = (from date in dates
                              where date > dateTime
                              select date);

            return validDates.Count() == 0
                ? null
                : (DateTime?)validDates.OrderBy(d => d.Ticks).First();
        }

        public static DayOfWeek? NextDayOfWeek(this DateTime dateTime, List<DayOfWeek> daysOfWeek)
        {
            IEnumerable<DayOfWeek> nextDaysOfWeek = (from day in daysOfWeek
                                                     where ((int)day + 6) % 7 > ((int)dateTime.DayOfWeek + 6) % 7
                                                     orderby ((int)day + 6) % 7
                                                     select day);

            return nextDaysOfWeek.Count() == 0 ? null : (DayOfWeek?)nextDaysOfWeek.First();
        }
        public static DayOfWeek? NextDayOfWeek(this DateTime dateTime, DaysOfWeekType daysOfWeekType)
        {
            return dateTime.NextDayOfWeek(DateTimeUtils.GetDaysOfWeek(daysOfWeekType));
        }

        public static int GetDayOfMonth(this DateTime dateTime, int dayOfMonth)
        {
            int daysOfMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

            return daysOfMonth < dayOfMonth ? dayOfMonth = daysOfMonth : dayOfMonth;
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

        private static List<DayOfWeek> GetDaysOfWeek(DaysOfWeekType daysOfWeekType)
        {
            List<DayOfWeek> daysOfWeek = new List<DayOfWeek>();

            if (daysOfWeekType.HasFlag(DaysOfWeekType.Monday) == true) { daysOfWeek.Add(DayOfWeek.Monday); }
            if (daysOfWeekType.HasFlag(DaysOfWeekType.Tuesday) == true) { daysOfWeek.Add(DayOfWeek.Tuesday); }
            if (daysOfWeekType.HasFlag(DaysOfWeekType.Wednesday) == true) { daysOfWeek.Add(DayOfWeek.Wednesday); }
            if (daysOfWeekType.HasFlag(DaysOfWeekType.Thursday) == true) { daysOfWeek.Add(DayOfWeek.Thursday); }
            if (daysOfWeekType.HasFlag(DaysOfWeekType.Friday) == true) { daysOfWeek.Add(DayOfWeek.Friday); }
            if (daysOfWeekType.HasFlag(DaysOfWeekType.Saturday) == true) { daysOfWeek.Add(DayOfWeek.Saturday); }
            if (daysOfWeekType.HasFlag(DaysOfWeekType.Sunday) == true) { daysOfWeek.Add(DayOfWeek.Sunday); }

            return daysOfWeek;
        }
    }
}
