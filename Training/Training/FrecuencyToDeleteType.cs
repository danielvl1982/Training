using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class FrecuencyToDeleteType
    {
        private static List<FrecuencyToDeleteType> items;

        private FrecuencyToDeleteType(string name, bool isRecurring)
        {
            this.IsRecurring = isRecurring;
            this.Name = name;
        }

        public bool IsRecurring { get; internal set; }

        public string Description { get { return this.IsRecurring == true ? "every " + this.Occurs.Description : "once"; } }
        public string Name { get; private set; }

        public FrecuencyOccur Occurs { get; set; }

        public static List<FrecuencyToDeleteType> GetItems()
        {
                if (FrecuencyToDeleteType.items == null)
                {
                    FrecuencyToDeleteType.LoadItems();
                }

                return FrecuencyToDeleteType.items;
          }

        public static FrecuencyToDeleteType NewByName(string name)
        {
            IEnumerable<FrecuencyToDeleteType> result = (from frecuency in FrecuencyToDeleteType.GetItems()
                                                 where frecuency.Name.ToUpper() == name.ToUpper()
                                                 select frecuency);

            return result.Count() == 0
                ? throw new Exception("No existe el elemento de configuración " + name)
                : (FrecuencyToDeleteType)result.First().MemberwiseClone();
        }

        private static void LoadItems()
        {
            FrecuencyToDeleteType.items = new List<FrecuencyToDeleteType>
            {
                new FrecuencyToDeleteType("Once", false),
                new FrecuencyToDeleteType("Recurring", true)
            };
        }
    }
}
