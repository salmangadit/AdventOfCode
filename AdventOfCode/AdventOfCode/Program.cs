namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using Common;

    class Program
    {
        static List<IDay> days = new List<IDay>
        {
            //new Day1(),
            //new Day2(),
            //new Day3(),
            //new Day4(),
            //new Day6(),
            //new Day7(),
            //new Day8(),
            //new Day9(),
            //new Day10()
            //new Day11()
            new Day12()
        };
        static void Main(string[] args)
        {
            foreach (var d in days)
            {
                d.Run();
            }

            var t =
                "initial state: #..#.#..##......###...###\n" +
                "\n" +
                "...## => #\n" +
                "..#.. => #\n" +
                ".#... => #\n" +
                ".#.#. => #\n" +
                ".#.## => #\n" +
                ".##.. => #\n" +
                ".#### => #\n" +
                "#.#.# => #\n" +
                "#.### => #\n" +
                "##.#. => #\n" +
                "##.## => #\n" +
                "###.. => #\n" +
                "###.# => #\n" +
                "####. => #\n";

            new Day12().Run(t);

            Console.WriteLine("Press Enter to End.");
            Console.ReadLine();
        }
    }
}
