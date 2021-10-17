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
            Schedule mySchedule = new Schedule(false, FrecuencyType.Day)
            {
                StartDate = new DateTime(2020, 1, 1),
                DateTime = new DateTime(2020, 1, 8, 14, 0, 0)
            };

            DateTime currentDate = new DateTime(2020, 01, 04);
            DateTime result = new DateTime(2020, 01, 08, 14, 0, 0);

            Assert.AreEqual(result, new ScheduleExecution(mySchedule, currentDate).GetDateTime());
        }

        [TestMethod]
        public void Schedule_Recurring_Dayly_NextExecute()
        {
            Schedule mySchedule = new Schedule(true, FrecuencyType.Day)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 1
            };

            DateTime currentDate = new DateTime(2020, 01, 04);
            DateTime result = new DateTime(2020, 01, 05);

            Assert.AreEqual(result, new ScheduleExecution(mySchedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Recurring_Weekly_NextExecute()
        {
            Schedule mySchedule = new Schedule(true, FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2
            };
            mySchedule.AddDay(DayOfWeek.Monday);
            mySchedule.AddDay(DayOfWeek.Thursday);
            mySchedule.AddDay(DayOfWeek.Friday);

            DateTime currentDate = new DateTime(2020, 01, 01);
            DateTime result = new DateTime(2020, 01, 02);

            Assert.AreEqual(result, new ScheduleExecution(mySchedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Recurring_Weekly_Frecuency_NextExecute()
        {
            Schedule mySchedule = new Schedule(true, FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DailyFrecuencyEndTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyFrecuencyStartTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
            };
            mySchedule.AddDay(DayOfWeek.Monday);
            mySchedule.AddDay(DayOfWeek.Thursday);
            mySchedule.AddDay(DayOfWeek.Friday);

            DateTime currentDate = new DateTime(2020, 01, 02);

            ScheduleExecution myExecution = new ScheduleExecution(mySchedule, currentDate);

            DateTime result = new DateTime(2020, 01, 2, 4, 0, 0);

            Assert.AreEqual(result, myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(result.AddHours(2), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(result.AddHours(4), myExecution.GetDateTime());
        }
    }
}
