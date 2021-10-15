using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class FrecuencyType
    {
        private static List<FrecuencyType> items;

        private FrecuencyType(string name, bool isRecurring)
        {
            this.IsRecurring = isRecurring;
            this.Name = name;
        }

        public bool IsRecurring { get; internal set; }

        public string Description { get { return this.IsRecurring == true ? "every " + this.Occurs.Description : "once"; } }
        public string Name { get; private set; }

        public FrecuencyOccur Occurs { get; set; }

        public static List<FrecuencyType> GetItems()
        {
                if (FrecuencyType.items == null)
                {
                    FrecuencyType.LoadItems();
                }

                return FrecuencyType.items;
          }

        public static FrecuencyType NewByName(string name)
        {
            IEnumerable<FrecuencyType> result = (from frecuency in FrecuencyType.GetItems()
                                                 where frecuency.Name.ToUpper() == name.ToUpper()
                                                 select frecuency);

            return result.Count() == 0
                ? throw new Exception("No existe el elemento de configuración " + name)
                : (FrecuencyType)result.First().MemberwiseClone();
        }

        private static void LoadItems()
        {
            FrecuencyType.items = new List<FrecuencyType>
            {
                new FrecuencyType("Once", false),
                new FrecuencyType("Recurring", true)
            };
        }
    }
}
