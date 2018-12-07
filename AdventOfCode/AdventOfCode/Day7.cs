namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;

    public class Day7 : BaseDay<string, int>
    {
        private Dictionary<string, Node> nodeMap;
        private Tree tree;

        public Day7() : base(2018, 7) { }

        public override string Part1()
        {
            nodeMap = new Dictionary<string, Node>();
            tree = new Tree();

            foreach (var input in this.inputs)
            {
                var steps = Regex.Split(input, @"Step (\w+) must be finished before step (\w+) can begin.");

                // First step
                if (nodeMap.TryGetValue(steps[1], out Node node))
                {
                    node.Next.Add(steps[2]);
                    node.Next = node.Next.OrderBy(a => a).ToHashSet();
                }
                else
                {
                    var n = new Node(steps[1])
                    {
                        Next = new HashSet<string> { steps[2] }
                    };
                    nodeMap.Add(steps[1], n);
                }

                // Second step
                if (nodeMap.TryGetValue(steps[2], out node))
                {
                    node.Previous.Add(steps[1]);
                    node.Previous = node.Previous.OrderBy(a => a).ToHashSet();
                }
                else
                {
                    var n = new Node(steps[2])
                    {
                        Previous = new HashSet<string> { steps[1] }
                    };
                    nodeMap.Add(steps[2], n);
                }
            }

            tree.FindRoots(nodeMap);
            var order = string.Empty;

            Node currentNode = null;
            var previousOptions = tree.Roots;
            while (!tree.IsComplete(nodeMap))
            {
                var options = new List<Node>();
                if (currentNode != null)
                {
                    options = currentNode.ImmediateOptions(nodeMap);
                }

                foreach (var o in previousOptions)
                {
                    if (!options.Any(a => a.Id == o))
                    {
                        options.Add(nodeMap[o]);
                    }
                }

                options = options.OrderBy(a => a.Id).ToList();
                currentNode = options.First();
                currentNode.IsCompleted = true;
                previousOptions = options.Where(a => !a.IsCompleted).Select(b => b.Id).ToList();
                order += currentNode.Id;
            }

            return order;
        }

        public override int Part2()
        {
            // Clear all nodes first
            foreach (var n in nodeMap)
            {
                n.Value.IsCompleted = false;
            }

            var timer = 0;
            var busyNodes = new List<Node>(5);
            int workerLimit = 5;

            var order = string.Empty;

            Node currentNode = null;
            var previousOptions = tree.Roots;
            while (!tree.IsComplete(nodeMap))
            {
                var options = new List<Node>();
                if (currentNode != null)
                {
                    options = currentNode.ImmediateOptions(nodeMap);
                }

                foreach (var o in previousOptions)
                {
                    if (!options.Any(a => a.Id == o))
                    {
                        options.Add(nodeMap[o]);
                    }
                }

                options = options.OrderBy(a => a.Id).ToList();

                // Increment time
                timer++;

                // Assign a free worker to each options, if possible
                foreach (var o in options)
                {
                    if (!o.IsCompleted && busyNodes.Count <= workerLimit && !busyNodes.Any(a => a.Id == o.Id))
                    {
                        busyNodes.Add(o);
                        o.TimeLeft = Char.Parse(o.Id) - 4;
                    }
                }

                var nodesToRemove = new List<Node>();
                foreach (var b in busyNodes)
                {
                    b.Decrement();
                    if (!b.IsBusy)
                    {
                        nodesToRemove.Add(b);
                        currentNode = b;
                        currentNode.IsCompleted = true;
                        previousOptions = options.Where(a => !a.IsCompleted).Select(c => c.Id).ToList();
                        order += currentNode.Id;
                    }
                }

                foreach (var nr in nodesToRemove)
                {
                    busyNodes.Remove(nr);
                }
            }

            return timer;
        }

        private class Node
        {
            public string Id { get; set; }
            public HashSet<string> Next { get; set; }
            public HashSet<string> Previous { get; set; }
            public bool IsCompleted { get; set; } = false;

            public int TimeLeft = 0;

            public bool IsBusy => TimeLeft > 0;

            public Node(string id)
            {
                this.Id = id;
                this.Next = new HashSet<string>();
                this.Previous = new HashSet<string>();
            }

            public override string ToString()
            {
                return $"{this.Id} - {this.IsCompleted}";
            }

            public void Decrement()
            {
                this.TimeLeft--;
            }

            public bool AreNextComplete(Dictionary<string, Node> nodeMap)
            {
                bool complete = true;
                foreach (var n in Next)
                {
                    complete &= nodeMap[n].IsCompleted;
                    complete &= nodeMap[n].AreNextComplete(nodeMap);
                }

                return complete;
            }

            public bool ArePreviousComplete(Dictionary<string, Node> nodeMap)
            {
                bool complete = true;
                foreach (var p in Previous)
                {
                    complete &= nodeMap[p].IsCompleted;
                }

                return complete;
            }

            public List<Node> ImmediateOptions(Dictionary<string, Node> nodeMap)
            {
                var options = new List<Node>();
                foreach (var n in Next)
                {
                    if (!nodeMap[n].IsCompleted && nodeMap[n].ArePreviousComplete(nodeMap))
                    {
                        options.Add(nodeMap[n]);
                    }
                    else
                    {
                        options.AddRange(nodeMap[n].ImmediateOptions(nodeMap));
                    }
                }
                return options;
            }
        }

        private class Tree
        {
            public List<string> Roots { get; set; }

            public Tree()
            {
                Roots = new List<string>();
            }

            public bool IsComplete(Dictionary<string, Node> nodeMap)
            {
                return nodeMap.All(a => a.Value.IsCompleted);
            }

            public void FindRoots(Dictionary<string, Node> nodeMap)
            {
                foreach (var n in nodeMap)
                {
                    if (n.Value.Previous.Count == 0)
                    {
                        Roots.Add(n.Key);
                    }
                }
            }
        }
    }
}
