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
            new Y2024.Day4(),
            new Y2024.Day5()
        };
        static void Main(string[] args)
        {
            foreach (var d in days)
            {
                d.Run();
            }
            //new Y2024.Day5().Run("47|53\n97|13\n97|61\n97|47\n75|29\n61|13\n75|53\n29|13\n97|29\n53|29\n61|53\n97|53\n61|29\n47|13\n75|47\n97|75\n47|61\n75|61\n47|29\n75|13\n53|13\n\n75,47,61,53,29\n97,61,53,29,13\n75,29,13\n75,97,47,61,53\n61,13,29\n97,13,75,29,47");
            //new Day14().Run();

            Console.WriteLine("Press Enter to End.");
            Console.ReadLine();
        }
    }
}
