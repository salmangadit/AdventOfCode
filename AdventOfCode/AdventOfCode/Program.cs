namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using Common;

    class Program
    {
        static List<IDay> days = new List<IDay>
        {
            new Y2024.Day1(),
            new Y2024.Day2(),
            new Y2024.Day3(),
            new Y2024.Day4(),
            new Y2024.Day5(),
            new Y2024.Day6(),
            //new Y2024.Day7(), // Too heavy
            new Y2024.Day8()
        };
        static void Main(string[] args)
        {
            new Y2024.Day8().Run("............\n........0...\n.....0......\n.......0....\n....0.......\n......A.....\n............\n............\n........A...\n.........A..\n............\n............");
            foreach (var d in days)
            {
                d.Run();
            }

            Console.WriteLine("Press Enter to End.");
            Console.ReadLine();
        }
    }
}
