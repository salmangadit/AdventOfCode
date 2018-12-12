namespace AdventOfCode.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Coordinate
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

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
