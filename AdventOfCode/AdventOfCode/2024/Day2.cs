namespace AdventOfCode.Y2024
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Common;

    public class Day2 : BaseDay<int, int>
    {
        public Day2() : base(2024, 2) { }

        public override int Part1()
        {
            var safeCount = 0;
            foreach (var item in this.inputs)
            {
                var levels = item.Split(' ').Select(a => int.Parse(a));
                if (IsSafe(levels)) safeCount++;
            }

            return safeCount;
        }

        public override int Part2()
        {
            var safeCount = 0;
            foreach (var item in this.inputs)
            {
                var levels = item.Split(' ').Select(a => int.Parse(a));
                if (IsSafe(levels))
                {
                    safeCount++;
                }
                else
                {
                    for (var i = 0; i < levels.Count(); i++)
                    {
                        var modList = new List<int>(levels);
                        modList.RemoveAt(i);
                        if (IsSafe(modList))
                        {
                            safeCount++;
                            break;
                        }
                    }
                }
            }

            return safeCount;
        }

        private static bool IsSafe(IEnumerable<int> levels)
        {
            var curr = -1;
            var direction = 0;
            var safe = true;
            foreach (var level in levels)
            {
                if (curr < 0)
                {
                    curr = level;
                    continue;
                }
                else
                {
                    var isDecreasing = level < curr;
                    var levelDelta = Math.Abs(level - curr);

                    if (levelDelta >= 1 && levelDelta <= 3 && isDecreasing && (direction <= 0))
                    {
                        direction = -1;
                        curr = level;
                        continue;
                    }
                    else if (levelDelta >= 1 && levelDelta <= 3 && !isDecreasing && (direction >= 0))
                    {
                        direction = 1;
                        curr = level;
                        continue;
                    }
                    else
                    {
                        safe = false;
                        break;
                    }
                }
            }

            return safe;
        }
    }
}
