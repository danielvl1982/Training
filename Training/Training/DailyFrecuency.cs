using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class DailyFrecuency
    {
        private static List<DailyFrecuency> items;

        public DailyFrecuency(string name, bool isRecurring, DailyType? type = null)
        {
            this.DailyFrecuencyIsRecurring = isRecurring;
            this.DailyFrecuencyType = type;
            this.DailyFrecuencyName = name;
        }

        public bool DailyFrecuencyIsRecurring { get; internal set; }

        public DailyType? DailyFrecuencyType { get; private set; }

        public TimeSpan? DailyFrecuencyTime { get; set; }
        public TimeSpan? DailyFrecuencyEndTime { get; set; }
        public TimeSpan? DailyFrecuencyStartTime { get; set; }

        public int DailyFrecuencyEvery { get; set; }

        public string DailyFrecuencyName { get; private set; }

        public string DailyFrecuencyDescription
        {
            get
            {
                string message = string.Empty;

                if (this.DailyFrecuencyIsRecurring == true)
                {
                    message += "every ";
                    message += this.DailyFrecuencyType switch
                    {
                        DailyType.Hour => "hour",
                        DailyType.Minute => "minute",
                        DailyType.Second => "second",
                        _ => string.Empty,
                    };
                }
                else
                {
                    message += "once";
                }

                return message;
            }
        }

        public static List<DailyFrecuency> GetItems()
        {
            if (DailyFrecuency.items == null)
            {
                DailyFrecuency.LoadItems();
            }

            return DailyFrecuency.items;
        }

        public static DailyFrecuency NewByName(string name)
        {
            IEnumerable<DailyFrecuency> result = (from frecuency in DailyFrecuency.GetItems()
                                                  where frecuency.DailyFrecuencyName.ToUpper() == name.ToUpper()
                                                  select frecuency);

            return result.Count() == 0
                ? throw new Exception("No existe el elemento de configuración " + name)
                : (DailyFrecuency)result.First().MemberwiseClone();
        }

        private static void LoadItems()
        {
            DailyFrecuency.items = new List<DailyFrecuency>
            {
                new DailyFrecuency("Once", false),
                new DailyFrecuency("Recurring_Hour", true, DailyType.Hour),
                new DailyFrecuency("Recurring_Minute", true,  DailyType.Minute),
                new DailyFrecuency("Recurring_Second", true, DailyType.Second)
            };
        }
    }
}
