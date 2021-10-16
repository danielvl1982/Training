using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public enum FrecuencyDailyType
    {
        Hour = 0,
        Minute = 1,
        Second = 2
    }

    public enum FrecuencyType
    {
        Day = 0,
        Week = 1
    }

    public class FrecuencyOccur
    {
        private static List<FrecuencyOccur> dailyItems;
        private static List<FrecuencyOccur> triggerItems;

        private FrecuencyOccur(string name, FrecuencyDailyType type)
        {
            this.Name = name;
            this.DailyType = type;
        }
        private FrecuencyOccur(string name, FrecuencyType type)
        {
            this.Name = name;
            this.InitialType = type;
        }

        public FrecuencyDailyType DailyType { get; private set; }

        public FrecuencyType InitialType { get; private set; }

        public string Description
        {
            get
            {
                return this.DailyType switch
                {
                    FrecuencyDailyType.Hour => "hour",
                    FrecuencyDailyType.Minute => "minute",
                    FrecuencyDailyType.Second => "second",
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
                new FrecuencyOccur("Hours", FrecuencyDailyType.Hour),
                new FrecuencyOccur("Minutes", FrecuencyDailyType.Minute),
                new FrecuencyOccur("Seconds", FrecuencyDailyType.Second)
            };
        }
        private static void LoadTriggerItems()
        {
            FrecuencyOccur.triggerItems = new List<FrecuencyOccur>
            {
                new FrecuencyOccur("Daily", FrecuencyType.Day),
                new FrecuencyOccur("Weekly", FrecuencyType.Week)
            };
        }
    }
}
