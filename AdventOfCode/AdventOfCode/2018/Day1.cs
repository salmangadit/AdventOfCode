namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Common;

    public class Day1 : BaseDay<int, int>
    {
        public Day1() : base(2018, 1) { }

        public override int Part1()
        {
            return this.inputs.Select(a => int.Parse(a)).Sum();
        }

        public override int Part2()
        {
            var frequency = new HashSet<int>();
            var result = this.inputs.Select(a => int.Parse(a));

            int curr = 0;
            while (true)
            {
                foreach (var r in result)
                {
                    curr += r;
                    if (frequency.Contains(curr))
                    {
                        return curr;
                    }
                    else
                    {
                        frequency.Add(curr);
                    }
                }
            }
        }
    }
}
