namespace AdventOfCode.Y2024
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;

    public class Day5 : BaseDay<int, int>
    {
        public Day5() : base(2024, 5) { }

        public override int Part1()
        {
            var rules = Parse(out var tests);

            var passedTests = new List<string>();
            foreach (var test in tests)
            {
                var testItems = test.Split(',').Select(a => int.Parse(a)).ToList();
                if (AllTestsPass(testItems, rules))
                {
                    passedTests.Add(test);
                }
            }

            var middleSum = 0;
            foreach (var p in passedTests)
            {
                var parsed = p.Split(',').Select(a => int.Parse(a)).ToArray();
                middleSum += parsed[parsed.Length / 2];
            }

            return middleSum;
        }

        public override int Part2()
        {
            var rules = Parse(out var tests);

            var nonPassedTests = new List<string>();
            foreach (var test in tests)
            {
                var testItems = test.Split(',').Select(a => int.Parse(a)).ToList();
                if (!AllTestsPass(testItems, rules))
                {
                    nonPassedTests.Add(test);
                }
            }

            var middleSum = 0;
            foreach (var p in nonPassedTests)
            {
                var parsed = p.Split(',').Select(a => int.Parse(a)).ToList();
                var fixedTest = parsed;

                while (!AllTestsPass(fixedTest, rules))
                {
                    fixedTest = FixTest(fixedTest, rules);
                }

                middleSum += fixedTest[fixedTest.Count / 2];
            }

            return middleSum;
        }

        public List<int> FixTest(List<int> tests, List<RuleNode> rules)
        {
            var fixedTest = new List<int>(tests);
            bool stop = false;
            for (int i = 0; i < tests.Count; i++)
            {
                var before = tests.Take(i).ToArray();
                var after = tests.Skip(i + 1).ToArray();

                var rule = rules.FirstOrDefault(a => a.Value == tests[i]);

                if (rule == null)
                {
                    continue;
                }
                else
                {
                    // Check everything
                    var allPassCheck = before.All(a => rule.Before.Contains(a)) && after.All(a => rule.After.Contains(a));

                    if (allPassCheck)
                    {
                        continue;
                    }

                    foreach (var b in before)
                    {
                        if (!rule.Before.Contains(b))
                        {
                            // this rule is broken
                            fixedTest.Remove(b);
                            fixedTest.Insert(i + 1, b);
                            stop = true;
                        }
                    }

                    foreach (var a in after)
                    {
                        if (!rule.After.Contains(a))
                        {
                            // this rule is broken
                            fixedTest.Remove(a);
                            fixedTest.Insert(i, a);
                            stop = true;
                        }
                    }

                    if (stop) break;
                }
            }

            return fixedTest;
        }

        public bool AllTestsPass(List<int> tests, List<RuleNode> rules)
        {
            for (int i = 0; i < tests.Count; i++)
            {
                var before = tests.Take(i).ToArray();
                var after = tests.Skip(i + 1).ToArray();

                var rule = rules.FirstOrDefault(a => a.Value == tests[i]);

                if (rule == null)
                {
                    continue;
                }
                else
                {
                    var result = before.All(a => rule.Before.Contains(a)) && after.All(a => rule.After.Contains(a));
                    if (!result) return false;
                }
            }

            return true;
        }

        public List<RuleNode> Parse(out List<string> tests)
        {
            int breakingLine = -1;
            var rules = new List<RuleNode>();
            for (int i = 0; i < inputs.Length; i++)
            {
                string line = this.inputs[i];
                if (line.Contains(","))
                {
                    breakingLine = i;
                    break;
                }

                var nodes = line.Split('|').Select(a => int.Parse(a)).ToArray();

                for (int j = 0; j < 2; j++)
                {
                    var relevantRule = rules.FirstOrDefault(a => a.Value == nodes[j]);
                    if (relevantRule == null)
                    {
                        var newRule = new RuleNode()
                        {
                            Value = nodes[j]
                        };

                        if (j == 0)
                        {
                            newRule.Add(nodes[j + 1], false);
                        }
                        else
                        {
                            newRule.Add(nodes[j - 1], true);
                        }
                        rules.Add(newRule);
                    }
                    else
                    {
                        if (j == 0)
                        {
                            relevantRule.Add(nodes[j + 1], false);
                        }
                        else
                        {
                            relevantRule.Add(nodes[j - 1], true);
                        }
                    }
                }
            }

            tests = inputs.Skip(breakingLine).ToList();
            return rules;
        }

        public class RuleNode
        {
            public int Value { get; set; }
            public HashSet<int> Before { get; set; }
            public HashSet<int> After { get; set; }

            public RuleNode()
            {
                Before = new HashSet<int>();
                After = new HashSet<int>();
            }

            public void Add(int value, bool before)
            {
                if (before)
                {
                    Before.Add(value);
                }
                else
                {
                    After.Add(value);
                }
            }
        }
    }
}
