using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class Frecuency
    {
        private static List<Frecuency> items;

        private Frecuency(string name, bool isRecurring, FrecuencyType type)
        {
            this.IsRecurring = isRecurring;
            this.Type = type;
            this.Name = name;
        }

        public bool IsRecurring { get; internal set; }

        public string Name { get; private set; }

        public FrecuencyType Type { get; set; }

        public string Description
        {
            get
            {
                string message = string.Empty;

                if (this.IsRecurring == true)
                {
                    message += "every ";
                    message += this.Type switch
                    {
                        FrecuencyType.Day => "day",
                        FrecuencyType.Week => "week",
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

        public static List<Frecuency> GetItems()
        {
                if (Frecuency.items == null)
                {
                    Frecuency.LoadItems();
                }

                return Frecuency.items;
          }

        public static Frecuency NewByName(string name)
        {
            IEnumerable<Frecuency> result = (from frecuency in Frecuency.GetItems()
                                                 where frecuency.Name.ToUpper() == name.ToUpper()
                                                 select frecuency);

            return result.Count() == 0
                ? throw new Exception("No existe el elemento de configuración " + name)
                : (Frecuency)result.First().MemberwiseClone();
        }

        private static void LoadItems()
        {
            Frecuency.items = new List<Frecuency>
            {
                new Frecuency("Once", false, FrecuencyType.Day),
                new Frecuency("Recurring_Day", true, FrecuencyType.Day),
                new Frecuency("Recurring_Week", true, FrecuencyType.Week)
            };
        }
    }
}
