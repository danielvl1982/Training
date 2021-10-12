using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class DailyFrecuencyType
    {
        private static List<DailyFrecuencyType> items;

        private DailyFrecuencyType(string name, bool isRecurring)
        {
            this.IsRecurring = isRecurring;
            this.Name = name;
        }

        public bool IsRecurring { get; internal set; }

        public string Description { get { return this.IsRecurring == true ? "every " + this.Occurs.Description : "once"; } }
        public string Name { get; private set; }

        public DailyOccur Occurs { get; set; }

        public static List<DailyFrecuencyType> Items
        {
            get
            {
                if (DailyFrecuencyType.items == null)
                {
                    DailyFrecuencyType.items = new List<DailyFrecuencyType>();

                    DailyFrecuencyType.LoadItems();
                }

                return DailyFrecuencyType.items;
            }
        }

        public static DailyFrecuencyType GetByName(string name)
        {
            IEnumerable<DailyFrecuencyType> result = (from frecuency in DailyFrecuencyType.Items
                                                where frecuency.Name.ToUpper() == name.ToUpper()
                                                select frecuency);

            return result.Count() == 0 ? throw new Exception("No existe el elemento de configuración " + name) : result.First();
        }

        private static void AddItem(string name, bool isRecurring)
        {
            DailyFrecuencyType.items.Add(new DailyFrecuencyType(name, isRecurring));
        }
        private static void LoadItems()
        {
            DailyFrecuencyType.AddItem("None", false);
            DailyFrecuencyType.AddItem("Hours", true);
            DailyFrecuencyType.AddItem("Minutes", true);
            DailyFrecuencyType.AddItem("Seconds", true);
        }
    }
}
