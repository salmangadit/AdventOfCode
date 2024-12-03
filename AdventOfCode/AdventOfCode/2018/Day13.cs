namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Common;

    public class Day13 : BaseDay<(int, int), (int, int)>
    {
        public Day13() : base(2018, 13) { }

        public override (int, int) Part1()
        {
            var maxWidth = inputs.Max(a => a.Length);

            GridElement[,] grid = new GridElement[maxWidth, inputs.Length];
            int cartCounter = 0;

            List<Cart> carts = new List<Cart>();

            // Populate
            for (var i = 0; i < inputs.Length; i++)
            {
                for (var j = 0; j < inputs[i].Length; j++)
                {
                    grid[j, i] = new GridElement(j, i, inputs[i][j], ref cartCounter);

                    if (Cart.Orientations.Any(a => a == inputs[i][j]))
                    {
                        // This is a cart.
                        var newCart = new Cart(grid[j, i], ++cartCounter, inputs[i][j]);
                        carts.Add(newCart);
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(newCart.Orientation);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(grid[j, i]);
                    }
                }
                Console.WriteLine();
            }

            (int, int) location = (0, 0);
            while (!CheckCollisions(carts, out location))
            {
                bool hasCollision = false;
                // Move carts
                foreach (var c in carts)
                {
                    MoveCart(grid, c);
                    if (CheckCollisions(carts, out location))
                    {
                        hasCollision = true;
                        break;
                    }
                }

                if (hasCollision)
                {
                    break;
                }
            }

            // Draw again
            for (var i = 0; i < inputs.Length; i++)
            {
                for (var j = 0; j < inputs[i].Length; j++)
                {
                    var cart = carts.FirstOrDefault(a => a.Location.X == j && a.Location.Y == i);
                    if (cart != null)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(cart.Orientation);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(grid[j, i]);
                    }
                }
                Console.WriteLine();
            }

            return location;
        }

        public override (int, int) Part2()
        {
            var maxWidth = inputs.Max(a => a.Length);

            GridElement[,] grid = new GridElement[maxWidth, inputs.Length];
            int cartCounter = 0;

            List<Cart> carts = new List<Cart>();

            // Populate
            for (var i = 0; i < inputs.Length; i++)
            {
                for (var j = 0; j < inputs[i].Length; j++)
                {
                    grid[j, i] = new GridElement(j, i, inputs[i][j], ref cartCounter);

                    if (Cart.Orientations.Any(a => a == inputs[i][j]))
                    {
                        // This is a cart.
                        var newCart = new Cart(grid[j, i], ++cartCounter, inputs[i][j]);
                        carts.Add(newCart);
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(newCart.Orientation);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(grid[j, i]);
                    }
                }
                Console.WriteLine();
            }

            (int, int) location = (0, 0);
            while (carts.Count(a => a.Orientation != 'X') > 1)
            {
                bool hasCollision = false;
                // Move carts
                foreach (var c in carts.OrderBy(a => a.Location.X).ThenBy(b => b.Location.Y))
                {
                    MoveCart(grid, c);
                    if (CheckCollisions(carts, out location))
                    {
                        hasCollision = true;
                    }
                }

                if (hasCollision && carts.Count(a => a.Orientation != 'X') == 1)
                {
                    break;
                }
            }

            // Draw again
            for (var i = 0; i < inputs.Length; i++)
            {
                for (var j = 0; j < inputs[i].Length; j++)
                {
                    var cart = carts.FirstOrDefault(a => a.Location.X == j && a.Location.Y == i);
                    if (cart != null)
                    {
                        if (cart.Orientation != 'X')
                        {

                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {

                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write(cart.Orientation);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(grid[j, i]);
                    }
                }
                Console.WriteLine();
            }

            var lastCart = carts.First(a => a.Orientation != 'X');
            return (lastCart.Location.X, lastCart.Location.Y);
        }

        private bool CheckCollisions(List<Cart> carts, out (int, int) location)
        {
            for (var i = 0; i < carts.Count; i++)
            {
                for (var j = i + 1; j < carts.Count; j++)
                {
                    if (carts[i].Orientation == 'X' || carts[j].Orientation == 'X')
                    {
                        continue;
                    }

                    if (carts[i].Location.X == carts[j].Location.X && carts[i].Location.Y == carts[j].Location.Y)
                    {
                        // Disable both carts
                        carts[i].Orientation = 'X';
                        carts[j].Orientation = 'X';
                        location = (carts[i].Location.X, carts[i].Location.Y);
                        return true;
                    }
                }
            }

            location = (0, 0);
            return false;
        }

        private void MoveCart(GridElement[,] grid, Cart cart)
        {
            if (cart.Orientation == 'X')
            {
                return;
            }

            var newX = cart.Location.X;
            var newY = cart.Location.Y;
            switch (cart.Orientation)
            {
                case '>':
                    newX = cart.Location.X + 1;
                    break;
                case '<':
                    newX = cart.Location.X - 1;
                    break;
                case 'v':
                    newY = cart.Location.Y + 1;
                    break;
                case '^':
                    newY = cart.Location.Y - 1;
                    break;
            }

            cart.Location = grid[newX, newY];
            cart.Orientation = ReorientCart(cart, grid, newX, newY);
        }

        private char ReorientCart(Cart currCart, GridElement[,] grid, int x, int y)
        {
            if (grid[x, y].Track == '\\')
            {
                switch (currCart.Orientation)
                {
                    case '>':
                        return 'v';
                    case '<':
                        return '^';
                    case '^':
                        return '<';
                    case 'v':
                        return '>';
                }
            }

            if (grid[x, y].Track == '/')
            {
                switch (currCart.Orientation)
                {
                    case '>':
                        return '^';
                    case '<':
                        return 'v';
                    case '^':
                        return '>';
                    case 'v':
                        return '<';
                }
            }

            // Deal with intersections?
            if (grid[x, y].Track == '+')
            {
                currCart.IntersectionCount++;
                switch (currCart.Orientation)
                {
                    case '>':
                        if (currCart.IntersectionCount % 3 == 0)
                        {
                            return '^';
                        }
                        else if (currCart.IntersectionCount % 3 == 2)
                        {
                            return 'v';
                        }
                        else
                        {
                            return currCart.Orientation;
                        }
                    case '<':
                        if (currCart.IntersectionCount % 3 == 0)
                        {
                            return 'v';
                        }
                        else if (currCart.IntersectionCount % 3 == 2)
                        {
                            return '^';
                        }
                        else
                        {
                            return currCart.Orientation;
                        }
                    case '^':
                        if (currCart.IntersectionCount % 3 == 0)
                        {
                            return '<';
                        }
                        else if (currCart.IntersectionCount % 3 == 2)
                        {
                            return '>';
                        }
                        else
                        {
                            return currCart.Orientation;
                        }
                    case 'v':
                        if (currCart.IntersectionCount % 3 == 0)
                        {
                            return '>';
                        }
                        else if (currCart.IntersectionCount % 3 == 2)
                        {
                            return '<';
                        }
                        else
                        {
                            return currCart.Orientation;
                        }
                }
            }

            return currCart.Orientation;
        }

        private class GridElement
        {
            public int X;
            public int Y;
            public char Track;

            public GridElement(int x, int y, char track, ref int cartCounter)
            {
                X = x;
                Y = y;
                if (Cart.Orientations.Any(a => a == track))
                {
                    // Set correct underlying orientation
                    switch (track)
                    {
                        case '>':
                        case '<':
                            Track = '-';
                            break;
                        case '^':
                        case 'v':
                            Track = '|';
                            break;
                    }
                }
                else
                {
                    this.Track = track;
                }

            }

            public override string ToString()
            {
                return Track.ToString();
            }
        }

        private class Cart
        {
            public static char[] Orientations = { '>', '<', 'v', '^' };
            public int Id;
            public char Orientation;
            public GridElement Location;

            public int IntersectionCount = -1;

            public Cart(GridElement location, int id, char orient)
            {
                this.Location = location;
                this.Id = id;
                this.Orientation = orient;
            }

            public override string ToString()
            {
                return Orientation.ToString();
            }
        }
    }
}
