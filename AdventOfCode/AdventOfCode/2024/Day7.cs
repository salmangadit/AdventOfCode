namespace AdventOfCode.Y2024
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;

    public class Day7 : BaseDay<long, long>
    {
        public Day7() : base(2024, 7) { }

        public override long Part1()
        {
            long correctSums = 0;
            foreach (var item in this.inputs)
            {
                var parts = item.Split(':');
                var total = long.Parse(parts[0]);
                var inputNumbers = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select( a => long.Parse(a)).ToList();
                var sums = EvaluateSums(inputNumbers, 0, Operator.Add);
                if (sums.Any(a => a == total))
                {
                    correctSums += total;
                }
            }

            return correctSums;
        }

        public List<long> EvaluateSums(List<long> numbers, long prevSum, Operator op, bool allowCon = false)
        {
            var results = new List<long>();
            long sum = op == Operator.Add ? (numbers[0] + prevSum) : op == Operator.Mul ? (numbers[0] * prevSum) : op == Operator.Con ? long.Parse(prevSum.ToString() + numbers[0].ToString()) : numbers[0];
            if (numbers.Count > 1)
            {
                results.AddRange(EvaluateSums(numbers.Skip(1).ToList(), sum, Operator.Add, allowCon));
                results.AddRange(EvaluateSums(numbers.Skip(1).ToList(), sum, Operator.Mul, allowCon));
                if (allowCon)
                {
                    results.AddRange(EvaluateSums(numbers.Skip(1).ToList(), sum, Operator.Con, allowCon));
                }
            }
            else
            {
                results.Add(sum);
            }

            return results;
        }

        public override long Part2()
        {
            long correctSums = 0;
            foreach (var item in this.inputs)
            {
                var parts = item.Split(':');
                var total = long.Parse(parts[0]);
                var inputNumbers = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(a => long.Parse(a)).ToList();
                var sums = EvaluateSums(inputNumbers, 0, Operator.Add, true);
                if (sums.Any(a => a == total))
                {
                    correctSums += total;
                }
            }

            return correctSums;
        }

        public enum Operator
        {
            Add,
            Mul,
            Con
        }
    }
}
