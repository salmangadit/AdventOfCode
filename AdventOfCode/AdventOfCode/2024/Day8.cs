namespace AdventOfCode.Y2024
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;

    public class Day8 : BaseDay<long, long>
    {
        public Day8() : base(2024, 8) { }

        public override long Part1()
        {
            var positionMap = new Dictionary<char, List<(int, int)>>();
            var mapW = CreatePositionMap(positionMap);
            var mapH = this.inputs.Length - 1;

            var hashMap = new List<(int, int)>();

            foreach (var key in positionMap.Keys)
            {
                for (var i = 0; i < positionMap[key].Count - 1; ++i)
                {
                    for (var j = i + 1; j < positionMap[key].Count; ++j)
                    {
                        hashMap.AddRange(GetHashPositions(positionMap[key][i], positionMap[key][j]));
                    }
                }
            }

            //DrawDebugMap(positionMap, mapW, mapH, hashMap);

            // Eliminate based on map
            return hashMap.Where(a => a.Item1 <= mapW && a.Item1 >= 0 && a.Item2 <= mapH && a.Item2 >= 0).Distinct().Count();
        }

        public override long Part2()
        {
            var positionMap = new Dictionary<char, List<(int, int)>>();
            var mapW = CreatePositionMap(positionMap);
            var mapH = this.inputs.Length - 1;

            var hashMap = new List<(int, int)>();

            foreach (var key in positionMap.Keys)
            {
                for (var i = 0; i < positionMap[key].Count - 1; ++i)
                {
                    for (var j = i + 1; j < positionMap[key].Count; ++j)
                    {
                        hashMap.AddRange(GetHarmonicHashPositions(positionMap[key][i], positionMap[key][j], mapW, mapH));
                    }
                }
            }

            //DrawDebugMap(positionMap, mapW, mapH, hashMap);

            // Eliminate based on map
            return hashMap.Where(a => a.Item1 <= mapW && a.Item1 >= 0 && a.Item2 <= mapH && a.Item2 >= 0).Distinct().Count();
        }

        public List<(int, int)> GetHarmonicHashPositions((int, int) point1, (int, int) point2, int mapW, int mapH)
        {
            List<(int, int)> possiblePositions = new List<(int, int)> { point1, point2 };
            List<(int, int)> allPositions = new List<(int, int)> { point1, point2 };
            bool positionAdded = true;

            var primaryHashes = GetUniqueHashPositions(point1, point2, possiblePositions);

            if (IsValidPosition(primaryHashes[0], mapW, mapH))
            {
                possiblePositions.Add(primaryHashes[0]);
                allPositions.Add(primaryHashes[0]);
            }

            if (IsValidPosition(primaryHashes[1], mapW, mapH))
            {
                possiblePositions.Add(primaryHashes[1]);
                allPositions.Add(primaryHashes[1]);
            }

            (int, int) p1 = point1;
            (int, int) p2 = point2;
            (int, int) hashP1 = primaryHashes[0];
            (int, int) hashP2 = primaryHashes[1];

            do
            {
                positionAdded = false;
                var point1Hashes = GetUniqueHashPositions(p1, hashP1, allPositions);
                allPositions.AddRange(point1Hashes);
                if (point1Hashes.Count > 0 && IsValidPosition(point1Hashes[0], mapW, mapH))
                {
                    possiblePositions.Add(point1Hashes[0]);
                    positionAdded = true;
                }

                if (point1Hashes.Count > 1 && IsValidPosition(point1Hashes[1], mapW, mapH))
                {
                    possiblePositions.Add(point1Hashes[1]);
                    positionAdded = true;
                }

                var point2Hashes = GetUniqueHashPositions(p2, hashP2, allPositions);
                allPositions.AddRange(point2Hashes);
                if (point2Hashes.Count > 0 && IsValidPosition(point2Hashes[0], mapW, mapH))
                {
                    possiblePositions.Add(point2Hashes[0]);
                    positionAdded = true;
                }

                if (point2Hashes.Count > 1 && IsValidPosition(point2Hashes[1], mapW, mapH))
                {
                    possiblePositions.Add(point2Hashes[1]);
                    positionAdded = true;
                }

                // Set new positions. p1 and hashP1 become new chirals, p2 and hashp2
                // p1 = hash1, p2 = hash2
                p1 = hashP1;
                p2 = hashP2;
                hashP1 = point1Hashes.First();
                hashP2 = point2Hashes.First();
            } while (positionAdded);

            return possiblePositions;
        }

        public bool IsValidPosition((int, int) point, int w, int h)
        {
            return point.Item1 <= w && point.Item1 >= 0 && point.Item2 <= h && point.Item2 >= 0;
        }

        public List<(int, int)> GetUniqueHashPositions((int, int) point1, (int, int) point2, List<(int, int)> possiblePositions)
        {
            List<(int, int)> pos = new List<(int, int)>();
            var positions = GetHashPositions(point1, point2);

            foreach (var position in positions)
            {
                if (!possiblePositions.Contains(position))
                {
                    pos.Add(position);
                }
            }

            return pos;
        }

        public List<(int, int)> GetHashPositions((int, int) point1, (int, int) point2)
        {
            var xDis = Math.Abs(point1.Item1 - point2.Item1);
            var yDis = Math.Abs(point1.Item2 - point2.Item2);

            var hash1 = (point1.Item1 <= point2.Item1 ? point1.Item1 - xDis : point1.Item1 + xDis, point1.Item2 <= point2.Item2 ? point1.Item2 - yDis : point1.Item2 + yDis);
            var hash2 = (point2.Item1 <= point1.Item1 ? point2.Item1 - xDis : point2.Item1 + xDis, point2.Item2 <= point1.Item2 ? point2.Item2 - yDis : point2.Item2 + yDis);

            return new List<(int, int)> { hash1, hash2 };
        }

        private int CreatePositionMap(Dictionary<char, List<(int, int)>> positionMap)
        {
            var mapW = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                string line = this.inputs[i];
                mapW = line.Length - 1;
                for (int j = 0; j < line.Length; j++)
                {
                    char c = line[j];

                    if (c != '.')
                    {
                        if (positionMap.TryGetValue(c, out var value))
                        {
                            value.Add((j, i));
                        }
                        else
                        {
                            positionMap.Add(c, new List<(int, int)> { (j, i) });
                        }
                    }
                }
            }

            return mapW;
        }

        private static void DrawDebugMap(Dictionary<char, List<(int, int)>> positionMap, int mapW, int mapH, List<(int, int)> hashMap)
        {
            /// Draw debug map
            for (var i = 0; i <= mapH; i++)
            {
                for (var j = 0; j <= mapW; j++)
                {
                    var anything = positionMap.FirstOrDefault(a => a.Value.Contains((j, i)));
                    if ((int)anything.Key == 0)
                    {
                        var anyHash = hashMap.FirstOrDefault(a => a == (j, i));
                        if (anyHash == (0, 0))
                            Console.Write('.');
                        else
                            Console.Write('#');
                    }
                    else
                    {
                        Console.Write(anything.Key);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
