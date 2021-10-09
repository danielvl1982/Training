using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class TriggerType
    {
        private static List<TriggerType> items;

        private TriggerType(string name, bool isRecurring)
        {
            this.IsRecurring = isRecurring;
            this.Name = name;
        }

        public bool IsRecurring { get; internal set; }

        public string Description { get { return this.IsRecurring == true ? "once" : "every " + this.Occurs.Description; } }
        public string Name { get; internal set; }

        public TriggerOccur Occurs { get; set; }

        public static List<TriggerType> Items
        {
            get
            {
                if (TriggerType.items == null)
                {
                    TriggerType.items = new List<TriggerType>();

                    TriggerType.LoadItems();
                }

                return TriggerType.items;
            }
        }

        public static TriggerType GetByName(string name)
        {
            IEnumerable<TriggerType> result = (from type in TriggerType.Items
                                               where type.Name.ToUpper() == name.ToUpper()
                                               select type);

            return result.Count() == 0 ? throw new System.Exception("No existe el elemento de configuración " + name) : result.First();
        }

        private static void AddItem(string name, bool isRecurring)
        {
            TriggerType.items.Add(new TriggerType(name, isRecurring));
        }
        private static void LoadItems()
        {
            TriggerType.AddItem("Once", false);
            TriggerType.AddItem("Recurring", true);
        }
    }
}
