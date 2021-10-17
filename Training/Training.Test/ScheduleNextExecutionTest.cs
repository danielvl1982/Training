using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Training.Test
{
    [TestClass]
    public class ScheduleNextExecutionTest
    {
        [TestMethod]
        public void Schedule_Once_NextExecute()
        {
            Schedule schedule = new Schedule(false, FrecuencyType.Day)
            {
                DateTime = new DateTime(2020, 1, 2)
            };

            DateTime currentDate = new DateTime(2020, 1, 1);

            Assert.AreEqual(schedule.DateTime, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Once_NextExecute_DatetimeStarDate_Error()
        {
            Schedule schedule = new Schedule(false, FrecuencyType.Day)
            {
                StartDate = new DateTime(2020, 1, 2),
                DateTime = new DateTime(2000, 1, 1),
            };

            DateTime currentDate = new DateTime(2020, 1, 1);

            Assert.AreEqual(schedule.DateTime, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Once_NextExecute_EndDateDateTime_Error()
        {
            Schedule schedule = new Schedule(false, FrecuencyType.Day)
            {
                EndDate = new DateTime(2020, 1, 1),
                DateTime = new DateTime(2020, 1, 1, 0, 0, 1),
            };

            DateTime currentDate = new DateTime(2020, 1, 1);

            Assert.AreEqual(schedule.DateTime, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Once_NextExecute_EndDateStartDate_Error()
        {
            Schedule schedule = new Schedule(false, FrecuencyType.Day)
            {
                EndDate = new DateTime(2020, 1, 1),
                StartDate = new DateTime(2020, 1, 2),
                DateTime = new DateTime(2000, 1, 2, 0, 0, 1),
            };

            DateTime currentDate = new DateTime(2020, 1, 1);

            Assert.AreEqual(schedule.DateTime, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Once_NextExecute_Every_No_Effect()
        {
            Schedule schedule = new Schedule(false, FrecuencyType.Day)
            {
                DateTime = new DateTime(2020, 1, 2),
                Every = 2,
            };

            DateTime currentDate = new DateTime(2020, 1, 1);

            Assert.AreEqual(schedule.DateTime, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Once_NextExecute_Frecuency_Error()
        {
            Schedule schedule = new Schedule(false, FrecuencyType.Week)
            {
                DateTime = new DateTime(2020, 1, 2)
            };

            DateTime currentDate = new DateTime(2020, 1, 1);

            Assert.AreEqual(schedule.DateTime, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Recurring_Dayly_NextExecute()
        {
            Schedule schedule = new Schedule(true, FrecuencyType.Day)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 1
            };

            DateTime currentDate = new DateTime(2020, 01, 04);
            DateTime result = new DateTime(2020, 01, 05);

            Assert.AreEqual(result, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Recurring_Dayly_NextExecute_Every_Error()
        {
            Schedule schedule = new Schedule(true, FrecuencyType.Day)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 0
            };

            DateTime currentDate = new DateTime(2020, 01, 04);
            DateTime result = new DateTime(2020, 01, 05);

            Assert.AreEqual(result, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Recurring_Weekly_NextExecute()
        {
            Schedule schedule = new Schedule(true, FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2
            };
            schedule.AddDay(DayOfWeek.Monday);
            schedule.AddDay(DayOfWeek.Thursday);
            schedule.AddDay(DayOfWeek.Friday);

            DateTime currentDate = new DateTime(2020, 01, 01);
            DateTime result = new DateTime(2020, 01, 02);

            Assert.AreEqual(result, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Recurring_Weekly_NextExecute_Days_Error()
        {
            Schedule schedule = new Schedule(true, FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2
            };

            DateTime currentDate = new DateTime(2020, 01, 01);
            DateTime result = new DateTime(2020, 01, 02);

            Assert.AreEqual(result, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Recurring_Weekly_NextExecute_Frecuency()
        {
            Schedule schedule = new Schedule(true, FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DailyFrecuencyEndTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyFrecuencyStartTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
            };
            schedule.AddDay(DayOfWeek.Monday);
            schedule.AddDay(DayOfWeek.Thursday);
            schedule.AddDay(DayOfWeek.Friday);

            DateTime currentDate = new DateTime(2020, 01, 02);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            DateTime result = new DateTime(2020, 01, 2, 4, 0, 0);

            Assert.AreEqual(result, myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(result.AddHours(2), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(result.AddHours(4), myExecution.GetDateTime());
        }
    }
}
