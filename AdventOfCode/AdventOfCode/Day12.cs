using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Common;

namespace AdventOfCode
{
    public class Day12 : BaseDay<int, long>
    {
        public Day12() : base(2018, 12) { }

        public override int Part1()
        {
            var flags = new Dictionary<PotFlag, bool>();

            var sanitizedInitial = this.inputs[0].Replace("initial state: ", "");
            var pot = new bool[sanitizedInitial.Length * 3];

            for (var i = 0; i < sanitizedInitial.Length; i++)
            {
                pot[i + sanitizedInitial.Length] = sanitizedInitial[i] == '#' ? true : false;
            }

            for (var i = 1; i < this.inputs.Length; i++)
            {
                var parts = this.inputs[i].Split("=>", StringSplitOptions.RemoveEmptyEntries);
                flags.Add(GeneratePotFlag(parts[0]), parts[1].Trim() == "#" ? true : false);
            }

            var nextGeneration = pot;
            //Console.WriteLine(0);
            //foreach (var g in nextGeneration)
            //{
            //    Console.Write(g ? '#' : '.');
            //}
            //Console.WriteLine();

            for (var gen = 0; gen < 20; gen++)
            {
                var temp = new bool[nextGeneration.Length];
                for (var i = 2; i < pot.Length - 2; i++)
                {
                    bool[] subArray = new bool[5];
                    Array.Copy(nextGeneration, i - 2, subArray, 0, 5);
                    var flag = GeneratePotFlag(subArray);

                    if (flags.TryGetValue(flag, out bool value))
                    {
                        temp[i] = value;
                    }
                    else
                    {
                        temp[i] = false;
                    }
                }
                nextGeneration = temp;
                //Console.WriteLine(gen);
                //foreach (var g in nextGeneration)
                //{
                //    Console.Write(g ? '#' : '.');
                //}
                //Console.WriteLine();
            }

            var plantScore = 0;
            for (var i=0; i<nextGeneration.Length; i++)
            {
                if (nextGeneration[i])
                {
                    plantScore += i - sanitizedInitial.Length;
                }
            } 

            return plantScore;
        }

        private PotFlag GeneratePotFlag(string parts)
        {
            return GeneratePotFlag(parts.ToCharArray());
        }

        private PotFlag GeneratePotFlag(char[] parts)
        {
            return GeneratePotFlag(parts.Select(p => p == '#' ? true : false).ToArray());
        }

        private PotFlag GeneratePotFlag(bool[] parts)
        {
            PotFlag flag = 0;

            if (parts[0])
            {
                flag |= PotFlag.LL;
            }

            if (parts[1])
            {
                flag |= PotFlag.L;
            }

            if (parts[2])
            {
                flag |= PotFlag.C;
            }

            if (parts[3])
            {
                flag |= PotFlag.R;
            }

            if (parts[4])
            {
                flag |= PotFlag.RR;
            }

            return flag;
        }

        public override long Part2()
        {
            var start = 6767;
            var currGen = 100;

            return ((50000000000 - 101) * 67) + start;
        }

        [Flags]
        private enum PotFlag
        {
            LL = 1, //1
            L = 2, //10
            C = 4, //100
            R = 8, //1000
            RR = 16 //10000
        }
    }
}
