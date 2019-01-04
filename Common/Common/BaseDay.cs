namespace AdventOfCode.Common
{
    using System;

    public abstract class BaseDay<TDay1Type, TDay2Type> : IDay
    {
        protected string fullInput;
        protected string[] inputs;
        private int day;
        public BaseDay(int year, int day)
        {
            this.day = day;
            this.fullInput = Input.Get(year, day).Result;
            this.inputs = this.fullInput.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        }

        public BaseDay(string input, int day)
        {
            this.day = day;
            this.fullInput = input.Trim();
            this.inputs = this.fullInput.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        }

        public void Run()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("Day: " + this.day);
            Result.Wrapper(1, Part1);
            Result.Wrapper(2, Part2);
        }

        public void Run(string testData)
        {
            this.fullInput = testData;
            this.inputs = this.fullInput.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("====================TEST============================");
            Console.WriteLine();
            Console.WriteLine("Day: " + this.day);
            Result.Wrapper(1, Part1);
            Result.Wrapper(2, Part2);
        }

        public abstract TDay1Type Part1();

        public abstract TDay2Type Part2();
    }
}
