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
            //new Day1(),
            //new Day2(),
            //new Day3(),
            //new Day4(),
            //new Day6(),
            //new Day7(),
            //new Day8(),
            new Day9()
        };
        static void Main(string[] args)
        {
            foreach (var d in days)
            {
                d.Run();
            }

            //new Day9().Run("10 players; last marble is worth 1618 points");

            Console.WriteLine("Press Enter to End.");
            Console.ReadLine();
        }
    }
}
