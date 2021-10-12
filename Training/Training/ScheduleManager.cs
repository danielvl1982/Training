﻿using System;

namespace Training
{
    public class ScheduleManager
    {
        public static void Validate(Schedule schedule)
        {
            if (schedule == null) { throw new Exception("Must instantiate schedule."); }

            if (schedule.StartDate.HasValue == true &&
                schedule.EndDate.HasValue == true &&
                schedule.StartDate.Value.CompareTo(schedule.EndDate.Value) > 0) { throw new Exception("End date must be greater to start date."); }

            ScheduleManager.ValidateTrigger(schedule);
        }

        private static void ValidateTrigger(Schedule schedule)
        {
            if (schedule.Trigger == null) { throw new Exception("Must instantiate trigger."); }
            if (schedule.Trigger.Type == null) { throw new Exception("Must instantiate trigger type."); }

            if (schedule.Trigger.Type.IsRecurring == true &&
                schedule.Trigger.Every <= 0) { throw new Exception("Every must be greater to 0."); }

            if (schedule.Trigger.Type.Occurs.Type == TriggerOccurType.Week &&
                schedule.Trigger.Days.Count == 0) { throw new Exception("Occurs weekly must indicate the days of the week."); }

            ScheduleManager.ValidateTriggerDailyFrecuency(schedule.Trigger);
        }

        private static void ValidateTriggerDailyFrecuency(Trigger trigger)
        {
            if (trigger.Frecuency == null) { return; }
            if (trigger.Frecuency.Type == null) { throw new Exception("Must instantiate frecuency type."); }

            if (trigger.Frecuency.StartTime.HasValue == true &&
                trigger.Frecuency.EndTime.HasValue == true &&
                trigger.Frecuency.StartTime.Value.CompareTo(trigger.Frecuency.EndTime.Value) > 0) { throw new Exception("End at must be greater to stating at."); }

            if (trigger.Frecuency.Type.IsRecurring == true &&
                trigger.Frecuency.Every <= 0) { throw new Exception("Occurs every must be greater to 0."); }
        }
    }
}