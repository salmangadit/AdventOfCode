namespace AdventOfCode.Common
{
    using System;

    public class Result
    {
        public static void Wrapper<TType>(int part, Func<TType> function)
        {
            Console.WriteLine("PART " + part);
            Console.WriteLine("Result: " + function());
        }
    }
}
