namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Common;

    class Program
    {
        static List<IDay> days = new List<IDay>
        {
            new Day1(),
            new Day2(),
            new Day3(),
            new Day4(),
            new Day7()
        };
        static void Main(string[] args)
        {
            foreach (var d in days)
            {
                d.Run();
            }

            var t =
                "1, 1\n" +
                "1, 6\n" +
                "8, 3\n" +
                "3, 4\n" +
                "5, 5\n" +
                "8, 9\n";

            var d6 = new Day6();
            d6.Run(t);

            Console.WriteLine("Press Enter to End.");
            Console.ReadLine();
        }
    }
}
