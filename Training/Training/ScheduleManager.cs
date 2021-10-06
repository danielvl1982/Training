using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Training
{
    public class ScheduleManager
    {
        private List<TriggerType> triggerTypes;
        private List<TriggerOccur> triggerOccurs;

        public ScheduleManager()
        {
        }

        public List<TriggerOccur> TriggerOccurs
        {
            get
            {
                if (this.triggerOccurs == null)
                {
                    this.triggerOccurs = new List<TriggerOccur>();

                    this.LoadTriggerOccurs();
                }

                return this.triggerOccurs;
            }
        }
        public List<TriggerType> TriggerTypes
        {
            get
            {
                if (this.triggerTypes == null)
                {
                    this.triggerTypes = new List<TriggerType>();

                    this.LoadTriggerTypes();
                }

                return this.triggerTypes;
            }            
        }

        public TriggerOccur GetTriggerOccur(string name)
        {
            return (TriggerOccur)(from occur in this.TriggerOccurs
                                 where occur.Name == name
                                 select occur);
        }
        public TriggerType GetTriggerType(string name)
        {
            return (TriggerType)(from type in this.TriggerTypes
                                 where type.Name == name
                                 select type);
        }

        private void AddTriggerOccur(string name, int recurrinEvery)
        {
            TriggerOccur myTriggerOccur = new TriggerOccur();
            myTriggerOccur.Name = name;
            myTriggerOccur.RecurringEvery = recurrinEvery;

            this.triggerOccurs.Add(myTriggerOccur);
        }
        private void AddTriggerType(string name, bool isRecurring, TriggerOccur occur)
        {
            TriggerType myTriggerType = new TriggerType();
            myTriggerType.IsRecurring = isRecurring;
            myTriggerType.Name = name;
            myTriggerType.Occurs = occur;

            this.triggerTypes.Add(myTriggerType);
        }
        private void LoadTriggerOccurs()
        {
            this.AddTriggerOccur("Dayly", 24);
        }
        private void LoadTriggerTypes()
        {
            this.AddTriggerType("Once", false, this.GetTriggerOccur("Daily"));
            this.AddTriggerType("Recurring", true, this.GetTriggerOccur("Daily"));
        }
    }
}
