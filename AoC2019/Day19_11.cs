using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    enum Direction
    {
        Up=0,Left=1, Down=2,Right=3
    }

    class Day19_11 : AbstractDay
    {
        public Day19_11() : base(11,2019)
        {
            
        }



        public override string PartA()
        {
            var program = Lines.First().Split(',').Select(long.Parse).ToArray();
            var input = new List<long>();
            var output = new List<long>();
            var c = new SeqCompiler(input,output,program);
            var points = new Dictionary<Point, long>();
            var position = new Point(0,0);
            var direction = Direction.Up;
            points[position] = 1;
            while (c.GetState() != State.Done)
            {
                var color = GetColorAtPosition(points, position);
                input.Add(color);
                Run(c);
                var newColor = output.First();
                output.RemoveAt(0);
                var dir = output.First();
                output.RemoveAt(0);
                points[position] = newColor;

                if(dir == 1) TurnRight(ref direction);
                else TurnLeft(ref direction);

                switch (direction)
                {
                    case Direction.Down:
                        position.y++;
                        break;
                    case Direction.Up:
                        position.y--;
                        break;
                    case Direction.Left:
                        position.x--;
                        break;
                    case Direction.Right:
                        position.x++;
                        break;
                }
            }

            for (int i = points.Keys.Select(p => p.y).Min(); i <= points.Keys.Select(p => p.y).Max(); i++)
            {
                for (int j = points.Keys.Select(p => p.x).Min(); j <= points.Keys.Select(p => p.x).Max(); j++)
                {
                    if (points.ContainsKey(new Point(j, i)))
                    {
                        Console.Write(points[new Point(j, i)] == 1L ? '|' : ' ');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
            
            
            return "";
        }

        private static long GetColorAtPosition(Dictionary<Point, long> points, Point position)
        {
            if (!points.ContainsKey(position))
            {
                return 0;
            }

            return points[position];

        }

        private void Run(SeqCompiler c)
        {
            while(c.Step() == State.Running) { }
        }

        private void TurnLeft(ref Direction d)
        {
            var nd = (((int) d + 1) % 4);
            d = (Direction)nd;
        }

        private void TurnRight(ref Direction d)
        {
            var nd = ((int)d - 1);
            if (nd < 0) nd = 3;
            d = (Direction)nd;
        }

        public override string PartB()
        {
            throw new NotImplementedException();
        }
    }
}
