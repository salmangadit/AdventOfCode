namespace AdventOfCode
{
    using AdventOfCode.Common;

    public class Day14 : BaseDay<string, int>
    {
        public Day14() : base(2018, 14) { }

        public override string Part1()
        {
            // Create first two nodes and link
            var root = new Node(3);
            root.Next = new Node(7);

            var elf1 = root;
            var elf2 = root.Next;
            var last = root.Next;

            var chainLength = 2;
            var inputVal = int.Parse(this.fullInput);

            while (chainLength < inputVal + 10)
            {
                var recipeScore = elf1.Value + elf2.Value;

                if (recipeScore < 10)
                    AddToChain(ref last, ref chainLength, recipeScore);
                else
                {
                    AddToChain(ref last, ref chainLength, recipeScore / 10);
                    AddToChain(ref last, ref chainLength, recipeScore % 10);
                }

                // Shift elf pointers
                var e1Val = elf1.Value + 1;
                for (var i = 0; i < e1Val; i++)
                {
                    elf1 = elf1.Next ?? root;
                }

                var e2Val = elf2.Value + 1;
                for (var i = 0; i < e2Val; i++)
                {
                    elf2 = elf2.Next ?? root;
                }
            }

            // Find value of last 10
            var pointer = root;
            for (var i = 0; i < chainLength - 10; i++)
            {
                pointer = pointer.Next;
            }

            var value = "";
            for (var i = 0; i < 10; i++)
            {
                value += pointer.ToString();
                pointer = pointer.Next;
            }

            return value;
        }

        public override int Part2()
        {
            // Create first two nodes and link
            var root = new Node(3);
            root.Next = new Node(7);

            var elf1 = root;
            var elf2 = root.Next;
            var last = root.Next;

            var chainLength = 2;

            var seekIndex = 0;
            var chainLengthAtSeek = 0;
            this.fullInput = this.fullInput.Trim();

            while (seekIndex < this.fullInput.Length)
            {
                var recipeScore = elf1.Value + elf2.Value;

                if (recipeScore < 10)
                    AddToChain(ref last, ref chainLength, ref seekIndex, ref chainLengthAtSeek, recipeScore);
                else
                {
                    // Split and add both items
                    AddToChain(ref last, ref chainLength, ref seekIndex, ref chainLengthAtSeek, recipeScore / 10);

                    if (seekIndex >= this.fullInput.Length)
                    {
                        break;
                    }

                    AddToChain(ref last, ref chainLength, ref seekIndex, ref chainLengthAtSeek, recipeScore % 10);
                }

                // Shift elf pointers
                var e1Val = elf1.Value + 1;
                for (var i = 0; i < e1Val; i++)
                {
                    elf1 = elf1.Next ?? root;
                }

                var e2Val = elf2.Value + 1;
                for (var i = 0; i < e2Val; i++)
                {
                    elf2 = elf2.Next ?? root;
                }
            }

            return chainLengthAtSeek;
        }

        private void AddToChain(ref Node last, ref int chainLength, int recipeScore)
        {
            // Add single element to chain
            last.Next = new Node(recipeScore);
            last = last.Next;
            chainLength++;
        }

        private void AddToChain(ref Node last, ref int chainLength, ref int seekIndex, ref int chainLengthAtSeek, int recipeScore)
        {
            // Add single element to chain
            last.Next = new Node(recipeScore);
            last = last.Next;

            // Check if seek should be incremented
            if (recipeScore == char.GetNumericValue(this.fullInput[seekIndex]))
            {
                if (seekIndex == 0)
                {
                    chainLengthAtSeek = chainLength;
                }
                seekIndex++;
            }
            else
            {
                seekIndex = 0;
            }

            // Check if it meets first element condition
            if (seekIndex < this.fullInput.Length && recipeScore == char.GetNumericValue(this.fullInput[seekIndex]))
            {
                if (seekIndex == 0)
                {
                    chainLengthAtSeek = chainLength;
                }
                seekIndex++;
            }

            chainLength++;
        }

        private class Node
        {
            public int Value;
            public Node Next;

            public Node(int v)
            {
                this.Value = v;
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }
    }
}
