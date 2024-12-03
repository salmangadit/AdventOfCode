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
            //new Day12()
            //new Day13()
            new Y2024.Day1(),
            new Y2024.Day2()
        };
        static void Main(string[] args)
        {
            foreach (var d in days)
            {
                d.Run();
            }
            new Y2024.Day2().Run("7 6 4 2 1\n1 2 7 8 9\n9 7 6 2 1\n1 3 2 4 5\n8 6 4 4 1\n1 3 6 7 9");
            //new Day14().Run();

            Console.WriteLine("Press Enter to End.");
            Console.ReadLine();
        }
    }
}
