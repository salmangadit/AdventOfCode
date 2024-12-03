namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Common;

    public class Day2 : BaseDay<int, string>
    {
        public Day2() : base(2018, 2) { }

        public override int Part1()
        {
            var twoLetterCount = 0;
            var threeLetterCount = 0;

            foreach (var i in this.inputs)
            {
                var countMap = new Dictionary<char, int>();
                foreach (char c in i)
                {
                    if (countMap.TryGetValue(c, out int count))
                    {
                        countMap[c] = count + 1;
                    }
                    else
                    {
                        countMap.Add(c, 1);
                    }
                }

                if (countMap.ContainsValue(2))
                {
                    twoLetterCount++;
                }

                if (countMap.ContainsValue(3))
                {
                    threeLetterCount++;
                }
            }

            return twoLetterCount * threeLetterCount;
        }

        public override string Part2()
        {
            var inputs = this.inputs.ToList();

            var differenceMap = new Dictionary<int, List<Tuple<string, string>>>();

            for (var i = 0; i < inputs.Count; i++)
            {
                for (var j = i + 1; j < inputs.Count; j++)
                {
                    var diffCount = 0;
                    for (var k = 0; k < inputs[i].Length; k++)
                    {
                        if (inputs[i][k] != inputs[j][k])
                        {
                            diffCount++;
                        }
                    }

                    var tuple = new Tuple<string, string>(inputs[i], inputs[j]);
                    if (differenceMap.ContainsKey(diffCount))
                    {
                        differenceMap[diffCount].Add(tuple);
                    }
                    else
                    {
                        differenceMap.Add(diffCount, new List<Tuple<string, string>> { tuple });
                    }
                }
            }

            differenceMap.TryGetValue(1, out var expected);

            var first = expected.First().Item1;
            var second = expected.First().Item2;

            var common = string.Empty;
            for (var k = 0; k < first.Length; k++)
            {
                if (first[k] == second[k])
                {
                    common += first[k];
                }
            }

            return common;
        }
    }
}
