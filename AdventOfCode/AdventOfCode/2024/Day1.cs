namespace AdventOfCode.Y2024
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Common;

    public class Day1 : BaseDay<int, int>
    {
        public Day1() : base(2024, 1) { }

        public override int Part1()
        {
            var left = new List<int>();
            var right = new List<int>();
            foreach (var item in this.inputs)
            {
                var parts = item.Split("   ");
                left.Add(int.Parse(parts[0]));
                right.Add(int.Parse(parts[1]));
            }

            var sleft = left.OrderBy(x => x).ToList();
            var sRight = right.OrderBy(x => x).ToList();

            var dist = 0;
            for (var i = 0; i < sleft.Count(); i++)
            {
                dist += Math.Abs(sleft[i] - sRight[i]);
            }

            return dist;
        }

        public override int Part2()
        {
            var left = new List<int>();
            var right = new List<int>();
            foreach (var item in this.inputs)
            {
                var parts = item.Split("   ");
                left.Add(int.Parse(parts[0]));
                right.Add(int.Parse(parts[1]));
            }

            var sim = 0;
            foreach (var item in left)
            {
                var count = right.Count(a => a == item);
                sim += count * item;
            }

            return sim;
        }

        //public override int Part2()
        //{
        //    var frequency = new HashSet<int>();
        //    var result = inputs.Select(a => int.Parse(a));

        //    int curr = 0;
        //    while (true)
        //    {
        //        foreach (var r in result)
        //        {
        //            curr += r;
        //            if (frequency.Contains(curr))
        //            {
        //                return curr;
        //            }
        //            else
        //            {
        //                frequency.Add(curr);
        //            }
        //        }
        //    }
        //}
    }
}
