namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Common;

    public class Day8 : BaseDay<int, int>
    {
        private Tree tree;

        public Day8() : base(2018, 8) { }

        public override int Part1()
        {
            var values = this.fullInput
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(a => int.Parse(a))
                .ToList();
            this.tree = Tree.BuildTree(values);
            return this.tree.MetadataSum(this.tree.Root);
        }

        public override int Part2()
        {
            return this.tree.ReferenceSum(this.tree.Root);
        }

        private class Tree
        {
            public Node Root { get; set; }

            public int MetadataSum(Node startingNode)
            {
                var childMetaSum = 0;

                foreach (var m in startingNode.Children)
                {
                    childMetaSum += MetadataSum(m);
                }

                return startingNode.Metadata.Sum() + childMetaSum;
            }

            public int ReferenceSum(Node startingNode)
            {
                var metaSum = 0;

                if (startingNode.Children.Count == 0)
                {
                    metaSum = startingNode.Metadata.Sum();
                }
                else
                {
                    foreach (var m in startingNode.Metadata)
                    {
                        if (m > 0 && startingNode.Children.Count >= m)
                        {
                            metaSum += ReferenceSum(startingNode.Children[m - 1]);
                        }
                    }
                }

                return metaSum;
            }

            public static Tree BuildTree(List<int> values)
            {
                var result = BuildNode(values, 0);

                return new Tree
                {
                    Root = result.Item1
                };
            }

            public static (Node, int) BuildNode(List<int> values, int index)
            {
                var childCount = values[index];
                var metaCount = values[index + 1];

                var node = new Node
                {
                    Children = new List<Node>(),
                    Metadata = new List<int>(childCount)
                };

                var nextStartingIndex = index + 2;
                for (var i = 0; i < childCount; i++)
                {
                    var result = BuildNode(values, nextStartingIndex);
                    node.Children.Add(result.Item1);
                    nextStartingIndex = result.Item2;
                }

                // Metadata
                for (var i = nextStartingIndex; i < nextStartingIndex + metaCount; i++)
                {
                    node.Metadata.Add(values[i]);
                }

                return (node, nextStartingIndex + metaCount);
            }
        }

        private class Node
        {
            public List<Node> Children { get; set; }

            public List<int> Metadata { get; set; }
        }
    }
}
