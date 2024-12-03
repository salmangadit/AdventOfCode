namespace AdventOfCode
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;

    public class Day4 : BaseDay<int, int>
    {
        private IEnumerable<Log> orderedLog;
        private Dictionary<int, int> sleepCountDictionary;
        private Dictionary<int, int[]> guardSleepMap;

        public Day4() : base(2018, 4) { }

        public override int Part1()
        {
            var parsedLogs = new List<Log>();
            foreach (var input in inputs)
            {
                parsedLogs.Add(Log.Parse(input));
            }

            this.orderedLog = parsedLogs
                .OrderBy(a => a.Year)
                .ThenBy(b => b.Month)
                .ThenBy(c => c.Day)
                .ThenBy(d => d.Hour)
                .ThenBy(e => e.Minute);

            this.sleepCountDictionary = new Dictionary<int, int>();
            this.guardSleepMap = new Dictionary<int, int[]>();

            var currGuard = -1;
            var currGuardSleep = 0;
            var prevSleepMinute = -1;
            foreach (var i in this.orderedLog)
            {
                if (i.Type == LogType.SHIFT)
                {
                    if (currGuard > -1)
                    {
                        if (sleepCountDictionary.ContainsKey(currGuard))
                        {
                            sleepCountDictionary[currGuard] += currGuardSleep;
                        }
                        else
                        {
                            sleepCountDictionary.Add(currGuard, currGuardSleep);
                        }
                    }

                    currGuardSleep = 0;
                    prevSleepMinute = -1;
                    currGuard = i.Guard;
                    continue;
                }

                if (i.Type == LogType.SLEEP)
                {
                    prevSleepMinute = i.Minute;
                    continue;
                }

                if (i.Type == LogType.WAKE && prevSleepMinute > -1)
                {
                    currGuardSleep += i.Minute - prevSleepMinute;
                    if (currGuard > -1)
                    {
                        if (guardSleepMap.TryGetValue(currGuard, out int[] sleepMap))
                        {
                            // Update with current sleep
                            for (var j = 0; j < 60; j++)
                            {
                                sleepMap[j] += (j >= prevSleepMinute && j < i.Minute ? 1 : 0);
                            }

                            guardSleepMap[currGuard] = sleepMap;
                        }
                        else
                        {
                            // Generate blank sleep map with all true
                            var map = new int[60];
                            for (var j = 0; j < 60; j++)
                            {
                                map[j] = 0;
                            }

                            // Update with current sleep
                            for (var j = 0; j < 60; j++)
                            {
                                map[j] += (j >= prevSleepMinute && j < i.Minute ? 1 : 0);
                            }

                            guardSleepMap.Add(currGuard, map);
                        }
                    }

                    continue;
                }
            }

            var mostSleepyGuard = this.sleepCountDictionary.OrderByDescending(a => a.Value).First().Key;
            var mostSleepyMinute = this.guardSleepMap[mostSleepyGuard].ToList().IndexOf(guardSleepMap[mostSleepyGuard].Max());

            return mostSleepyGuard * mostSleepyMinute;
        }

        public override int Part2()
        {
            var maxSleepMinuteValue = -1;
            var maxSleepMinute = -1;
            var guardId = -1;

            foreach (var sleep in this.guardSleepMap)
            {
                if (sleep.Value.Max() > maxSleepMinuteValue)
                {
                    maxSleepMinuteValue = sleep.Value.Max();
                    maxSleepMinute = sleep.Value.ToList().IndexOf(maxSleepMinuteValue);
                    guardId = sleep.Key;
                }
            }

            return maxSleepMinute * guardId;
        }

        private enum LogType
        {
            SLEEP,
            WAKE,
            SHIFT
        }

        private class Log
        {
            public int Year { get; set; }
            public int Month { get; set; }

            public int Day { get; set; }

            public int Hour { get; set; }

            public int Minute { get; set; }

            public int Guard { get; set; }

            public LogType Type { get; set; }

            public override string ToString()
            {
                return $"[{Year}-{Month}-{Day} {Hour}-{Minute}] {Type} {Guard}";
            }

            // [1518-11-05 00:03] Guard #99 begins shift
            public static Log Parse(string l)
            {
                var result = Regex.Split(l, @"\[(\d+)-(\d+)-(\d+) (\d+):(\d+)\] (\w+) (#(\d+))?");
                var type = result[6] == "wakes" ? LogType.WAKE : result[6] == "falls" ? LogType.SLEEP : LogType.SHIFT;
                return new Log
                {
                    Year = int.Parse(result[1]),
                    Month = int.Parse(result[2]),
                    Day = int.Parse(result[3]),
                    Hour = int.Parse(result[4]),
                    Minute = int.Parse(result[5]),
                    Type = type,
                    Guard = type == LogType.SHIFT ? int.Parse(result[8]) : -1
                };
            }
        }
    }
}
