using System;
using Training;
using Xunit;

namespace XUnitTest.Scheduler
{
    public class ScheduleTest
    {
        [Fact]
        public void Schedule_Once_NextExecute()
        {
            Schedule schedule = new Schedule(FrecuencyType.Once)
            {
                CurrentDate = new DateTime(2020, 1, 1),
                DateTime = new DateTime(2020, 1, 2)
            };

            new ScheduleExecution(schedule).GetDateTime().Should().Be(new DateTime(2020, 1, 1));
        }
    }
}
