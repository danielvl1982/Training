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
                Type = FrecuencyType.NewByName("Once")
            };
            mySchedule.Type.Occurs = FrecuencyOccur.NewByName("Daily");

            DateTime currentDate = new DateTime(2020, 01, 04);

            ScheduleExecution myExecution = new ScheduleExecution(mySchedule, currentDate);

            DateTime result = new DateTime(2020, 01, 08, 14, 0, 0);

            Assert.AreEqual(result, myExecution.DateTime);
        }

        [TestMethod]
        public void Schedule_Recurring_Dayly_NextExecute()
        {
            Schedule mySchedule = new Schedule
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 1,
                Type = FrecuencyType.NewByName("Recurring")
            };
            mySchedule.Type.Occurs = FrecuencyOccur.NewByName("Daily");

            DateTime currentDate = new DateTime(2020, 01, 04);

            ScheduleExecution myExecution = new ScheduleExecution(mySchedule, currentDate);

            DateTime result = new DateTime(2020, 01, 05);

            Assert.AreEqual(result, myExecution.DateTime);
        }
        [TestMethod]
        public void Schedule_Recurring_Weekly_NextExecute()
        {
            Schedule mySchedule = new Schedule
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                Type = FrecuencyType.NewByName("Recurring")
            };
            mySchedule.Type.Occurs = FrecuencyOccur.NewByName("Weekly");
            mySchedule.AddDay(DayOfWeek.Monday);
            mySchedule.AddDay(DayOfWeek.Thursday);
            mySchedule.AddDay(DayOfWeek.Friday);

            DateTime currentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(mySchedule, currentDate);

            DateTime result = new DateTime(2020, 01, 02);

            Assert.AreEqual(result, myExecution.DateTime);
        }
        [TestMethod]
        public void Schedule_Recurring_Weekly_Frecuency_NextExecute()
        {
            Schedule mySchedule = new Schedule
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                Type = FrecuencyType.NewByName("Recurring")
            };
            mySchedule.Type.Occurs = FrecuencyOccur.NewByName("Weekly");
            mySchedule.AddDay(DayOfWeek.Monday);
            mySchedule.AddDay(DayOfWeek.Thursday);
            mySchedule.AddDay(DayOfWeek.Friday);

            DailyFrecuency myFrecuency = new DailyFrecuency
            {
                EndTime = new TimeSpan(8, 0, 0),
                Every = 2,
                StartTime = new TimeSpan(4, 0, 0),
                Type = FrecuencyType.NewByName("Recurring")
            };
            myFrecuency.Type.Occurs = FrecuencyOccur.NewByName("Hours");

            mySchedule.Frecuency = myFrecuency;

            DateTime currentDate = new DateTime(2020, 01, 02);

            ScheduleExecution myExecution = new ScheduleExecution(mySchedule, currentDate);

            DateTime result = new DateTime(2020, 01, 2, 4, 0, 0);

            Assert.AreEqual(result, myExecution.DateTime);

            myExecution.SetCurrentDate(myExecution.DateTime.Value);

            Assert.AreEqual(result.AddHours(2), myExecution.DateTime);

            myExecution.SetCurrentDate(myExecution.DateTime.Value);

            Assert.AreEqual(result.AddHours(4), myExecution.DateTime);
        }
    }
}
