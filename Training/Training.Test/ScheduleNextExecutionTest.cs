using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Training.Test
{
    [TestClass]
    public class ScheduleNextExecutionTest
    {
        [TestMethod]
        public void Schedule_Monthy_NextExecute_Day()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 3,
                MonthyDay = 1,
                MonthyType = MonthyType.Day,
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour
            };

            schedule.CurrentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(schedule);

            Assert.AreEqual(new DateTime(2020, 01, 1, 3, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 01, 1, 4, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 01, 1, 5, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 01, 1, 6, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 04, 1, 3, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 04, 1, 4, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 04, 1, 5, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 04, 1, 6, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;
        }
        [TestMethod]
        public void Schedule_Monthy_NextExecute_Day15()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 3,
                MonthyDay = 15,
                MonthyType = MonthyType.Day
            };

            schedule.CurrentDate = new DateTime(2020, 01, 1);

            ScheduleExecution myExecution = new ScheduleExecution(schedule);

            Assert.AreEqual(new DateTime(2020, 01, 15), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 04, 15), myExecution.GetDateTime());
        }
        [TestMethod]
        public void Schedule_Monthy_NextExecute_Day_LastDayMonth()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 1,
                MonthyDay = 31,
                MonthyType = MonthyType.Day
            };

            schedule.CurrentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(schedule);

            Assert.AreEqual(new DateTime(2020, 01, 31), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 02, 29), myExecution.GetDateTime());
        }
        [TestMethod]
        public void Schedule_Monthy_NextExecute_Day_LastDayMonthDaily()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 1,
                MonthyDay = 31,
                MonthyType = MonthyType.Day,
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour
            };

            schedule.CurrentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(schedule);

            Assert.AreEqual(new DateTime(2020, 01, 31, 3, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 01, 31, 4, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 01, 31, 5, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 01, 31, 6, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 02, 29, 3, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 02, 29, 4, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 02, 29, 5, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 02, 29, 6, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;
        }
        [TestMethod]
        public void Schedule_Monthy_NextExecute_Weekday()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 3,
                MonthyType = MonthyType.First,
                DaysOfWeek = DaysOfWeekType.Thursday,
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour
            };

            schedule.CurrentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(schedule);

            Assert.AreEqual(new DateTime(2020, 01, 2, 3, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 01, 2, 4, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 01, 2, 5, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 01, 2, 6, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 04, 2, 3, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 04, 2, 4, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 04, 2, 5, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;

            Assert.AreEqual(new DateTime(2020, 04, 2, 6, 0, 0), myExecution.GetDateTime());

            schedule.CurrentDate = myExecution.GetDateTime().Value;
        }
    }
}
