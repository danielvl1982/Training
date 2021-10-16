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

        public static FrecuencyOccur NewByName(string name)
        {
            FrecuencyOccur occur = null;

            if (FrecuencyOccur.GetDailyItems().Exists(o => o.Name.ToUpper() == name.ToUpper()) == true)
            {
                occur = FrecuencyOccur.GetDailyItems().Find(o => o.Name.ToUpper() == name.ToUpper());
            }

            if (occur == null &&
                FrecuencyOccur.GetTriggerItems().Exists(o => o.Name.ToUpper() == name.ToUpper()) == true)
            {
                occur = FrecuencyOccur.GetTriggerItems().Find(o => o.Name.ToUpper() == name.ToUpper());
            }

            return occur == null
                ? throw new Exception("Item doesn't exist in configuration " + name)
                : (FrecuencyOccur)occur.MemberwiseClone();
        }

        public static List<FrecuencyOccur> GetDailyItems()
        {
            if (FrecuencyOccur.dailyItems == null)
            {
                FrecuencyOccur.LoadDailyItems();
            }

            return FrecuencyOccur.dailyItems;
        }
        public static List<FrecuencyOccur> GetTriggerItems()
        {
            if (FrecuencyOccur.triggerItems == null)
            {
                FrecuencyOccur.LoadTriggerItems();
            }

            return FrecuencyOccur.triggerItems;
        }

        private static void LoadDailyItems()
        {
            FrecuencyOccur.dailyItems = new List<FrecuencyOccur>
            {
                new FrecuencyOccur("Hours", FrecuencyOccurType.Hour),
                new FrecuencyOccur("Minutes", FrecuencyOccurType.Minute),
                new FrecuencyOccur("Seconds", FrecuencyOccurType.Second)
            };
        }
        private static void LoadTriggerItems()
        {
            FrecuencyOccur.triggerItems = new List<FrecuencyOccur>
            {
                new FrecuencyOccur("Daily", FrecuencyOccurType.Day),
                new FrecuencyOccur("Weekly", FrecuencyOccurType.Week)
            };
        }
    }
}
