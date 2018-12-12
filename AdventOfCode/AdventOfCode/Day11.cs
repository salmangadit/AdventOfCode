namespace AdventOfCode
{
    using System;
    using Common;

    public class Day11 : BaseDay<(int, int), (int, int, int)>
    {
        private int serialNum = 0;
        private int[,] powerGrid = new int[301, 301];

        public Day11() : base("9995", 11)
        {
            serialNum = int.Parse(this.fullInput);
        }

        public override (int, int) Part1()
        {
            var maxPower = int.MinValue;
            (int, int) maxCoords = (0, 0);

            for (var j = 1; j < 301; j++)
            {
                for (var i = 1; i < 301; i++)
                {
                    powerGrid[i, j] = Power(i, j) + powerGrid[i - 1, j] + powerGrid[i, j - 1] - powerGrid[i - 1, j - 1];
                }
            }

            for (var j = 1; j < 298; j++)
            {
                for (var i = 1; i < 298; i++)
                {
                    var power = PowerNxN(3, i, j);
                    if (power > maxPower)
                    {
                        maxPower = power;
                        maxCoords = (i + 1, j + 1);
                    }
                }
            }

            return maxCoords;
        }

        public override (int, int, int) Part2()
        {
            var maxPower = int.MinValue;
            (int, int) maxCoords = (0, 0);
            int gridSize = 1;

            for (var j = 1; j < 298; j++)
            {
                for (var i = 1; i < 298; i++)
                {
                    for (var n = 0; n < 301 - Math.Max(i, j); n++)
                    {
                        var power = PowerNxN(n, i, j);
                        if (power > maxPower)
                        {
                            maxPower = power;
                            maxCoords = (i + 1, j + 1);
                            gridSize = n;
                        }
                    }
                }
            }

            return (maxCoords.Item1, maxCoords.Item2, gridSize);
        }

        private int PowerNxN(int n, int i, int j)
        {
            return powerGrid[i + n, j + n] + powerGrid[i, j] - powerGrid[i + n, j] - powerGrid[i, j + n];
        }

        private int Power(int x, int y)
        {
            var r = x + 10;
            var s = ((r * y) + serialNum) * r;
            var h = (s / 100) % 10;
            return h - 5;
        }
    }
}
