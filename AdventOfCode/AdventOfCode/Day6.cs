namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Common;

    public class Day6 : BaseDay<int, int>
    {
        public Day6() : base(2018, 6) { }

        public override int Part1()
        {
            var coords = this.inputs.Select((a, i) => Coordinate.Parse(i, a));
            var gp = this.GetGridParams(coords);

            var fullWidth = gp.Item3 + gp.Item1 + 2;
            var fullHeight = gp.Item4 + gp.Item2 + 2;
            var grid = new char[fullWidth, fullHeight];

            // Populate the grid
            for (var i = 0; i < fullWidth; i++)
            {
                for (var j = 0; j < fullHeight; j++)
                {
                    // Find closest distance
                    var minDistance = int.MaxValue;
                    List<Coordinate> minCoordsCandidates = new List<Coordinate>();
                    foreach (var c in coords)
                    {
                        var d = ManhattanDistance(c, new Coordinate { X = i, Y = j });

                        if (d == minDistance)
                        {
                            // Something else is distant here, just negate this
                            minCoordsCandidates.Add(c);
                        }
                        else if (Math.Min(d, minDistance) < minDistance)
                        {
                            minDistance = d;
                            minCoordsCandidates = new List<Coordinate> { c };
                        }
                    }

                    // Minumum found, assign this spot in the grid
                    if (minCoordsCandidates.Count > 1)
                    {
                        grid[i, j] = '.';
                    }
                    else
                    {
                        grid[i, j] = minDistance == 0 ? Char.ToUpper(minCoordsCandidates.First().Id) : minCoordsCandidates.First().Id;
                    }
                }
            }

            // Find infinite candidates
            var infiniteCandidates = new HashSet<char>();
            for (var i = 0; i < fullWidth; i++)
            {
                infiniteCandidates.Add(grid[i, 0]);
                infiniteCandidates.Add(grid[i, fullHeight - 1]);
            }

            for (var j = 0; j < fullHeight; j++)
            {
                infiniteCandidates.Add(grid[0, j]);
                infiniteCandidates.Add(grid[fullWidth - 1, j]);
            }


            var areaCount = new Dictionary<char, int>();
            // Now count the non infinite space
            for (var i = 0; i < fullWidth; i++)
            {
                for (var j = 0; j < fullHeight; j++)
                {
                    var id = Char.ToLower(grid[i, j]);
                    if (!infiniteCandidates.Contains(id))
                    {
                        if (areaCount.ContainsKey(id))
                        {
                            areaCount[id]++;
                        }
                        else
                        {
                            areaCount.Add(id, 1);
                        }
                    }
                }
            }

            return areaCount.Max(a => a.Value);
        }

        public override int Part2()
        {
            var coords = this.inputs.Select((a, i) => Coordinate.Parse(i, a));
            var gp = this.GetGridParams(coords);

            var fullWidth = gp.Item3 + gp.Item1 + 2;
            var fullHeight = gp.Item4 + gp.Item2 + 2;
            var grid = new char[fullWidth, fullHeight];

            var regionSize = 0;
            // Populate the grid
            for (var i = 0; i < fullWidth; i++)
            {
                for (var j = 0; j < fullHeight; j++)
                {
                    var rollingDistance = 0;

                    foreach (var c in coords)
                    {
                        rollingDistance += ManhattanDistance(c, new Coordinate { X = i, Y = j });
                        if (rollingDistance >= 10000)
                        {
                            break;
                        }
                    }

                    if (rollingDistance >= 10000)
                    {
                        continue;
                    }
                    else
                    {
                        regionSize++;
                    }
                }
            }

            return regionSize;
        }

        private (int, int, int, int) GetGridParams(IEnumerable<Coordinate> coords)
        {
            int x, y, w, h;
            x = coords.Min(a => a.X);
            y = coords.Min(a => a.Y);
            w = coords.Max(a => a.X) - x;
            h = coords.Max(a => a.Y) - y;

            return (x, y, w, h);
        }

        private int ManhattanDistance(Coordinate c1, Coordinate c2)
        {
            return Math.Abs(c1.X - c2.X) + Math.Abs(c1.Y - c2.Y);
        }

        private class Coordinate
        {
            public char Id;
            public int X;
            public int Y;

            public static Coordinate Parse(int idx, string i)
            {
                var parts = i.Split(',');
                return new Coordinate
                {
                    Id = (char)(idx + 97),
                    X = int.Parse(parts[0].Trim()),
                    Y = int.Parse(parts[1].Trim())
                };
            }
        }
    }
}
