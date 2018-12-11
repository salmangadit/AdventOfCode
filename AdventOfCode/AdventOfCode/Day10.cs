namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;

    public class Day10 : BaseDay<string, int>
    {
        private int timeOfMessage = 0;
        public Day10() : base(2018, 10) { }

        public override string Part1()
        {
            var points = this.inputs.Select(a => Point.Parse(a)).ToList();

            int time = 0;
            DrawGrid(points, time++);

            while (true)
            {
                // Adjust each point by time
                for (var j = 0; j < points.Count; j++)
                {
                    points[j].Position.X += points[j].Velocity.X;
                    points[j].Position.Y += points[j].Velocity.Y;
                }

                if (DrawGrid(points, time))
                {
                    timeOfMessage = time;
                    break;
                }
                time++;
            }

            return null;
        }

        private bool DrawGrid(IEnumerable<Point> points, int time)
        {
            var left = points.Min(a => a.Position.X);
            var right = points.Max(a => a.Position.X);
            var top = points.Min(a => a.Position.Y);
            var bottom = points.Max(a => a.Position.Y);
            
            if (bottom - top < 10)
            {                
                var grid = new List<char[]>();

                // Fill with duds
                for (var j = 0; j <= bottom - top; j++)
                {
                    grid.Add(new char[right - left + 1]);
                }

                foreach (var p in points)
                {
                    grid[p.Position.Y - top][p.Position.X - left] = '#';
                }

                for (var j = 0; j <= bottom - top; j++)
                {
                    for (var i = 0; i <= right - left; i++)
                    {
                        Console.Write(grid[j][i]);
                    }

                    Console.WriteLine();
                }
                return true;
            }

            return false;
        }

        public override int Part2()
        {
            return timeOfMessage;
        }

        private class Point
        {
            public Coordinate Position { get; set; }
            public Coordinate Velocity { get; set; }

            public static Point Parse(string input)
            {
                var parts = Regex.Split(input, @"position=<(-? ?\d+), (-? ?\d+)> velocity=<(-? ?\d+), (-? ?\d+)>");
                return new Point
                {
                    Position = new Coordinate(parts[1], parts[2]),
                    Velocity = new Coordinate(parts[3], parts[4])
                };
            }
        }

        private class Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Coordinate(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public Coordinate(string x, string y)
            {
                this.X = int.Parse(x);
                this.Y = int.Parse(y);
            }
        }
    }
}
