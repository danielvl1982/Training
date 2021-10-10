using System;

namespace Training.Console
{
    class Program
    {
        static void Main()
        {
            Program.ExampleOnce();
            Program.ExampleRecurring();

            System.Console.ReadLine();
        }

        private static void ExampleOnce()
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

            try
            {
                ScheduleExecution myExecution = new ScheduleExecution(mySchedule, currentDate);

                System.Console.WriteLine("Current date: {0}", currentDate.ToString("dd/MM/yyyy HH:mm:ss"));
                System.Console.WriteLine(
                    "Next execution time: {0}",
                    myExecution.DateTime.HasValue == true ? myExecution.DateTime.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty);
                System.Console.WriteLine("Descripción: {0}", myExecution.Description);
                System.Console.WriteLine();
            }
            catch (Exception exc) { System.Console.WriteLine(exc.Message); }
        }
        private static void ExampleRecurring()
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

            try
            {
                for (int index = 0; index < 5; index++)
                {
                    ScheduleExecution myExecution = new ScheduleExecution(mySchedule, currentDate);

                    System.Console.WriteLine("Current date: {0}", currentDate.ToString("dd/MM/yyyy HH:mm:ss"));
                    System.Console.WriteLine(
                        "Next execution time: {0}",
                        myExecution.DateTime.HasValue == true ? myExecution.DateTime.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty);
                    System.Console.WriteLine("Descripción: {0}", myExecution.Description);
                    System.Console.WriteLine();

                    if (myExecution.DateTime.HasValue == false) { break; }

                    currentDate = myExecution.DateTime.Value.AddMilliseconds(1);
                }
            }
            catch (Exception exc) { System.Console.WriteLine(exc.Message); }
        }
    }
}
