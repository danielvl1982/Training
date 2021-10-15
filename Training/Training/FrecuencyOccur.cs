using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public enum FrecuencyOccurType
    {
        Day = 0,
        Week = 1,
        Hour = 2,
        Minute = 3,
        Second = 4
    }

    public class FrecuencyOccur
    {
        private static List<FrecuencyOccur> dailyItems;
        private static List<FrecuencyOccur> triggerItems;

        private FrecuencyOccur(string name, FrecuencyOccurType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public FrecuencyOccurType Type { get; private set; }

        public string Description
        {
            get
            {
                return this.Type switch
                {
                    FrecuencyOccurType.Hour => "hour",
                    FrecuencyOccurType.Minute => "minute",
                    FrecuencyOccurType.Second => "second",
                    _ => string.Empty,
                };
            }
        }

        public string Name { get; private set; }

        public static List<FrecuencyOccur> DailyItems
        {
            get
            {
                if (FrecuencyOccur.dailyItems == null)
                {
                    FrecuencyOccur.LoadDailyItems();
                }

                return FrecuencyOccur.dailyItems;
            }
        }
        public static List<FrecuencyOccur> TriggerItems
        {
            get
            {
                if (FrecuencyOccur.triggerItems == null)
                {
                    FrecuencyOccur.LoadTriggerItems();
                }

                return FrecuencyOccur.triggerItems;
            }
        }

        public static FrecuencyOccur NewByName(string name)
        {
            FrecuencyOccur occur = null;

            if (FrecuencyOccur.DailyItems.Exists(o => o.Name.ToUpper() == name.ToUpper()) == true)
            {
                occur = FrecuencyOccur.DailyItems.Find(o => o.Name.ToUpper() == name.ToUpper());
            }

            if (FrecuencyOccur.TriggerItems.Exists(o => o.Name.ToUpper() == name.ToUpper()) == true)
            {
                occur = FrecuencyOccur.TriggerItems.Find(o => o.Name.ToUpper() == name.ToUpper());
            }

            return occur == null
                ? throw new Exception("Item doesn't exist in configuration " + name)
                : (FrecuencyOccur)occur.MemberwiseClone();
        }

        private static void LoadDailyItems()
        {
            FrecuencyOccur.dailyItems = new List<FrecuencyOccur>();
            FrecuencyOccur.dailyItems.Add(new FrecuencyOccur("Hours", FrecuencyOccurType.Hour));
            FrecuencyOccur.dailyItems.Add(new FrecuencyOccur("Minutes", FrecuencyOccurType.Minute));
            FrecuencyOccur.dailyItems.Add(new FrecuencyOccur("Seconds", FrecuencyOccurType.Second));
        }
        private static void LoadTriggerItems()
        {
            FrecuencyOccur.triggerItems = new List<FrecuencyOccur>();
            FrecuencyOccur.triggerItems.Add(new FrecuencyOccur("Daily", FrecuencyOccurType.Day));
            FrecuencyOccur.triggerItems.Add(new FrecuencyOccur("Weekly", FrecuencyOccurType.Week));
        }
    }
}
