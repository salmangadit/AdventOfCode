namespace AdventOfCode.Y2024
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;

    public class Day6 : BaseDay<int, int>
    {
        public Day6() : base(2024, 6) { }

        public List<char> CharacterDirection = new List<char> { '<', '^', '>', 'v' };

        public override int Part1()
        {
            List<List<char>> map;
            (int, int) start;
            CreateMap(out map, out start);
            var completedMap = MoveUntilFinished(map, start);
            return completedMap.Sum(a => a.Count(b => b == 'X'));
        }

        public override int Part2()
        {
            List<List<char>> map;
            (int, int) start;
            CreateMap(out map, out start);

            var loopCount = 0;
            for (int i = 0; i<map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] != '#' && CharacterDirection.All(a => a != map[i][j]))
                    {
                        if (MoveUntilStuck(map, start, (i, j)))
                        {
                            loopCount++;
                        }
                    }
                }
            }
            return loopCount;
        }

        private void CreateMap(out List<List<char>> map, out (int, int) start)
        {
            map = new List<List<char>>();
            start = (0, 0);
            for (int i = 0; i < inputs.Length; i++)
            {
                string line = this.inputs[i];
                var lineValues = new List<char>();
                for (int j = 0; j < line.Length; j++)
                {
                    lineValues.Add(line[j]);

                    if (line[j] == '>' ||
                        line[j] == '<' ||
                        line[j] == '^' ||
                        line[j] == 'v')
                    {
                        start = (i, j);
                    }
                }
                map.Add(lineValues);
            }
        }

        public bool MoveUntilStuck(List<List<char>> map, (int, int) start, (int, int) obstacle)
        {
            var currPos = start;
            var mapWithObstacle = new List<List<char>>();

            foreach(var row in map)
            {
                mapWithObstacle.Add(new List<char>(row));
            }

            // Set obstacle
            mapWithObstacle[obstacle.Item1][obstacle.Item2] = '#';
            var xCount = 0;

            while (!Move(mapWithObstacle, currPos, out currPos, out bool alreadyX)) 
            {
                if (alreadyX)
                {
                    xCount++;
                }

                if (xCount > 1000)
                {
                    return true;
                }
            }
            return false;
        }

        public List<List<char>> MoveUntilFinished(List<List<char>> map, (int, int) start)
        {
            var currPos = start;
            while (!Move(map, currPos, out currPos, out bool alreadyX)) { }
            return map;
        }

        public bool Move(List<List<char>> map, (int, int) currCharacterPos, out (int, int) newCharacterPos, out bool alreadyX)
        {
            if (!CharacterDirection.Any(a => a == map[currCharacterPos.Item1][currCharacterPos.Item2]))
            {
                throw new Exception("Invalid character position");
            }
            alreadyX = false;
            if (map[currCharacterPos.Item1][currCharacterPos.Item2] == '^')
            {
                if (currCharacterPos.Item1 == 0)
                {
                    // Exit
                    map[currCharacterPos.Item1][currCharacterPos.Item2] = 'X';
                    newCharacterPos = (currCharacterPos.Item1 - 1, currCharacterPos.Item2);
                    return true;
                }

                if (map[currCharacterPos.Item1 - 1][currCharacterPos.Item2] == '#')
                {
                    // Rotate
                    map[currCharacterPos.Item1][currCharacterPos.Item2] = '>';
                    newCharacterPos = (currCharacterPos.Item1, currCharacterPos.Item2);
                    return false;
                }
                else
                {
                    // Just move
                    map[currCharacterPos.Item1][currCharacterPos.Item2] = 'X';
                    newCharacterPos = (currCharacterPos.Item1 - 1, currCharacterPos.Item2);
                    alreadyX = map[newCharacterPos.Item1][newCharacterPos.Item2] == 'X';
                    map[newCharacterPos.Item1][newCharacterPos.Item2] = '^';
                    return false;
                }
            }
            else if (map[currCharacterPos.Item1][currCharacterPos.Item2] == '<')
            {
                if (currCharacterPos.Item2 == 0)
                {
                    // Exit
                    map[currCharacterPos.Item1][currCharacterPos.Item2] = 'X';
                    newCharacterPos = (currCharacterPos.Item1, currCharacterPos.Item2 - 1);
                    return true;
                }

                if (map[currCharacterPos.Item1][currCharacterPos.Item2 - 1] == '#')
                {
                    // Rotate
                    map[currCharacterPos.Item1][currCharacterPos.Item2] = '^';
                    newCharacterPos = (currCharacterPos.Item1, currCharacterPos.Item2);
                    return false;
                }
                else
                {
                    // Just move
                    map[currCharacterPos.Item1][currCharacterPos.Item2] = 'X';
                    newCharacterPos = (currCharacterPos.Item1, currCharacterPos.Item2 - 1);
                    alreadyX = map[newCharacterPos.Item1][newCharacterPos.Item2] == 'X';
                    map[newCharacterPos.Item1][newCharacterPos.Item2] = '<';
                    return false;
                }
            }
            else if (map[currCharacterPos.Item1][currCharacterPos.Item2] == 'v')
            {
                if (currCharacterPos.Item1 == map[currCharacterPos.Item1].Count - 1)
                {
                    // Exit
                    map[currCharacterPos.Item1][currCharacterPos.Item2] = 'X';
                    newCharacterPos = (currCharacterPos.Item1 + 1, currCharacterPos.Item2);
                    return true;
                }

                if (map[currCharacterPos.Item1 + 1][currCharacterPos.Item2] == '#')
                {
                    // Rotate
                    map[currCharacterPos.Item1][currCharacterPos.Item2] = '<';
                    newCharacterPos = (currCharacterPos.Item1, currCharacterPos.Item2);
                    return false;
                }
                else
                {
                    // Just move
                    map[currCharacterPos.Item1][currCharacterPos.Item2] = 'X';
                    newCharacterPos = (currCharacterPos.Item1 + 1, currCharacterPos.Item2);
                    alreadyX = map[newCharacterPos.Item1][newCharacterPos.Item2] == 'X';
                    map[newCharacterPos.Item1][newCharacterPos.Item2] = 'v';
                    return false;
                }
            }
            else if (map[currCharacterPos.Item1][currCharacterPos.Item2] == '>')
            {
                if (currCharacterPos.Item2 == map.Count - 1)
                {
                    // Exit
                    map[currCharacterPos.Item1][currCharacterPos.Item2] = 'X';
                    newCharacterPos = (currCharacterPos.Item1, currCharacterPos.Item2 + 1);
                    return true;
                }

                if (map[currCharacterPos.Item1][currCharacterPos.Item2 + 1] == '#')
                {
                    // Rotate
                    map[currCharacterPos.Item1][currCharacterPos.Item2] = 'v';
                    newCharacterPos = (currCharacterPos.Item1, currCharacterPos.Item2);
                    return false;
                }
                else
                {
                    // Just move
                    map[currCharacterPos.Item1][currCharacterPos.Item2] = 'X';
                    newCharacterPos = (currCharacterPos.Item1, currCharacterPos.Item2 + 1);
                    alreadyX = map[newCharacterPos.Item1][newCharacterPos.Item2] == 'X';
                    map[newCharacterPos.Item1][newCharacterPos.Item2] = '>';
                    return false;
                }
            }

            newCharacterPos = currCharacterPos;
            return false;
        }
    }
}
