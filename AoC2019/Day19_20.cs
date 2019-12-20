using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_20 : AbstractDay
    {
        public Day19_20() : base(20,2019)
        {
            
        }

        public override string PartA()
        {
            var input = File.ReadAllLines("..\\..\\in.txt");
            var maze = new char[input[0].Length, input.Length];
            var g = new Dictionary<Point, List<Point>>();
            var dict = new Dictionary<string, Point>();

            for (int i = 0; i < input[0].Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    maze[i, j] = input[j][i];
                }
            }
            for (int i = 2; i < input[0].Length - 2; i++)
            {
                for (int j = 2; j < input.Length-2; j++)
                {
                    if (maze[i, j] == '.')
                    {
                        var point = new Point(i, j);
                        if(!g.ContainsKey(point))
                            g[point] = new List<Point>();
                        g[point].AddRange(GetN(maze, i, j));
                        if (IsPortalTile(maze, i, j, out var p))
                        {
                            if (dict.ContainsKey(p))
                            {
                                g[dict[p]].Add(point);
                                if (!g.ContainsKey(point))
                                    g[point] = new List<Point>();
                                g[point].Add(dict[p]);
                            }
                            else
                            {
                                dict[p] = new Point(i, j);
                            }
                        }
                    }
                }
            }

            var start = dict["AA"];
            var end = dict["ZZ"];
            int depth = 0;
            var Q = new Queue<Point>();
            var visited = new HashSet<Node20>();
            Q.Enqueue(start);
            var dist = new Dictionary<Point, int>();
            dist[start] = 0;
            while (Q.Count > 0)
            {
                var current = Q.Dequeue();
                visited.Add(new Node20{P=current, Depth = depth});
                foreach (var n in g[current])
                {
                    if (n.x == end.x && n.y == end.y && depth == 0)
                    {
                        Console.WriteLine(dist[n]);
                        return "";
                    }
                    var node = new Node20
                    {
                        P = n,
                        Depth = depth
                    };
                    if (Equals(n, end) || Equals(n, start) && depth > 0)
                    {
                        continue;
                    }
                    if (!Q.Contains(n) && !visited.Contains(node))
                    {
                        if (Inner.Contains(n))
                        {
                            depth++;
                        }

                        if (Outer.Contains(n))
                        {
                            if (depth == 0) continue;
                                depth--;
                        }
                        Q.Enqueue(n);
                        visited.Add(new Node20{P=n, Depth = depth});
                        dist[n] = dist[current] + 1;
                        if(dist[n]%100 == 0)
                            Console.WriteLine(dist[n]);
                    }

                }
            }

            return "";
        }

        private IEnumerable<Point> GetN(char[,] maze, int x, int y)
        {
            var rv = new List<Point>();
            if (maze[x + 1, y] == '.') rv.Add(new Point(x + 1, y));
            if (maze[x - 1, y] == '.') rv.Add(new Point(x - 1, y));
            if (maze[x, y + 1] == '.') rv.Add(new Point(x, y + 1));
            if (maze[x, y - 1] == '.') rv.Add(new Point(x, y - 1));

            return rv;
        }
        HashSet<Point> Outer = new HashSet<Point>();
        HashSet<Point> Inner = new HashSet<Point>();
        private bool IsPortalTile(char[,] maze, int x, int y, out string portal)
        {
            portal = "";
            Point p = new Point(x,y);
            if (IsCapital(maze[x + 1, y]) && IsCapital(maze[x + 2, y]))
            {
                portal = new string(new[]{ maze[x + 1, y] , maze[x + 2, y] });
                if (x < 40)
                    Inner.Add(p);
                else
                    Outer.Add(p);
                return true;
            }

            if (IsCapital(maze[x - 2, y]) && IsCapital(maze[x - 1, y]))
            {
                portal = new string(new[]{ maze[x - 2, y] , maze[x - 1, y] });
                if (x > 10)
                    Inner.Add(p);
                else
                    Outer.Add(p);
                return true;
            }

            if (IsCapital(maze[x, y + 1]) && IsCapital(maze[x, y + 2]))
            {
                portal = new string(new[]{ maze[x, y + 1] , maze[x, y + 2] });
                if (y < 120)
                    Inner.Add(p);
                else
                    Outer.Add(p);
                return true;
            }

            if (IsCapital(maze[x, y - 2]) && IsCapital(maze[x, y - 1]))
            {
                portal = new string(new[]{ maze[x, y - 2], maze[x, y - 1] });
                if (y > 10)
                    Inner.Add(p);
                else
                    Outer.Add(p);
                return true;
            }
            return false;
        }

        private bool IsCapital(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        public override string PartB()
        {
            var input = File.ReadAllLines("..\\..\\in.txt");
            var maze = new char[input[0].Length, input.Length];
            var g = new Dictionary<Point, List<Point>>();
            var dict = new Dictionary<string, Point>();

            for (int i = 0; i < input[0].Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    maze[i, j] = input[j][i];
                }
            }
            for (int i = 2; i < input[0].Length - 2; i++)
            {
                for (int j = 2; j < input.Length - 2; j++)
                {
                    if (maze[i, j] == '.')
                    {
                        var point = new Point(i, j);
                        if (!g.ContainsKey(point))
                            g[point] = new List<Point>();
                        g[point].AddRange(GetN(maze, i, j));
                        if (IsPortalTile(maze, i, j, out var p))
                        {
                            if (dict.ContainsKey(p))
                            {
                                g[dict[p]].Add(point);
                                if (!g.ContainsKey(point))
                                    g[point] = new List<Point>();
                                g[point].Add(dict[p]);
                            }
                            else
                            {
                                dict[p] = new Point(i, j);
                            }
                        }
                    }
                }
            }

            var start = dict["AA"];
            var end = dict["ZZ"];

            var Q = new Queue<Point>();
            var visited = new HashSet<Point>();
            Q.Enqueue(start);
            var dist = new Dictionary<Point, int>();
            dist[start] = 0;
            while (Q.Count > 0)
            {
                var current = Q.Dequeue();
                visited.Add(current);
                foreach (var n in g[current])
                {

                    if (!Q.Contains(n) && !visited.Contains(n))
                    {
                        Q.Enqueue(n);
                        dist[n] = dist[current] + 1;
                    }
                    if (n.x == end.x && n.y == end.y)
                    {
                        Console.WriteLine(dist[n]);
                        return "";
                    }
                }
            }

            return "";
        }
    }

    class Node20
    {
        public Point P;
        public int Depth;
    }
}
