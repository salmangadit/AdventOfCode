namespace AdventOfCode
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;

    public class Day3 : BaseDay<int, int>
    {
        public Day3() : base(2018, 3) { }

        public override int Part1()
        {
            // 0 is default
            // number is Id
            // -1 is overlap
            int[,] fabric = new int[1000, 1000];

            // Initialise it
            for (var i=0; i<1000; i++)
            {
                for (var j=0; j<1000; j++)
                {
                    fabric[i, j] = 0;
                }
            }

            var ct = 0;
            foreach (var input in inputs)
            {
                var claim = Claim.Parse(input);

                for (var i = claim.Top; i < claim.Top + claim.Height; i++)
                {
                    for (var j = claim.Left; j < claim.Left + claim.Width; j++)
                    {
                        if (fabric[i, j] == -1)
                        {
                            continue;
                        }

                        if (fabric[i, j] > 0)
                        {
                            fabric[i, j] = -1;
                            ct++;
                        }
                        else
                        {
                            fabric[i, j] = claim.Id;
                        }
                    }
                }
            }

            var countOverlap = 0;
            
            // Find sq inches of overlap
            for (var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    if (fabric[i, j] < 0)
                    {
                        ++countOverlap;
                    }
                }
            }

            return countOverlap;
        }

        public override int Part2()
        {
            // 0 is default
            // number is Id
            // -1 is overlap
            int[,] fabric = new int[1000, 1000];

            // Initialise it
            for (var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    fabric[i, j] = 0;
                }
            }

            var claimsUntouched = new HashSet<int>();
            foreach (var input in inputs)
            {
                var claim = Claim.Parse(input);
                claimsUntouched.Add(claim.Id);
                for (var i = claim.Top; i < claim.Top + claim.Height; i++)
                {
                    for (var j = claim.Left; j < claim.Left + claim.Width; j++)
                    {
                        if (fabric[i, j] == -1)
                        {
                            claimsUntouched.Remove(claim.Id);
                            continue;
                        }

                        if (fabric[i, j] > 0)
                        {
                            claimsUntouched.Remove(fabric[i, j]);
                            claimsUntouched.Remove(claim.Id);
                            fabric[i, j] = -1;
                        }
                        else
                        {
                            fabric[i, j] = claim.Id;
                        }
                    }
                }
            }

            return claimsUntouched.First();
        }

        private class Claim
        {
            public int Id { get; set; }
            public int Left { get; set; }

            public int Top { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }

            // #1 @ 1,3: 4x4
            public static Claim Parse(string claim)
            {
                var result = Regex.Split(claim, @"#(\d+) @ (\d+),(\d+): (\d+)x(\d+)");
                return new Claim
                {
                    Id = int.Parse(result[1]),
                    Left = int.Parse(result[2]),
                    Top = int.Parse(result[3]),
                    Width = int.Parse(result[4]),
                    Height = int.Parse(result[5])
                };
            }
        }
    }
}
