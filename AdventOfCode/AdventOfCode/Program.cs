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
            new Y2024.Day7()
        };
        static void Main(string[] args)
        {
            new Y2024.Day7().Run("190: 10 19\n3267: 81 40 27\n83: 17 5\n156: 15 6\n7290: 6 8 6 15\n161011: 16 10 13\n192: 17 8 14\n21037: 9 7 18 13\n292: 11 6 16 20");
            foreach (var d in days)
            {
                d.Run();
            }

            Console.WriteLine("Press Enter to End.");
            Console.ReadLine();
        }
    }
}
