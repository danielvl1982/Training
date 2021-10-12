using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public static class DateTimeUtils
    {
        public static DateTime DateTimeDayOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
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
    }
}
