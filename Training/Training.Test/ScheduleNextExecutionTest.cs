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
                StartDate = new DateTime(2020, 1, 1)
            };

            Trigger myTriggerOnce = new Trigger
            {
                DateTime = new DateTime(2020, 1, 8, 14, 0, 0),
                Type = TriggerType.GetByName("Once")
            };
            myTriggerOnce.Type.Occurs = TriggerOccur.GetByName("Daily");

            mySchedule.Trigger = myTriggerOnce;

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
                StartDate = new DateTime(2020, 1, 1)
            };

            Trigger myTriggerRecurring = new Trigger
            {
                Every = 1,
                Type = TriggerType.GetByName("Recurring")
            };
            myTriggerRecurring.Type.Occurs = TriggerOccur.GetByName("Daily");

            mySchedule.Trigger = myTriggerRecurring;

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
                StartDate = new DateTime(2020, 1, 1)
            };

            Trigger myTriggerRecurring = new Trigger
            {
                Every = 2,
                Type = TriggerType.GetByName("Recurring")
            };
            myTriggerRecurring.Type.Occurs = TriggerOccur.GetByName("Weekly");
            myTriggerRecurring.AddDay(DayOfWeek.Monday);
            myTriggerRecurring.AddDay(DayOfWeek.Thursday);
            myTriggerRecurring.AddDay(DayOfWeek.Friday);

            mySchedule.Trigger = myTriggerRecurring;

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
                StartDate = new DateTime(2020, 1, 1)
            };

            Trigger myTriggerRecurring = new Trigger
            {
                Every = 2,
                Type = TriggerType.GetByName("Recurring")
            };
            myTriggerRecurring.Type.Occurs = TriggerOccur.GetByName("Weekly");
            myTriggerRecurring.AddDay(DayOfWeek.Monday);
            myTriggerRecurring.AddDay(DayOfWeek.Thursday);
            myTriggerRecurring.AddDay(DayOfWeek.Friday);

            DailyFrecuency myFrecuency = new DailyFrecuency
            {
                EndTime = new TimeSpan(8, 0, 0),
                Every = 2,
                StartTime = new TimeSpan(4, 0, 0),
                Type = DailyFrecuencyType.GetByName("Recurring")
            };
            myFrecuency.Type.Occurs = DailyOccur.GetByName("Hours");

            myTriggerRecurring.Frecuency = myFrecuency;

            mySchedule.Trigger = myTriggerRecurring;

            DateTime currentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(mySchedule, currentDate);

            DateTime result = new DateTime(2020, 01, 1, 4, 0, 0);

            Assert.AreEqual(result, myExecution.DateTime);

            myExecution.SetCurrentDate(myExecution.DateTime.Value);

            Assert.AreEqual(result.AddHours(2), myExecution.DateTime);

            myExecution.SetCurrentDate(myExecution.DateTime.Value);

            Assert.AreEqual(result.AddHours(4), myExecution.DateTime);
        }
    }
}
