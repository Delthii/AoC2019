using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_3 : AbstractDay
    {
        public Day19_3() : base(2019, 3)
        {
        }
        bool[,] matrix = new bool[20000, 20000];

        public override string PartA()
        {
            var input = File.ReadAllLines("C:\\Users\\Peter\\source\\repos\\AoC2019\\AoC2019\\in.txt").Select(s => s.Split(',')).ToArray();
            int x = 10000;
            int y = 10000;

            var w1 = input[0];
            var w2 = input[1];

            int min = 100000000;

            //R999,D586,L462,D725,L236,U938,R366,D306,R263,
            DrawWire(w1);
            DrawWire(w2);

            return "";
        }

        private void DrawWire(string[] w1)
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
                        Col(matrix, y, y+length-1, x);
                        y += length;
                        break;
                    case 'D':
                        Col(matrix, y-length-1, y, x);
                        y -= length;
                        break;
                    case 'R':
                        Row(matrix, x, x+length-1, y);
                        x += length;
                        break;
                    case 'L':
                        Row(matrix, x-length-1, x, y);
                        y -= length;
                        break;
                }
            }
        }

        private void Row(bool[,] m, int start, int end, int y)
        {
            for (int x = start; x < end; x++)
            {
                if (m[x, y])
                {
                    NoteIntersection(x, y);
                }
                m[x, y] = true;
            }
        }

        private void Col(bool[,] m, int start, int end, int x)
        {
            for (int y = start; y < end; y++)
            {
                if (m[x, y])
                {
                    NoteIntersection(x, y);
                }
                m[x, y] = true;
            }
        }

        private int min = 1000000000;
        private int dx;
        private int dy;
        private int sx = 10000;
        private int sy = 10000;

        private void NoteIntersection(int x, int y)
        {
            var dist = Math.Abs(x - sx) + Math.Abs(y - sy);
            if (dist < min)
            {
                dx = x;
                dy = y;
                min = dist;
            }
        }

        public override string PartB()
        {
            return "";
        }
    }
}
