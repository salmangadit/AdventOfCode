namespace AdventOfCode.Y2024
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;

    public class Day4 : BaseDay<int, int>
    {
        public Day4() : base(2024, 4) { }

        public override int Part1()
        {
            List<List<Node>> nodes = BuildGraph();

            var nodesWithX = nodes.SelectMany(a => a.Where(b => b.Value == 'X'));
            var xmas = 0;
            foreach (var node in nodesWithX)
            {
                xmas += node.Scan("XMAS");
            }

            return xmas;
        }

        public override int Part2()
        {
            List<List<Node>> nodes = BuildGraph();

            var nodesWithA = nodes.SelectMany(a => a.Where(b => b.Value == 'A'));
            var xmas = 0;
            foreach (var node in nodesWithA)
            {
                xmas += (node.ScanMas() ? 1 : 0);
            }

            return xmas;
        }

        private List<List<Node>> BuildGraph()
        {
            var nodes = new List<List<Node>>();

            List<Node> prevRow = null;
            for (int x = 0; x < inputs.Length; x++)
            {
                var currRow = new List<Node>();
                string item = this.inputs[x];
                for (int y = 0; y < item.Length; y++)
                {
                    char c = item[y];
                    currRow.Add(new Node
                    {
                        Value = c,
                        X = x,
                        Y = y
                    });
                }

                for (int y = 0; y < currRow.Count; y++)
                {
                    if (y > 0)
                    {
                        currRow[y].Left = currRow[y - 1];
                    }

                    if (y < currRow.Count - 1)
                    {
                        currRow[y].Right = currRow[y + 1];
                    }

                    if (prevRow != null)
                    {
                        prevRow[y].Bottom = currRow[y];
                        currRow[y].Top = prevRow[y];

                        if (y > 0)
                        {
                            currRow[y].TopLeft = prevRow[y - 1];
                            prevRow[y - 1].RightBottom = currRow[y];
                        }

                        if (y < currRow.Count - 1)
                        {
                            currRow[y].TopRight = prevRow[y + 1];
                            prevRow[y + 1].LeftBottom = currRow[y];
                        }
                    }
                }

                nodes.Add(currRow);
                prevRow = currRow;
            }

            return nodes;
        }

        class Node
        {
            public char Value { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public Node Left { get; set; }
            public Node TopLeft { get; set; }
            public Node Top { get; set; }
            public Node TopRight { get; set; }
            public Node Right { get; set; }
            public Node RightBottom { get; set; }
            public Node Bottom { get; set; }
            public Node LeftBottom { get; set; }

            public bool ScanMas()
            {
                return Value == 'A' &&
                    ((TopLeft?.Value == 'M' && RightBottom?.Value == 'S') || (TopLeft?.Value == 'S' && RightBottom?.Value == 'M')) &&
                    ((TopRight?.Value == 'M' && LeftBottom?.Value == 'S') || (TopRight?.Value == 'S' && LeftBottom?.Value == 'M'));
            }

            public int Scan(string pattern, byte? dir = null)
            {
                var currChar = pattern[0];

                if (currChar != Value)
                {
                    return 0;
                }

                if (pattern.Length == 1)
                {
                    return (Value == currChar) ? 1 : 0;
                }
                else
                {
                    var remaining = pattern.Substring(1);
                    if (dir.HasValue)
                    {
                        switch (dir.Value)
                        {
                            case 1:
                                return Right?.Scan(remaining, dir.Value) ?? 0;
                            case 2:
                                return RightBottom?.Scan(remaining, dir.Value) ?? 0;
                            case 3:
                                return Bottom?.Scan(remaining, dir.Value) ?? 0;
                            case 4:
                                return LeftBottom?.Scan(remaining, dir.Value) ?? 0;
                            case 5:
                                return Left?.Scan(remaining, dir.Value) ?? 0;
                            case 6:
                                return TopLeft?.Scan(remaining, dir.Value) ?? 0;
                            case 7:
                                return Top?.Scan(remaining, dir.Value) ?? 0;
                            case 8:
                                return TopRight?.Scan(remaining, dir.Value) ?? 0;

                        }

                        return 0;
                    }
                    else
                    {
                        return (Right?.Scan(remaining, 1) ?? 0) +
                            (RightBottom?.Scan(remaining, 2) ?? 0) +
                            (Bottom?.Scan(remaining, 3) ?? 0) +
                            (LeftBottom?.Scan(remaining, 4) ?? 0) +
                            (Left?.Scan(remaining, 5) ?? 0) +
                            (TopLeft?.Scan(remaining, 6) ?? 0) +
                            (Top?.Scan(remaining, 7) ?? 0) +
                            (TopRight?.Scan(remaining, 8) ?? 0);
                    }
                }
            }
        }
    }
}
