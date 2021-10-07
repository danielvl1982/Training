using System;

namespace Training.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Case 1
            ScheduleManager myManager = new ScheduleManager();

            Schedule mySchedule = new Schedule();
            mySchedule.CurrentDate = new DateTime(2020, 1, 04);
            mySchedule.Enabled = true;
            mySchedule.StartDate = new DateTime(2020, 1, 1);

            Trigger myTrigger = new Trigger();
            myTrigger.DateTime = new DateTime(2020, 1, 8);
            myTrigger.Type = myManager.GetTriggerType("Once");

            myTrigger.Type.Occurs = myManager.GetTriggerOccur("Daily");

            mySchedule.Trigger = myTrigger;

            try
            {
                Execution myExecution = mySchedule.GetNextExecution();

                System.Console.WriteLine("Next execution time: {0}", myExecution.DateTime.ToString("dd/MM/yyyy HH:mm:ss"));
                System.Console.WriteLine("Descripción: {0}", myExecution.Description);
            }
            catch(Exception exc)
            {                
                System.Console.WriteLine(exc.Message);
            }

            //Case 2
            mySchedule.CurrentDate = new DateTime(2020, 1, 04);
            mySchedule.Enabled = true;
            mySchedule.StartDate = new DateTime(2020, 1, 1);

            mySchedule.Trigger.DateTime = null;
            mySchedule.Trigger.Every = 1;
            mySchedule.Trigger.Type = myManager.GetTriggerType("Recurring");

            try
            {
                Execution myExecution = mySchedule.GetNextExecution();

                System.Console.WriteLine("Next execution time: {0}", myExecution.DateTime.ToString("dd/MM/yyyy HH:mm:ss"));
                System.Console.WriteLine("Descripción: {0}", myExecution.Description);

                myExecution = mySchedule.GetNextExecution();

                System.Console.WriteLine("Next execution time: {0}", myExecution.DateTime.ToString("dd/MM/yyyy HH:mm:ss"));
                System.Console.WriteLine("Descripción: {0}", myExecution.Description);

                myExecution = mySchedule.GetNextExecution();

                System.Console.WriteLine("Next execution time: {0}", myExecution.DateTime.ToString("dd/MM/yyyy HH:mm:ss"));
                System.Console.WriteLine("Descripción: {0}", myExecution.Description);

                myExecution = mySchedule.GetNextExecution();

                System.Console.WriteLine("Next execution time: {0}", myExecution.DateTime.ToString("dd/MM/yyyy HH:mm:ss"));
                System.Console.WriteLine("Descripción: {0}", myExecution.Description);
            }
            catch (Exception exc)
            {
                System.Console.WriteLine(exc.Message);
            }
        }
    }
}
