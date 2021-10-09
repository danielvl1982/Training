using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public enum RecurringType
    {
        day = 0,
        week = 1
    }

    public class TriggerOccur
    {
        private static List<TriggerOccur> items;

        private TriggerOccur(string name, RecurringType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public RecurringType Type { get; private set; }

        public string Description
        {
            get
            {
                return this.Type switch
                {
                    RecurringType.day => "day",
                    RecurringType.week => "week",
                    _ => string.Empty,
                };
            }
        }

        public string Name { get; private set; }

        public static List<TriggerOccur> Items
        {
            get
            {
                if (TriggerOccur.items == null)
                {
                    TriggerOccur.items = new List<TriggerOccur>();

                    TriggerOccur.LoadItems();
                }

                return TriggerOccur.items;
            }
        }

        public static TriggerOccur GetByName(string name)
        {
            IEnumerable<TriggerOccur> result = (from occur in TriggerOccur.Items
                                                where occur.Name.ToUpper() == name.ToUpper()
                                                select occur);

            return result.Count() == 0 ? throw new System.Exception("No existe el elemento de configuración " + name) : result.First();
        }
        private static void AddItem(string name, RecurringType type)
        {
            TriggerOccur.items.Add(new TriggerOccur(name, type));
        }
        private static void LoadItems()
        {
            TriggerOccur.AddItem("Daily", RecurringType.day);
        }
    }
}