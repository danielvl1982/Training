using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public enum TriggerOccurType
    {
        Day = 0,
        Week = 1
    }

    public class TriggerOccur
    {
        private static List<TriggerOccur> items;

        private TriggerOccur(string name, TriggerOccurType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public TriggerOccurType Type { get; private set; }

        public string Description
        {
            get
            {
                return this.Type switch
                {
                    TriggerOccurType.Day => "day",
                    TriggerOccurType.Week => "week",
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

            return result.Count() == 0 ? throw new Exception("No existe el elemento de configuración " + name) : result.First();
        }

        private static void AddItem(string name, TriggerOccurType type)
        {
            TriggerOccur.items.Add(new TriggerOccur(name, type));
        }
        private static void LoadItems()
        {
            TriggerOccur.AddItem("Daily", TriggerOccurType.Day);
            TriggerOccur.AddItem("Weekly", TriggerOccurType.Week);
        }
    }
}