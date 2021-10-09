using System;

namespace Training.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Schedule mySchedule = new Schedule();
            mySchedule.CurrentDate = new DateTime(2020, 1, 04);
            mySchedule.StartDate = new DateTime(2020, 1, 1);

            Trigger myTriggerOnce = new Trigger();
            myTriggerOnce.DateTime = new DateTime(2020, 1, 8);
            myTriggerOnce.Type = TriggerType.GetByName("Once");
            myTriggerOnce.Type.Occurs = TriggerOccur.GetByName("Daily");

            Trigger myTriggerRecurring = new Trigger();
            myTriggerRecurring.Every = 1;
            myTriggerRecurring.Type = TriggerType.GetByName("Recurring");
            myTriggerRecurring.Type.Occurs = TriggerOccur.GetByName("Daily");

            mySchedule.AddTrigger(myTriggerOnce);
            mySchedule.AddTrigger(myTriggerRecurring);

            try
            {
                for (int index = 0; index < 5; index++)
                {
                    Execution myExecution = mySchedule.GetNextExecution();

                    System.Console.WriteLine("Current date: {0}", myExecution.DateTime.ToString("dd/MM/yyyy HH:mm:ss"));
                    System.Console.WriteLine("Next execution time: {0}", myExecution.DateTime.ToString("dd/MM/yyyy HH:mm:ss"));
                    System.Console.WriteLine("Descripción: {0}", myExecution.Description);
                    System.Console.WriteLine();

                    mySchedule.CurrentDate = myExecution.DateTime;
                }
            }
            catch(Exception exc)
            {                
                System.Console.WriteLine(exc.Message);
            }

            System.Console.ReadLine();
        }
    }
}
