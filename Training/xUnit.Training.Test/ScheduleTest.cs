using FluentAssertions;
using System;
using System.Collections.Generic;
using Training;
using Xunit;

namespace xUnit.Training.Test
{
    public class ScheduleTest
    {
        #region Test Exceptions

        [Fact]
        public void Schedule_Null_Exception()
        {
            Action action = () => new ScheduleExecution(null).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Must instantiate schedule.");
        }
        [Fact]
        public void Schedule_EndDate_StartDate_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 1, 1),
                StartDate = new DateTime(2020, 1, 2)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("End date must be greater to start date.");
        }
        [Fact]
        public void Schedule_CurrentDate_StartDate_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                StartDate = new DateTime(2020, 1, 2)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Current date must be greater to start date.");
        }
        [Fact]
        public void Schedule_DateTime_StartDate_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DateTime = new DateTime(2020, 1, 1),
                StartDate = new DateTime(2020, 1, 2)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("DateTime must be greater to start date.");
        }
        [Fact]
        public void Schedule_CurrentDate_EndDate_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                EndDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Current date must be lesser to end date.");
        }
        [Fact]
        public void Schedule_DateTime_EndDate_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DateTime = new DateTime(2020, 1, 2),
                EndDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("DateTime must be lesser to end date.");
        }
        [Fact]
        public void Schedule_Once_Every_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DateTime = new DateTime(2020, 1, 2),
                Every = 1
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Every must be 0.");
        }
        [Fact]
        public void Schedule_Recurring_Every_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Day)
            {
                CurrentDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Every must be greater to 0.");
        }
        [Fact]
        public void Schedule_Recurring_Every_MinValue_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Day)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Every = int.MinValue
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Every must be greater to 0.");
        }
        [Fact]
        public void Schedule_Weekly_Days_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Every = 1
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Occurs weekly must indicate the days of the week.");
        }
        [Fact]
        public void Schedule_Monthy_Day_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 3,
                MonthyType = MonthyType.Day,
                StartDate = new DateTime(2020, 1, 1),
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Occurs monthy day must indicate the day of the month.");
        }
        [Fact]
        public void Schedule_Monthy_Day_MinValue_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 3,
                MonthyDay = int.MinValue,
                MonthyType = MonthyType.Day,
                StartDate = new DateTime(2020, 1, 1),
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Occurs monthy day must indicate the day of the month.");
        }
        [Fact]
        public void Schedule_Monthy_Day_MaxValue_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 3,
                MonthyDay = 32,
                MonthyType = MonthyType.Day,
                StartDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Occurs monthy day mustn’t be greater to 31.");
        }
        [Fact]
        public void Schedule_Monthy_Day_DaysOfWeek_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                DaysOfWeek = DaysOfWeekType.Monday,
                Every = 3,
                MonthyDay = 1,
                MonthyType = MonthyType.Day,
                StartDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Occurs monthy mustn’t indicate the days of the week.");
        }
        [Fact]
        public void Schedule_Monthy_Week_Day_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                DaysOfWeek = DaysOfWeekType.Monday,
                Every = 3,
                MonthyDay = 1,
                MonthyType = MonthyType.First,
                StartDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Occurs monthy day mustn’t indicate the day of the month.");
        }
        [Fact]
        public void Schedule_Monthy_Week_DaysOfWeek_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 3,
                MonthyType = MonthyType.First,
                StartDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Occurs monthy must indicate the days of the week.");
        }
        [Fact]
        public void Schedule_Weekly_Once_Time_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyType = DailyType.Once,
                Every = 2,
                StartDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Must indicate Occurs once at time.");
        }
        [Fact]
        public void Schedule_Weekly_Once_Every_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEvery = 1,
                DailyFrecuencyType = DailyType.Once,
                DailyFrecuencyTime = new TimeSpan(0,0,0),
                Every = 2,
                StartDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Occurs every must be 0.");
        }
        [Fact]
        public void Schedule_Weekly_Recurring_StartTime_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEndTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyFrecuencyType = DailyType.Hour,
                Every = 2,
                StartDate = new DateTime(2020, 1, 1),
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Must indicate starting at.");
        }
        [Fact]
        public void Schedule_Weekly_Recurring_EndTime_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEvery = 2,
                DailyFrecuencyStartTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 2,
                StartDate = new DateTime(2020, 1, 1),
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Must indicate end at.");
        }
        [Fact]
        public void Schedule_Weekly_Recurring_EndTimeStarTime_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEndTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyFrecuencyStartTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 2,
                StartDate = new DateTime(2020, 1, 1),
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("End at must be greater to stating at.");
        }
        [Fact]
        public void Schedule_Weekly_Recurring_Every_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEndTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyStartTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 2,
                StartDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Occurs every must be greater to 0.");
        }
        [Fact]
        public void Schedule_Weekly_Recurring_Every_MinValue_Exception()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEndTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyEvery = int.MinValue,
                DailyFrecuencyStartTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 2,
                StartDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleExecution(schedule).GetDateTime();
            action.Should().Throw<ScheduleException>().WithMessage("Occurs every must be greater to 0.");
        }

        #endregion

        #region Test Day

        [Fact]
        public void Schedule_Once()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DateTime = new DateTime(2020, 1, 2)
            };

            List<DateTime> expected = new List<DateTime>()
            {
                new DateTime(2020, 1, 2),
                new DateTime(2020, 1, 2)
            };

            schedule.CurrentDate = new DateTime(2020, 1, 2);

            ValidateExpected(schedule, expected);
        }

        [Fact]
        public void Schedule_Recurring()
        {
            Schedule schedule = new Schedule(FrecuencyType.Day)
            {
                CurrentDate = new DateTime(2020, 1, 4),
                Every = 1,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime> expected = new List<DateTime>()
            {
                new DateTime(2020, 1, 5),
                new DateTime(2020, 1, 6)
            };

            ValidateExpected(schedule, expected);
        }

        #endregion

        #region Test Week

        [Fact]
        public void Schedule_Weekly_DayOfWeek_Change_Day_Chage_Week()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                Every = 2
            };

            List<DateTime> expected = new List<DateTime>()
            {
                new DateTime(2020, 1, 2),
                new DateTime(2020, 1, 3),
                new DateTime(2020, 1, 13)
            };

            ValidateExpected(schedule, expected);
        }

        #region Test Daily

        [Fact]
        public void Schedule_Weekly_Once_Change_Day_Change_Week()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Once,
                Every = 2,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime> expected = new List<DateTime>()
            {
                new DateTime(2020, 1, 2, 4, 0, 0),
                new DateTime(2020, 1, 3, 4, 0, 0),
                new DateTime(2020, 1, 13, 4, 0, 0)
            };

            schedule.CurrentDate = new DateTime(2020, 1, 2);

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void Schedule_Weekly_Recurring_Change_Hour_Days_Week()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEndTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyFrecuencyStartTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 2,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime> expected = new List<DateTime>()
            {
                new DateTime(2020, 1, 2, 4, 0, 0),
                new DateTime(2020, 1, 2, 6, 0, 0),
                new DateTime(2020, 1, 2, 8, 0, 0),
                new DateTime(2020, 1, 3, 4, 0, 0),
                new DateTime(2020, 1, 3, 6, 0, 0),
                new DateTime(2020, 1, 3, 8, 0, 0),
                new DateTime(2020, 1, 13, 4, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void Schedule_Weekly_Recurring_Change_Minutes()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEndTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyFrecuencyStartTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Minute,
                Every = 2,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime> expected = new List<DateTime>()
            {
                new DateTime(2020, 1, 2, 4, 0, 0),
                new DateTime(2020, 1, 2, 4, 2, 0),
                new DateTime(2020, 1, 2, 4, 4, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void Schedule_Weekly_Recurring_Change_Seconds()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEndTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyFrecuencyStartTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Second,
                Every = 2,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime> expected = new List<DateTime>()
            {
                new DateTime(2020, 1, 2, 4, 0, 0),
                new DateTime(2020, 1, 2, 4, 0, 2),
                new DateTime(2020, 1, 2, 4, 0, 4)
            };

            ValidateExpected(schedule, expected);
        }

        #endregion

        #endregion

        #region Test Month

        [Fact]
        public void Schedule_Monthy_Day()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Every = 3,
                MonthyDay = 15,
                MonthyType = MonthyType.Day,
                StartDate = new DateTime(2020, 1, 1),
            };

            List<DateTime> expected = new List<DateTime>()
            {
                new DateTime(2020, 1, 15),
                new DateTime(2020, 4, 15)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void Schedule_Monthy_Day_Recurring_Hour()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 3,
                MonthyDay = 1,
                MonthyType = MonthyType.Day,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime> expected = new List<DateTime>()
            {
                new DateTime(2020, 1, 1, 3, 0, 0),
                new DateTime(2020, 1, 1, 4, 0, 0),
                new DateTime(2020, 1, 1, 5, 0, 0),
                new DateTime(2020, 1, 1, 6, 0, 0),
                new DateTime(2020, 4, 1, 3, 0, 0),
                new DateTime(2020, 4, 1, 4, 0, 0),
                new DateTime(2020, 4, 1, 5, 0, 0),
                new DateTime(2020, 4, 1, 6, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void Schedule_Monthy_Day_LastDayOfMonth()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Every = 1,
                MonthyDay = 31,
                MonthyType = MonthyType.Day,
                StartDate = new DateTime(2020, 1, 1),
            };

            List<DateTime> expected = new List<DateTime>()
            {
                new DateTime(2020, 1, 31),
                new DateTime(2020, 2, 29)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void Schedule_Monthy_Day_LastDayMonth_Recurring_Hour()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 1,
                MonthyDay = 31,
                MonthyType = MonthyType.Day,
                StartDate = new DateTime(2020, 1, 1)

            };

            List<DateTime> expected = new List<DateTime>()
            {
                new DateTime(2020, 1, 31, 3, 0, 0),
                new DateTime(2020, 1, 31, 4, 0, 0),
                new DateTime(2020, 1, 31, 5, 0, 0),
                new DateTime(2020, 1, 31, 6, 0, 0),
                new DateTime(2020, 2, 29, 3, 0, 0),
                new DateTime(2020, 2, 29, 4, 0, 0),
                new DateTime(2020, 2, 29, 5, 0, 0),
                new DateTime(2020, 2, 29, 6, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void Schedule_Monthy_FirstWeek()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Thursday,
                Every = 3,
                MonthyType = MonthyType.First,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime> expected = new List<DateTime>()
            {
                new DateTime(2020, 1, 2, 0, 0, 0),
                new DateTime(2020, 4, 2, 0, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void Schedule_Monthy_FirstWeek_Recurring_Hour()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Thursday,
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 3,
                MonthyType = MonthyType.First,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime> expected = new List<DateTime>()
            {
                new DateTime(2020, 1, 2, 3, 0, 0),
                new DateTime(2020, 1, 2, 4, 0, 0),
                new DateTime(2020, 1, 2, 5, 0, 0),
                new DateTime(2020, 1, 2, 6, 0, 0),
                new DateTime(2020, 4, 2, 3, 0, 0),
                new DateTime(2020, 4, 2, 4, 0, 0),
                new DateTime(2020, 4, 2, 5, 0, 0),
                new DateTime(2020, 4, 2, 6, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }

        #endregion

        private static void ValidateExpected(Schedule schedule, List<DateTime> expected)
        {
            expected.ForEach(delegate (DateTime datetime)
            {
                new ScheduleExecution(schedule).GetDateTime().Should().Be((DateTime?)datetime);

                schedule.CurrentDate = new ScheduleExecution(schedule).GetDateTime().Value;
            });
        }
    }
}
