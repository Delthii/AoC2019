using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2019
{
    class Day19_3 : AbstractDay
    {
        public Day19_3() : base(3, 2019)
        {
        }

        private string[][] Input => Lines.Select(s => s.Split(',')).ToArray();

        public override string PartA()
        {
            Init(out var lines1, out var lines2);
            var intersections = IntersectLines(lines1, lines2);

            return intersections.Min(p => Math.Abs(p.x) + Math.Abs(p.y)).ToString();
        }

        public override string PartB()
        {
            Init(out var lines1, out var lines2);
            var combinedDistance = IntersectLinesB(lines1, lines2);

            return combinedDistance.Min().ToString();
        }

        private void Init(out List<Line> lines1, out List<Line> lines2)
        {
            var w1 = Input[0];
            var w2 = Input[1];
            lines1 = new List<Line>();
            lines2 = new List<Line>();
            DrawWire(w1, lines1);
            DrawWire(w2, lines2);
        }

        private static void DrawWire(string[] wireVectors, ICollection<Line> lines)
        {
            int x = 0, y = 0;
            foreach (var vector in wireVectors)
            {
                var dir = vector[0];
                var length = int.Parse(vector.Substring(1));
                switch (dir)
                {
                    case 'U':
                        lines.Add(new Line(new Point(x, y), new Point(x, y + length)));
                        y += length;
                        break;
                    case 'D':
                        lines.Add(new Line(new Point(x, y), new Point(x, y - length)));
                        y -= length;
                        break;
                    case 'R':
                        lines.Add(new Line(new Point(x, y), new Point(x + length, y)));
                        x += length;
                        break;
                    case 'L':
                        lines.Add(new Line(new Point(x, y), new Point(x - length, y)));
                        x -= length;
                        break;
                }
            }
        }

        private static IEnumerable<Point> IntersectLines(IEnumerable<Line> lines1, IEnumerable<Line> lines2)
        {
            var intersections = new List<Point>();

            foreach (var line1 in lines1)
            {
                foreach (var line2 in lines2)
                {
                    if (line1.TryIntersect(line2, out var point))
                    {
                        intersections.Add(point);
                    }
                }
            }

            return intersections;
        }

        private static IEnumerable<int> IntersectLinesB(IEnumerable<Line> lines1, IEnumerable<Line> lines2)
        {
            var intersections = new List<int>();
            var p1 = 0;

            foreach (var line1 in lines1)
            {
                var p2 = 0;
                foreach (var line2 in lines2)
                {
                    if (line1.TryIntersect(line2, out var point))
                    {
                        var cd = p1+p2 + new Line(line1.start, point).Length() + new Line(line2.start, point).Length();
                        intersections.Add(cd);
                    }
                    p2 += line2.Length();
                }
                p1 += line1.Length();
            }

            return intersections;
        }
    }
}
