namespace AdventOfCode.Y2024
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;

    public class Day3 : BaseDay<int, int>
    {
        public Day3() : base(2024, 3) { }

        public override int Part1()
        {
            return ExtractCompleteMultipliers(this.fullInput);
        }

        public override int Part2()
        {
            //(do\(\)).*mul\(\d{ 1,3},\d{ 1,3}\).*.? (don't\(\))
            //var do_dont_batches = new Regex(@"do\(\).*mul\(\d{1,3},\d{1,3}\).*.?don't\(\)");
            // remove all don't-> do()?
            var inputModified = Regex.Replace(this.fullInput, @"(don't\(\)(?:(?!do\(\)).)*(do\(\)|$))", string.Empty, RegexOptions.Singleline);
            return ExtractCompleteMultipliers(inputModified);
        }

        private int ExtractCompleteMultipliers(string input)
        {
            var regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)");
            var matches = regex.Matches(input);

            var mul = 0;
            foreach (Match match in matches)
            {
                var trimmed = match.Value.Replace("mul(", "").Replace(")", "").Split(",").Select(a => int.Parse(a.Trim())).ToArray();
                mul += (trimmed[0] * trimmed[1]);
            }

            return mul;
        }
    }
}
