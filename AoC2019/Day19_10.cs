using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_10 : AbstractDay
    {
        public Day19_10() : base(10, 2019)
        {
            
        }
        public override string PartA()
        {
            var input = Lines.ToArray();//File.ReadAllLines("..\\..\\in.txt");
            Asteroids(input, out var maxPoint, out var max);
            return max + "";
        }

        public override string PartB()
        {
            var input = Lines.ToArray();
            var asteroids = Asteroids(input, out var maxPoint, out var max);
            var dict = SetupPartB(asteroids, maxPoint, out var derivatives);

            var ordered = derivatives.OrderBy(d => d).ToArray();

            for (int cnt = 0; cnt < 200;)
            {
                foreach (var d in ordered)
                {
                    if (dict[d].Count > 0)
                    {
                        dict[d] = dict[d].OrderBy(e => e.ManhattanDistance(maxPoint)).ToList();
                        var killed = dict[d][0];
                        dict[d].RemoveAt(0);
                        cnt++;
                        if (cnt == 200)
                        {
                            killed.x *= 100;
                            return (killed.x + killed.y) + "";
                        }
                    }
                }
            }

            return "";
        }

        private Dictionary<decimal, List<Point>> SetupPartB(List<Point> asteroids, Point maxPoint, out HashSet<decimal> derivatives)
        {
            var dict = new Dictionary<decimal, List<Point>>();
            derivatives = new HashSet<decimal>();

            foreach (var a in asteroids)
            {
                if (Equals(a, maxPoint)) continue;
                var d = Derivative(maxPoint, a);
                if (!dict.ContainsKey(d))
                {
                    dict[d] = new List<Point>();
                }

                dict[d].Add(a);
                derivatives.Add(d);
            }

            return dict;
        }

        private List<Point> Asteroids(string[] input, out Point maxPoint, out int max)
        {
            var astroids = InitAsteroids(input);

            maxPoint = new Point(-1, -1);
            max = 0;
            foreach (var a1 in astroids)
            {
                var set = new HashSet<decimal>();
                foreach (var a2 in astroids)
                {
                    if (Equals(a1, a2)) continue;
                    set.Add(Derivative(a1, a2));
                }

                if (set.Count > max)
                {
                    max = set.Count;
                    maxPoint = a1;
                }
            }

            return astroids;
        }

        private static List<Point> InitAsteroids(string[] input)
        {
            var asteroids = new List<Point>();
            for (int i = 0; i < input.Length; i++)
            {
                var line = input[i];
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == '#') asteroids.Add(new Point(j, i));
                }
            }

            return asteroids;
        }

        private decimal Derivative(Point p1, Point p2)
        {
            decimal d;
            decimal dx = Math.Abs(p2.x - p1.x);
            decimal dy = Math.Abs(p2.y - p1.y);
            if (p2.y <= p1.y)
            {
                if (p2.x >= p1.x) d = dy != 0 ? dx / dy : 999;
                else d = dx != 0 ? dy / dx + 100000000 : 999999999;
            }
            else
            {
                if (p2.x >= p1.x) d = dx != 0 ? dy / dx + 10000 : 99999;
                else d = dy != 0 ? (dx / dy) * 1000000 : 9999999;
            }
            return d;
        }

        
    }
}
