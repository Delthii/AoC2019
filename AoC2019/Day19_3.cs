using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    struct Line
    {
        public Line(Point p1, Point p2)
        {
            start = p1;
            end = p2;
        }
        public Point start;
        public Point end;
    }

    class Day19_3 : AbstractDay
    {
        public Day19_3() : base(2019, 3)
        {
        }


        public override string PartB()
        {
            var input = File.ReadAllLines("C:\\Users\\pma\\source\\repos\\AoC2019\\AoC2019\\in.txt").Select(s => s.Split(',')).ToArray();
            var w1 = input[0];
            var w2 = input[1];
            var lines1 = new List<Line>();
            var lines2 = new List<Line>();
            int x = sx;
            int y = sy;
            BRun(w1, lines1, x, y);
            BRun(w2, lines2, x, y);

            return "";
        }

        private static void BRun(string[] w, List<Line> lines, int x, int y)
        {
            for (int i = 0; i < w.Length; i++)
            {
                var dir = w[i][0];
                var length = int.Parse(w[i].Substring(1));
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

        bool[,] matrix = new bool[40000, 40000];
        private int min = 1000000000;
        private int dx;
        private int dy;
        private int sx = 20000;
        private int sy = 20000;

        public override string PartA()
        {
            var input = File.ReadAllLines("C:\\Users\\pma\\source\\repos\\AoC2019\\AoC2019\\in.txt").Select(s => s.Split(',')).ToArray();

            var w1 = input[0];
            var w2 = input[1];

            DrawWire(w1,(char)1);
            Console.WriteLine();
            DrawWire(w2, (char)2);

            return "";
        }

        private void DrawWire(string[] w1, char wire)
        {
            int x = sx;
            int y = sy;
            for (int i = 0; i < w1.Length; i++)
            {
                var dir = w1[i][0];
                var length = int.Parse(w1[i].Substring(1));
                switch (dir)
                {
                    case 'U':
                        Col(matrix, y, y+length, x, wire);
                        y += length;
                        break;
                    case 'D':
                        Col(matrix, y-length, y, x, wire);
                        y -= length;
                        break;
                    case 'R':
                        Row(matrix, x, x+length, y, wire);
                        x += length;
                        break;
                    case 'L':
                        Row(matrix, x-length, x, y, wire);
                        x -= length;
                        break;
                }
                Console.WriteLine(x + " " + y);
            }
        }

        private void Row(bool[,] m, int start, int end, int y, char wire)
        {
            for (int x = start; x < end; x++)
            {
                if (m[x, y] && wire == 2)
                {
                    NoteIntersection(x, y);
                }
                m[x, y] = wire == 1;

            }
        }

        private void Col(bool[,] m, int start, int end, int x, char wire)
        {
            for (int y = start; y < end; y++)
            {
                if (m[x, y] && wire == 2)
                {
                    NoteIntersection(x, y);
                }
                m[x, y] = wire == 1;
            }
        }

        

        private void NoteIntersection(int x, int y)
        {
            var dist = Math.Abs(x - sx) + Math.Abs(y - sy);
            if (dist < min && dist != 0)
            {
                dx = x;
                dy = y;
                min = dist;
            }
        }

        
    }
}
