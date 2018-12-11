namespace AdventOfCode
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;

    public class Day9 : BaseDay<int, long>
    {
        public Day9() : base(2018, 9) { }

        public override int Part1()
        {
            var split = Regex.Split(this.fullInput, @"(\d+) players; last marble is worth (\d+) points");
            var playerCount = int.Parse(split[1]);
            var lastMarble = int.Parse(split[2]);
            return (int)HighestScore(playerCount, lastMarble);
        }

        public override long Part2()
        {
            var split = Regex.Split(this.fullInput, @"(\d+) players; last marble is worth (\d+) points");
            var playerCount = int.Parse(split[1]);
            var lastMarble = int.Parse(split[2]) * 100;
            return HighestScore(playerCount, lastMarble);
        }

        private static long HighestScore(int playerCount, int lastMarble)
        {
            Node currentMarble = null;
            Dictionary<int, long> scores = new Dictionary<int, long>();
            // Construct circular linked list
            for (var i = 0; i <= lastMarble; i++)
            {
                if (i == 0)
                {
                    currentMarble = new Node(i);
                    currentMarble.Right = currentMarble;
                    currentMarble.Left = currentMarble;
                }
                else if (i % 23 == 0)
                {
                    var currScore = i;

                    // Traverse left 7
                    var marbleToRemove = currentMarble;
                    for (var j = 0; j < 7; j++)
                    {
                        marbleToRemove = marbleToRemove.Left;
                    }

                    // Remove this marble
                    marbleToRemove.Left.Right = marbleToRemove.Right;
                    marbleToRemove.Right.Left = marbleToRemove.Left;
                    currScore += marbleToRemove.Value;

                    // Current marble now becomes the right
                    currentMarble = marbleToRemove.Right;

                    // Save the score
                    var currPlayer = i % playerCount;
                    if (scores.ContainsKey(currPlayer))
                    {
                        scores[currPlayer] += currScore;
                    }
                    else
                    {
                        scores.Add(currPlayer, currScore);
                    }
                }
                else
                {
                    // Traverse one after current marble
                    var preMarble = currentMarble.Right;
                    var previousRight = preMarble.Right;

                    var newMarble = new Node(i);
                    newMarble.Left = preMarble;
                    newMarble.Right = previousRight;

                    preMarble.Right = newMarble;
                    previousRight.Left = newMarble;

                    currentMarble = newMarble;
                }
            }

            return scores.Max(a => a.Value);
        }

        private class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Value { get; set; }

            public Node(int value)
            {
                this.Value = value;
            }

            public override string ToString()
            {
                return $"{Left.Value} - {Value} - {Right.Value}";
            }
        }
    }
}
