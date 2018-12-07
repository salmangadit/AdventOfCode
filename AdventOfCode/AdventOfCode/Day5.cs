namespace AdventOfCode
{
    using System;
    using System.Linq;
    using AdventOfCode.Common;

    public class Day5 : BaseDay<int, int>
    {
        public Day5() : base(2018, 5) { }

        public override int Part1()
        {
            var modifiedPolymer = Reduce(this.fullInput);
            return modifiedPolymer.Length;
        }

        public override int Part2()
        {
            var lengths = new int[26];
            // Rewrite the polymer fully per character A-Z
            for (var i=65; i<= 90; i++)
            {
                var rewrittenBasePolymer = this.fullInput.Replace(((char)i).ToString(), "").Replace(((char)(i + 32)).ToString(), "");
                var modifiedPolymer = Reduce(rewrittenBasePolymer);
                lengths[i - 65] = modifiedPolymer.Length;
            }

            return lengths.Min();
        }

        private string Reduce(string polymer)
        {
            if (polymer.Length <= 1)
            {
                return polymer;
            }
            else if (polymer.Length == 2)
            {
                if (Math.Abs(polymer[0] - polymer[1]) == 32)
                {
                    return string.Empty;
                }
                else
                {
                    return polymer;
                }
            }
            else
            {
                var half = (int)Math.Floor((double)polymer.Length / 2);
                if (Math.Abs(polymer[half - 1] - polymer[half]) == 32)
                {
                    return Merge(Reduce(polymer.Substring(0, half - 1)), Reduce(polymer.Substring(half + 1)));
                }
                else
                {
                    return Merge(Reduce(polymer.Substring(0, half)), Reduce(polymer.Substring(half)));
                }
            }
        }

        private string Merge(string a, string b)
        {
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
            {
                return a + b;
            }
            else if (Math.Abs(a.Last() - b.First()) == 32)
            {
                return Merge(a.Substring(0, a.Length - 1), b.Substring(1));
            }
            else
            {
                return a + b;
            }
        }
    }
}
