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

            Console.WriteLine("Press Enter to End.");
            Console.ReadLine();
        }
    }
}
