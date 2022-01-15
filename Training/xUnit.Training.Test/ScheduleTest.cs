using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Globalization;
using Training;
using Xunit;

namespace xUnit.Training.Test
{
    public class ScheduleTest
    {
        #region Test Exceptions

        [Fact]
        public void TestingScheduleNullException()
        {
            Action action = () => new ScheduleRun().GetScheduleRun(null);
            action.Should().Throw<ScheduleException>().WithMessage("Must instantiate schedule.");
        }
        [Fact]
        public void TestingScheduleEndDateStartDateException()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 1, 1),
                StartDate = new DateTime(2020, 1, 2)
            };

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("End date must be greater to start date.");
        }
        [Fact]
        public void TestingScheduleDateTimeStartDateException()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DateTime = new DateTime(2020, 1, 1),
                StartDate = new DateTime(2020, 1, 2)
            };

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("DateTime must be greater to start date.");
        }
        [Fact]
        public void TestingScheduleCurrentDateEndDateException()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                EndDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Current date must be lesser to end date.");
        }
        [Fact]
        public void TestingScheduleDateTimeEndDateException()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DateTime = new DateTime(2020, 1, 2),
                EndDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("DateTime must be lesser to end date.");
        }
        [Fact]
        public void TestingScheduleDateTimeCurrentDateException()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DateTime = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Current date must be lesser to datetime.");
        }
        [Fact]
        public void TestingScheduleOnceNotDatetimeException()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Occurs once must indicate the datetime.");
        }
        [Fact]
        public void TestingScheduleOnceEveryException()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DateTime = new DateTime(2020, 1, 2),
                Every = 1
            };

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Mustn't indicate every.");
        }
        [Fact]
        public void TestingScheduleRecurringDatetimeException()
        {
            Schedule schedule = new Schedule(FrecuencyType.Day)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DateTime = new DateTime(2020, 1, 2)
            };

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Occurs recurring mustn’t indicate the datetime.");
        }
        [Fact]
        public void TestingScheduleRecurringEveryException()
        {
            Schedule schedule = new Schedule(FrecuencyType.Day)
            {
                CurrentDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Must indicate every.");
        }
        [Fact]
        public void TestingScheduleRecurringEveryMinValueException()
        {
            Schedule schedule = new Schedule(FrecuencyType.Day)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Every = int.MinValue
            };

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Must indicate every.");
        }
        [Fact]
        public void TestingScheduleWeeklyDaysException()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Every = 1
            };

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Occurs weekly must indicate the days of the week.");
        }
        [Fact]
        public void TestingScheduleMonthyDayException()
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

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Occurs monthy day must indicate the day of the month.");
        }
        [Fact]
        public void TestingScheduleMonthyDayMinValueException()
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

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Occurs monthy day must indicate the day of the month.");
        }
        [Fact]
        public void TestingScheduleMonthyDayMaxValueException()
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

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Occurs monthy day mustn’t be greater to 31.");
        }
        [Fact]
        public void TestingScheduleMonthyDayDaysOfWeekException()
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

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Occurs monthy mustn’t indicate the days of the week.");
        }
        [Fact]
        public void TestingScheduleMonthyWeekDayException()
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

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Occurs monthy day mustn’t indicate the day of the month.");
        }
        [Fact]
        public void TestingScheduleMonthyWeekDaysOfWeekException()
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

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Occurs monthy must indicate the days of the week.");
        }
        [Fact]
        public void TestingScheduleWeeklyOnceTimeException()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyType = DailyType.Once,
                Every = 2,
                StartDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Must indicate Occurs once at time.");
        }
        [Fact]
        public void TestingScheduleWeeklyOnceEveryException()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEvery = 1,
                DailyFrecuencyType = DailyType.Once,
                DailyFrecuencyTime = new TimeSpan(0, 0, 0),
                Every = 2,
                StartDate = new DateTime(2020, 1, 1)
            };

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Occurs every must be 0.");
        }
        [Fact]
        public void TestingScheduleWeeklyRecurringStartTimeException()
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

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Must indicate starting at.");
        }
        [Fact]
        public void TestingScheduleWeeklyRecurringEndTimeException()
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

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Must indicate end at.");
        }
        [Fact]
        public void TestingScheduleWeeklyRecurringEndTimeStarTimeException()
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

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("End at must be greater to stating at.");
        }
        [Fact]
        public void TestingScheduleWeeklyRecurringEveryException()
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

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Occurs every must be greater to 0.");
        }
        [Fact]
        public void TestingScheduleWeeklyRecurringEveryMinValueException()
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

            Action action = () => new ScheduleRun().GetScheduleRun(schedule);
            action.Should().Throw<ScheduleException>().WithMessage("Occurs every must be greater to 0.");
        }

        #endregion

        #region Test Day

        [Fact]
        public void TestingScheduleOnce()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DateTime = new DateTime(2020, 1, 2, 0, 0, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 2, 0, 0, 1),
                new DateTime(2020, 1, 2, 0, 0, 1)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleOnceDescription()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Culture = new CultureInfo("es-ES"),
                DateTime = new DateTime(2020, 1, 2, 12, 0, 0),
                EndDate = new DateTime(2020, 1, 3),
                StartDate = new DateTime(2020, 1, 1)
            };

            string expected = @"Una vez. Planificador sera usado en 02/01/2020 12:00:00 comenzado en 01/01/2020 hasta 03/01/2020";


            new ScheduleRun().GetScheduleRun(schedule).Description.Should().Be(expected);
        }
        [Fact]
        public void TestingScheduleRecurring()
        {
            Schedule schedule = new Schedule(FrecuencyType.Day)
            {
                CurrentDate = new DateTime(2020, 1, 4),
                Every = 1,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 5),
                new DateTime(2020, 1, 6)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleRecurringStartDateGreatherCurrentDate()
        {
            Schedule schedule = new Schedule(FrecuencyType.Day)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Every = 1,
                StartDate = new DateTime(2020, 1, 5)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 5),
                new DateTime(2020, 1, 6)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleRecurringDescription()
        {
            Schedule schedule = new Schedule(FrecuencyType.Day)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Culture = new CultureInfo("en-GB"),
                EndDate = new DateTime(2020, 1, 3),
                Every = 1,
                StartDate = new DateTime(2020, 1, 1)
            };

            string expected = @"Occurs every day. Schedule will be used on 02/01/2020 starting on 01/01/2020 until 03/01/2020";

            new ScheduleRun().GetScheduleRun(schedule).Description.Should().Be(expected);
        }

        #endregion

        #region Test Week

        [Fact]
        public void TestingScheduleWeeklyDayOfWeekChangeDayChageWeek()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                Every = 2,
                StartDate = new DateTime(2020, 1, 3),
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 3),
                new DateTime(2020, 1, 13)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleWeeklyDayOfWeekChangeDayChageWeekDescription()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                Every = 2
            };

            string expected = @"Occurs every 2 week on monday, thursday, friday";

            new ScheduleRun().GetScheduleRun(schedule).Description.Should().Be(expected);
        }

        #region Test Daily

        [Fact]
        public void TestingScheduleWeeklyOnceChangeDayChangeWeek()
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

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 2, 4, 0, 0),
                new DateTime(2020, 1, 3, 4, 0, 0),
                new DateTime(2020, 1, 13, 4, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleWeeklyOnceChangeDayChangeWeekDescription()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                Culture = new CultureInfo("en-GB"),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Once,
                Every = 2
            };

            string expected = @"Occurs every 2 week on monday, thursday, friday on 04:00:00";

            new ScheduleRun().GetScheduleRun(schedule).Description.Should().Be(expected);
        }
        [Fact]
        public void TestingScheduleWeeklyRecurringChangeHourDaysWeek()
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

            List<DateTime?> expected = new List<DateTime?>()
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
        public void TestingScheduleWeeklyRecurringChangeHourDaysWeekDescription()
        {
            Schedule schedule = new Schedule(FrecuencyType.Week)
            {
                CurrentDate = new DateTime(2020, 1, 2),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday | DaysOfWeekType.Friday,
                DailyFrecuencyEndTime = new TimeSpan(8, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyFrecuencyStartTime = new TimeSpan(4, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 2
            };

            string expected = @"Occurs every 2 week on monday, thursday, friday every 2 hour beetween 04:00:00 and 08:00:00";

            new ScheduleRun().GetScheduleRun(schedule).Description.Should().Be(expected);
        }
        [Fact]
        public void TestingScheduleWeeklyRecurringChangeMinutes()
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

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 2, 4, 0, 0),
                new DateTime(2020, 1, 2, 4, 2, 0),
                new DateTime(2020, 1, 2, 4, 4, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleWeeklyRecurringChangeSeconds()
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

            List<DateTime?> expected = new List<DateTime?>()
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
        public void TestingScheduleMonthyDay()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Every = 3,
                MonthyDay = 15,
                MonthyType = MonthyType.Day,
                StartDate = new DateTime(2020, 1, 15),
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 15),
                new DateTime(2020, 4, 15)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyDayRecurringHour()
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

            List<DateTime?> expected = new List<DateTime?>()
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
        public void TestingScheduleMonthyDayRecurringHourDescription()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 12, 1),
                Culture = new CultureInfo("en-GB"),
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 3,
                MonthyDay = 1,
                MonthyType = MonthyType.Day,
                StartDate = new DateTime(2020, 12, 1)
            };

            string expected = @"Occurs every 3 month. Day 1 every 1 hour beetween 03:00:00 and 06:00:00 starting on 01/12/2020";

            new ScheduleRun().GetScheduleRun(schedule).Description.Should().Be(expected);
        }
        [Fact]
        public void TestingScheduleMonthyDayLastDayOfMonth()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Every = 1,
                MonthyDay = 31,
                MonthyType = MonthyType.Day,
                StartDate = new DateTime(2020, 1, 1),
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 31),
                new DateTime(2020, 2, 29)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyDayLastDayMonthRecurringHour()
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

            List<DateTime?> expected = new List<DateTime?>()
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
        public void TestingScheduleMonthyFirstMondayThursday()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday,
                Every = 3,
                MonthyType = MonthyType.First,
                StartDate = new DateTime(2020, 1, 2)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 2, 0, 0, 0),
                new DateTime(2020, 1, 6, 0, 0, 0),
                new DateTime(2020, 4, 2, 0, 0, 0),
                new DateTime(2020, 4, 6, 0, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyFirstMondayThursdayEndDateDoesNotAllowRunning()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Monday,
                EndDate = new DateTime(2020, 1, 2),
                Every = 1,
                MonthyType = MonthyType.First
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                null
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyFirstMondayThursdayRecurringHour()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday,
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 3,
                MonthyType = MonthyType.First,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 2, 3, 0, 0),
                new DateTime(2020, 1, 2, 4, 0, 0),
                new DateTime(2020, 1, 2, 5, 0, 0),
                new DateTime(2020, 1, 2, 6, 0, 0),
                new DateTime(2020, 1, 6, 3, 0, 0),
                new DateTime(2020, 1, 6, 4, 0, 0),
                new DateTime(2020, 1, 6, 5, 0, 0),
                new DateTime(2020, 1, 6, 6, 0, 0),
                new DateTime(2020, 4, 2, 3, 0, 0),
                new DateTime(2020, 4, 2, 4, 0, 0),
                new DateTime(2020, 4, 2, 5, 0, 0),
                new DateTime(2020, 4, 2, 6, 0, 0),
                new DateTime(2020, 4, 6, 3, 0, 0),
                new DateTime(2020, 4, 6, 4, 0, 0),
                new DateTime(2020, 4, 6, 5, 0, 0),
                new DateTime(2020, 4, 6, 6, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyFirstMondayThursdayDescription()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Monday | DaysOfWeekType.Thursday,
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 3,
                MonthyType = MonthyType.First,
                StartDate = new DateTime(2020, 1, 1)
            };

            string expected = @"Occurs the first monday, thursday of the very 3 months every 1 hour beetween 03:00:00 and 06:00:00 starting on 1/1/2020";

            new ScheduleRun().GetScheduleRun(schedule).Description.Should().Be(expected);
        }
        [Fact]
        public void TestingScheduleMonthyFirstWeekday()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Weekday,
                Every = 3,
                MonthyType = MonthyType.First,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 2, 0, 0, 0),
                new DateTime(2020, 1, 3, 0, 0, 0),
                new DateTime(2020, 1, 6, 0, 0, 0),
                new DateTime(2020, 1, 7, 0, 0, 0),
                new DateTime(2020, 4, 1, 0, 0, 0),
                new DateTime(2020, 4, 2, 0, 0, 0),
                new DateTime(2020, 4, 3, 0, 0, 0),
                new DateTime(2020, 4, 6, 0, 0, 0),
                new DateTime(2020, 4, 7, 0, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyFirstWeekdayRecurringHour()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Weekday,
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 3,
                MonthyType = MonthyType.First,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 1, 3, 0, 0),
                new DateTime(2020, 1, 1, 5, 0, 0),
                new DateTime(2020, 1, 2, 3, 0, 0),
                new DateTime(2020, 1, 2, 5, 0, 0),
                new DateTime(2020, 1, 3, 3, 0, 0),
                new DateTime(2020, 1, 3, 5, 0, 0),
                new DateTime(2020, 1, 6, 3, 0, 0),
                new DateTime(2020, 1, 6, 5, 0, 0),
                new DateTime(2020, 1, 7, 3, 0, 0),
                new DateTime(2020, 1, 7, 5, 0, 0),
                new DateTime(2020, 4, 1, 3, 0, 0),
                new DateTime(2020, 4, 1, 5, 0, 0),
                new DateTime(2020, 4, 2, 3, 0, 0),
                new DateTime(2020, 4, 2, 5, 0, 0),
                new DateTime(2020, 4, 3, 3, 0, 0),
                new DateTime(2020, 4, 3, 5, 0, 0),
                new DateTime(2020, 4, 6, 3, 0, 0),
                new DateTime(2020, 4, 6, 5, 0, 0),
                new DateTime(2020, 4, 7, 3, 0, 0),
                new DateTime(2020, 4, 7, 5, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyFirstWeekenday()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Weekenday,
                Every = 3,
                MonthyType = MonthyType.First,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 4, 0, 0, 0),
                new DateTime(2020, 1, 5, 0, 0, 0),
                new DateTime(2020, 4, 4, 0, 0, 0),
                new DateTime(2020, 4, 5, 0, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyFirstWeekendayRecurringHour()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Weekenday,
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 3,
                MonthyType = MonthyType.First,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 4, 3, 0, 0),
                new DateTime(2020, 1, 4, 4, 0, 0),
                new DateTime(2020, 1, 4, 5, 0, 0),
                new DateTime(2020, 1, 4, 6, 0, 0),
                new DateTime(2020, 1, 5, 3, 0, 0),
                new DateTime(2020, 1, 5, 4, 0, 0),
                new DateTime(2020, 1, 5, 5, 0, 0),
                new DateTime(2020, 1, 5, 6, 0, 0),
                new DateTime(2020, 4, 4, 3, 0, 0),
                new DateTime(2020, 4, 4, 4, 0, 0),
                new DateTime(2020, 4, 4, 5, 0, 0),
                new DateTime(2020, 4, 4, 6, 0, 0),
                new DateTime(2020, 4, 5, 3, 0, 0),
                new DateTime(2020, 4, 5, 4, 0, 0),
                new DateTime(2020, 4, 5, 5, 0, 0),
                new DateTime(2020, 4, 5, 6, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthySecondThursday()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Thursday,
                Every = 3,
                MonthyType = MonthyType.Second,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 9, 0, 0, 0),
                new DateTime(2020, 4, 9, 0, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthySecondThursdayRecurringHour()
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
                MonthyType = MonthyType.Second,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 9, 3, 0, 0),
                new DateTime(2020, 1, 9, 4, 0, 0),
                new DateTime(2020, 1, 9, 5, 0, 0),
                new DateTime(2020, 1, 9, 6, 0, 0),
                new DateTime(2020, 4, 9, 3, 0, 0),
                new DateTime(2020, 4, 9, 4, 0, 0),
                new DateTime(2020, 4, 9, 5, 0, 0),
                new DateTime(2020, 4, 9, 6, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyThridThursday()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Thursday,
                Every = 3,
                MonthyType = MonthyType.Thrid,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 16, 0, 0, 0),
                new DateTime(2020, 4, 16, 0, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyThridThursdayRecurringHour()
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
                MonthyType = MonthyType.Thrid,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 16, 3, 0, 0),
                new DateTime(2020, 1, 16, 4, 0, 0),
                new DateTime(2020, 1, 16, 5, 0, 0),
                new DateTime(2020, 1, 16, 6, 0, 0),
                new DateTime(2020, 4, 16, 3, 0, 0),
                new DateTime(2020, 4, 16, 4, 0, 0),
                new DateTime(2020, 4, 16, 5, 0, 0),
                new DateTime(2020, 4, 16, 6, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyFourthThursday()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Thursday,
                Every = 3,
                MonthyType = MonthyType.Fourth,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 23, 0, 0, 0),
                new DateTime(2020, 4, 23, 0, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyFourthThursdayRecurringHour()
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
                MonthyType = MonthyType.Fourth,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 23, 3, 0, 0),
                new DateTime(2020, 1, 23, 4, 0, 0),
                new DateTime(2020, 1, 23, 5, 0, 0),
                new DateTime(2020, 1, 23, 6, 0, 0),
                new DateTime(2020, 4, 23, 3, 0, 0),
                new DateTime(2020, 4, 23, 4, 0, 0),
                new DateTime(2020, 4, 23, 5, 0, 0),
                new DateTime(2020, 4, 23, 6, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyLastThursdaySunday()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Thursday | DaysOfWeekType.Sunday,
                Every = 3,
                MonthyType = MonthyType.Last,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 26, 0, 0, 0),
                new DateTime(2020, 1, 30, 0, 0, 0),
                new DateTime(2020, 4, 26, 0, 0, 0),
                new DateTime(2020, 4, 30, 0, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }
        [Fact]
        public void TestingScheduleMonthyLastThursdaySundayRecurringHour()
        {
            Schedule schedule = new Schedule(FrecuencyType.Month)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DaysOfWeek = DaysOfWeekType.Thursday | DaysOfWeekType.Sunday,
                DailyFrecuencyEndTime = new TimeSpan(6, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyFrecuencyStartTime = new TimeSpan(3, 0, 0),
                DailyFrecuencyType = DailyType.Hour,
                Every = 3,
                MonthyType = MonthyType.Last,
                StartDate = new DateTime(2020, 1, 1)
            };

            List<DateTime?> expected = new List<DateTime?>()
            {
                new DateTime(2020, 1, 26, 3, 0, 0),
                new DateTime(2020, 1, 26, 4, 0, 0),
                new DateTime(2020, 1, 26, 5, 0, 0),
                new DateTime(2020, 1, 26, 6, 0, 0),
                new DateTime(2020, 1, 30, 3, 0, 0),
                new DateTime(2020, 1, 30, 4, 0, 0),
                new DateTime(2020, 1, 30, 5, 0, 0),
                new DateTime(2020, 1, 30, 6, 0, 0),
                new DateTime(2020, 4, 26, 3, 0, 0),
                new DateTime(2020, 4, 26, 4, 0, 0),
                new DateTime(2020, 4, 26, 5, 0, 0),
                new DateTime(2020, 4, 26, 6, 0, 0),
                new DateTime(2020, 4, 30, 3, 0, 0),
                new DateTime(2020, 4, 30, 4, 0, 0),
                new DateTime(2020, 4, 30, 5, 0, 0),
                new DateTime(2020, 4, 30, 6, 0, 0)
            };

            ValidateExpected(schedule, expected);
        }

        #endregion

        private static void ValidateExpected(Schedule schedule, List<DateTime?> expected)
        {
            DateTime? runDatetime;

            expected.ForEach((Action<DateTime?>)delegate (DateTime? datetime)
            {
                runDatetime = new global::Training.ScheduleRun().GetScheduleRun(schedule).RunDateTime;
                runDatetime.Should().Be((DateTime?)datetime);

                if (runDatetime == null) { return; }

                schedule.CurrentDate = new global::Training.ScheduleRun().GetScheduleRun(schedule).RunDateTime.Value;
            });
        }
    }
}
