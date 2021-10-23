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
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                DateTime = new DateTime(2020, 1, 2)
            };

            DateTime currentDate = new DateTime(2020, 1, 1);

            Assert.AreEqual(schedule.DateTime, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Once_NextExecute_DatetimeStarDate_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                StartDate = new DateTime(2020, 1, 2),
                DateTime = new DateTime(2000, 1, 1)
            };

            DateTime currentDate = new DateTime(2020, 1, 1);

            Assert.AreEqual(schedule.DateTime, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Once_NextExecute_EndDateDateTime_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                EndDate = new DateTime(2020, 1, 1),
                DateTime = new DateTime(2020, 1, 1, 0, 0, 1)
            };

            DateTime currentDate = new DateTime(2020, 1, 1);

            Assert.AreEqual(schedule.DateTime, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Once_NextExecute_EndDateStartDate_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                EndDate = new DateTime(2020, 1, 1),
                StartDate = new DateTime(2020, 1, 2),
                DateTime = new DateTime(2000, 1, 2, 0, 0, 1)
            };

            DateTime currentDate = new DateTime(2020, 1, 1);

            Assert.AreEqual(schedule.DateTime, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Once_NextExecute_Every_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                DateTime = new DateTime(2020, 1, 2),
                Every = 2
            };

            DateTime currentDate = new DateTime(2020, 1, 1);

            Assert.AreEqual(schedule.DateTime, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Dayly_NextExecute()
        {
            Schedule schedule = new Schedule(FrecuencyType.Day)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 1
            };

            DateTime currentDate = new DateTime(2020, 01, 04);
            DateTime result = new DateTime(2020, 01, 05);

            Assert.AreEqual(result, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Dayly_NextExecute_Every_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Day)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 0
            };

            DateTime currentDate = new DateTime(2020, 01, 04);
            DateTime result = new DateTime(2020, 01, 05);

            Assert.AreEqual(result, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
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

            DateTime currentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            Assert.AreEqual(new DateTime(2020, 01, 1, 3, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 01, 1, 4, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 01, 1, 5, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 01, 1, 6, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 04, 1, 3, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 04, 1, 4, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 04, 1, 5, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 04, 1, 6, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);
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

            DateTime currentDate = new DateTime(2020, 01, 1);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            Assert.AreEqual(new DateTime(2020, 01, 15), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

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

            DateTime currentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            Assert.AreEqual(new DateTime(2020, 01, 31), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

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

            DateTime currentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            Assert.AreEqual(new DateTime(2020, 01, 31, 3, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 01, 31, 4, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 01, 31, 5, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 01, 31, 6, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 02, 29, 3, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 02, 29, 4, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 02, 29, 5, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 02, 29, 6, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);
        }
        [TestMethod]
        public void Schedule_Monthy_NextExecute_Day_MonthDay_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 3,
                MonthyDay = 32,
                MonthyType = MonthyType.Day,
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour
            };

            DateTime currentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            Assert.AreEqual(new DateTime(2020, 01, 31, 0, 0, 0), myExecution.GetDateTime());
        }
        [TestMethod]
        public void Schedule_Monthy_NextExecute_Day_No_MonthDay_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 3,
                MonthyType = MonthyType.Day,
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour
            };

            DateTime currentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            Assert.AreEqual(new DateTime(2020, 01, 1, 3, 0, 0), myExecution.GetDateTime());
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

            DateTime currentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            Assert.AreEqual(new DateTime(2020, 01, 2, 3, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 01, 2, 4, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 01, 2, 5, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 01, 2, 6, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 04, 2, 3, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 04, 2, 4, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 04, 2, 5, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(new DateTime(2020, 04, 2, 6, 0, 0), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);
        }
        [TestMethod]
        public void Schedule_Monthy_NextExecute_Weekday_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 3,
                MonthyDay = 1,
                MonthyType = MonthyType.First,
                DaysOfWeek = DaysOfWeekType.Thursday,
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour
            };

            DateTime currentDate = new DateTime(2020, 01, 01);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            Assert.AreEqual(new DateTime(2020, 01, 2, 3, 0, 0), myExecution.GetDateTime());
        }
        [TestMethod]
        public void Schedule_Weekly_NextExecute()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday
            };

            DateTime currentDate = new DateTime(2020, 01, 01);
            DateTime result = new DateTime(2020, 01, 02);

            Assert.AreEqual(result, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Weekly_NextExecute_Days_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2
            };

            DateTime currentDate = new DateTime(2020, 01, 01);
            DateTime result = new DateTime(2020, 01, 02);

            Assert.AreEqual(result, new ScheduleExecution(schedule, currentDate).GetDateTime());
        }
        [TestMethod]
        public void Schedule_Weekly_NextExecute_Frecuency_Once()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEvery = 2,
                DailyFrecuencyTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Once
            };

            DateTime currentDate = new DateTime(2020, 01, 02);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            DateTime result = new DateTime(2020, 01, 2, 4, 0, 0);

            Assert.AreEqual(result, myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);
        }
        [TestMethod]
        public void Schedule_Weekly_NextExecute_Frecuency_Once_Change_Day()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Once
            };

            DateTime currentDate = new DateTime(2020, 01, 02);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            DateTime result = new DateTime(2020, 01, 2, 4, 0, 0);

            Assert.AreEqual(result, myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(result.AddDays(1), myExecution.GetDateTime());
        }
        [TestMethod]
        public void Schedule_Weekly_NextExecute_Frecuency_Once_Change_Week()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Once
            };

            DateTime currentDate = new DateTime(2020, 01, 02);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            DateTime result = new DateTime(2020, 01, 2, 4, 0, 0);

            Assert.AreEqual(result, myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(result.AddDays(1), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            //Monday two Weeks
            Assert.AreEqual(result.AddDays(11), myExecution.GetDateTime());
        }
        [TestMethod]
        public void Schedule_Weekly_NextExecute_Frecuency_Once_Change_Week_DailyFrecuencyEvery_No_Effect()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEvery = 2,
                DailyFrecuencyTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Once
            };

            DateTime currentDate = new DateTime(2020, 01, 02);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            DateTime result = new DateTime(2020, 01, 2, 4, 0, 0);

            Assert.AreEqual(result, myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            Assert.AreEqual(result.AddDays(1), myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);

            //Monday two Weeks
            Assert.AreEqual(result.AddDays(11), myExecution.GetDateTime());
        }
        [TestMethod]
        public void Schedule_Weekly_NextExecute_Frecuency_Once_Time_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEvery = 2,
                DailyFrecuencyType = DailyType.Once
            };

            DateTime currentDate = new DateTime(2020, 01, 02);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            DateTime result = new DateTime(2020, 01, 2, 4, 0, 0);

            Assert.AreEqual(result, myExecution.GetDateTime());

            myExecution.SetCurrentDate(myExecution.GetDateTime().Value);
        }
        [TestMethod]
        public void Schedule_Weekly_NextExecute_Frecuency_Recurring()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEndTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyFrecuencyStartTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Hour
            };

            DateTime currentDate = new DateTime(2020, 01, 02);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            DateTime result = new DateTime(2020, 01, 2, 4, 0, 0);

            Assert.AreEqual(result, myExecution.GetDateTime());
        }
        [TestMethod]
        public void Schedule_Weekly_NextExecute_Frecuency_Recurring_EndTime_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEvery = 2,
                DailyFrecuencyStartTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Hour
            };

            DateTime currentDate = new DateTime(2020, 01, 02);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            DateTime result = new DateTime(2020, 01, 2, 4, 0, 0);

            Assert.AreEqual(result, myExecution.GetDateTime());
        }
        [TestMethod]
        public void Schedule_Weekly_NextExecute_Frecuency_Recurring_EndTimeStarTime_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEndTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyFrecuencyStartTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyType = DailyType.Hour
            };

            DateTime currentDate = new DateTime(2020, 01, 02);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            DateTime result = new DateTime(2020, 01, 2, 4, 0, 0);

            Assert.AreEqual(result, myExecution.GetDateTime());
        }
        [TestMethod]
        public void Schedule_Weekly_NextExecute_Frecuency_Recurring_DailyFrecuencyEvery_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEndTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyStartTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Hour
            };

            DateTime currentDate = new DateTime(2020, 01, 02);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            DateTime result = new DateTime(2020, 01, 2, 4, 0, 0);

            Assert.AreEqual(result, myExecution.GetDateTime());
        }
        [TestMethod]
        public void Schedule_Weekly_NextExecute_Frecuency_Recurring_StartTime_Error()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEndTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyFrecuencyType = DailyType.Hour
            };

            DateTime currentDate = new DateTime(2020, 01, 02);

            ScheduleExecution myExecution = new ScheduleExecution(schedule, currentDate);

            DateTime result = new DateTime(2020, 01, 2, 4, 0, 0);

            Assert.AreEqual(result, myExecution.GetDateTime());
        }
    }
}
