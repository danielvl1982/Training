using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public enum DailyOccurType
    {
        Hour = 0,
        Minute = 1,
        Second = 2
    }

    public class DailyOccur
    {
        private static List<DailyOccur> items;

        private DailyOccur(string name, DailyOccurType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public DailyOccurType Type { get; private set; }

        public string Description
        {
            get
            {
                return this.Type switch
                {
                    DailyOccurType.Hour => "hour",
                    DailyOccurType.Minute => "minute",
                    DailyOccurType.Second => "second",
                    _ => string.Empty,
                };
            }
        }

        public string Name { get; private set; }

        public static List<DailyOccur> Items
        {
            get
            {
                if (DailyOccur.items == null)
                {
                    DailyOccur.items = new List<DailyOccur>();

                    DailyOccur.LoadItems();
                }

                return DailyOccur.items;
            }
        }

        public static DailyOccur GetByName(string name)
        {
            IEnumerable<DailyOccur> result = (from occur in DailyOccur.Items
                                                where occur.Name.ToUpper() == name.ToUpper()
                                                select occur);

            return result.Count() == 0 ? throw new Exception("No existe el elemento de configuración " + name) : result.First();
        }

        private static void AddItem(string name, DailyOccurType type)
        {
            DailyOccur.items.Add(new DailyOccur(name, type));
        }
        private static void LoadItems()
        {
            DailyOccur.AddItem("Daily", DailyOccurType.Hour);
            DailyOccur.AddItem("Weekly", DailyOccurType.Minute);
            DailyOccur.AddItem("Weekly", DailyOccurType.Second);
        }
    }
}
