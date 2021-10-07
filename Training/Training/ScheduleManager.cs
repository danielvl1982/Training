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
                                 where occur.Name.ToUpper() == name.ToUpper()
                                 select occur);
        }
        public TriggerType GetTriggerType(string name)
        {
            return (TriggerType)(from type in this.TriggerTypes
                                 where type.Name.ToUpper() == name.ToUpper()
                                 select type);
        }

        private void AddTriggerOccur(string name, RecurringType type)
        {
            this.triggerOccurs.Add(new TriggerOccur(name, type));
        }
        private void AddTriggerType(string name, bool isRecurring)
        {
            this.triggerTypes.Add(new TriggerType(name, isRecurring));
        }
        private void LoadTriggerOccurs()
        {
            this.AddTriggerOccur("Dayly", RecurringType.day);
        }
        private void LoadTriggerTypes()
        {
            this.AddTriggerType("Once", false);
            this.AddTriggerType("Recurring", true);
        }
    }
}
