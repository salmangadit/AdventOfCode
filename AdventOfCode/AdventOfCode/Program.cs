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
            new Y2024.Day2(),
            new Y2024.Day3(),
            new Y2024.Day4()
        };
        static void Main(string[] args)
        {
            foreach (var d in days)
            {
                d.Run();
            }
            //new Y2024.Day4().Run("MMMSXXMASM\nMSAMXMSMSA\nAMXSXMAAMM\nMSAMASMSMX\nXMASAMXAMM\nXXAMMXXAMA\nSMSMSASXSS\nSAXAMASAAA\nMAMMMXMMMM\nMXMXAXMASX");
            //new Day14().Run();

            Console.WriteLine("Press Enter to End.");
            Console.ReadLine();
        }
    }
}
