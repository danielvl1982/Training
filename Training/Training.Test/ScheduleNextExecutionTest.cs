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
            Schedule mySchedule = new Schedule
            {
                StartDate = new DateTime(2020, 1, 1),
                DateTime = new DateTime(2020, 1, 8, 14, 0, 0),
                Type = Frecuency.NewByName("Once")
            };

            DateTime currentDate = new DateTime(2020, 01, 04);

            ScheduleExecution myExecution = new ScheduleExecution(mySchedule, currentDate);

            DateTime result = new DateTime(2020, 01, 08, 14, 0, 0);

            Assert.AreEqual(result, myExecution.GetDateTime());
        }

        [TestMethod]
        public void Schedule_Recurring_Dayly_NextExecute()
        {
            Schedule mySchedule = new Schedule
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 1,
                Type = Frecuency.NewByName("Recurring_Day")
            };

            DateTime currentDate = new DateTime(2020, 01, 04);

            ScheduleExecution myExecution = new ScheduleExecution(mySchedule, currentDate);

            DateTime result = new DateTime(2020, 01, 05);

            Assert.AreEqual(result, myExecution.GetDateTime());
        }
        [TestMethod]
        public void Schedule_Recurring_Weekly_NextExecute()
        {
            Schedule mySchedule = new Schedule
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                Type = Frecuency.NewByName("Recurring_Week")
            };
            mySchedule.AddDay(DayOfWeek.Monday);
            mySchedule.AddDay(DayOfWeek.Thursday);
            mySchedule.AddDay(DayOfWeek.Friday);

            DateTime currentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(mySchedule, currentDate);

            DateTime result = new DateTime(2020, 01, 02);

            Assert.AreEqual(result, myExecution.GetDateTime());
        }
        [TestMethod]
        public void Schedule_Recurring_Weekly_Frecuency_NextExecute()
        {
            Schedule mySchedule = new Schedule
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                Type = Frecuency.NewByName("Recurring_Week")
            };
            mySchedule.AddDay(DayOfWeek.Monday);
            mySchedule.AddDay(DayOfWeek.Thursday);
            mySchedule.AddDay(DayOfWeek.Friday);

            DailyFrecuency myFrecuency = DailyFrecuency.NewByName("Recurring_Hour");
            myFrecuency.DailyFrecuencyEndTime = new TimeSpan(8, 0, 0);
            myFrecuency.DailyFrecuencyEvery = 2;
            myFrecuency.DailyFrecuencyStartTime = new TimeSpan(4, 0, 0);

            mySchedule.Frecuency = myFrecuency;

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
