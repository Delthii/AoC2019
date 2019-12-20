using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_13 : AbstractDay
    {
        public Day19_13() : base(13, 2019)
        {
            
        }
        /*
            0 is an empty tile. No game object appears in this tile.
            1 is a wall tile. Walls are indestructible barriers.
            2 is a block tile. Blocks can be broken by the ball.
            3 is a horizontal paddle tile. The paddle is indestructible.
            4 is a ball tile. The ball moves diagonally and bounces off objects.
         */
        public override string PartA()
        {
            var program = File.ReadAllLines("..\\..\\in.txt").First().Split(',').Select(long.Parse).ToArray();
            var input = new List<long>();
            var output = new List<long>();
            program[0] = 2;
            var c = new SeqCompiler(input, output, program);
            var map = new char[60, 60];
            int scoreCnt = 0;
            int score = 0;

            var paddle = new Point();
            var ball = new Point();

            while (c.Step() != State.Done)
            {
                while (c.Step() == State.Running) ;
                for (int i = 0; i < output.Count; i += 3)
                {
                    var x = (int)output[i];
                    var y = (int)output[i + 1];
                    var type = (int)output[i + 2];
                    var p = new Point(x, y);

                    if (type == 4) ball = p;
                    if (type == 3) paddle = p;


                    if (x == -1 && y == 0)
                    {
                        scoreCnt++;
                        if (scoreCnt == 1)
                        {
                            Console.WriteLine(score);
                            score = type;
                            scoreCnt = 0;
                        }
                    }
                    else
                    {
                        InsertPointTile(p, type, map);
                    }
                }
                //Print(map);
                output.Clear();
                if (paddle.x < ball.x)
                    input.Add(1);
                else if(paddle.x > ball.x)
                    input.Add(-1);
                else
                    input.Add(0);
            }

            Print(map);




            return "";
        }

        private static void InsertPointTile(KeyValuePair<Point, int> tile, char[,] map)
        {
            if (tile.Value == 0) map[tile.Key.x, tile.Key.y] = ' ';
            if (tile.Value == 1) map[tile.Key.x, tile.Key.y] = '#';
            if (tile.Value == 2) map[tile.Key.x, tile.Key.y] = 'B';
            if (tile.Value == 3) map[tile.Key.x, tile.Key.y] = '_';
            if (tile.Value == 4) map[tile.Key.x, tile.Key.y] = 'O';
        }

        private static void InsertPointTile(Point p, int type, char[,] map)
        {
            if (type == 0) map[p.x, p.y] = ' ';
            if (type == 1) map[p.x, p.y] = '#';
            if (type == 2) map[p.x, p.y] = 'B';
            if (type == 3) map[p.x, p.y] = '_';
            if (type == 4) map[p.x, p.y] = 'O';
        }

        private static void Print(char[,] map)
        {
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 44; j++)
                {
                    Console.Write(map[j, i]);
                }

                Console.WriteLine();
            }
        }

        private static Dictionary<Point, int> A(List<long> output)
        {
            var map = new Dictionary<Point, int>();
            for (int i = 0; i < output.Count; i += 3)
            {
                var x = (int)output[i];
                var y = (int)output[i + 1];
                var type = (int)output[i + 2];

                var p = new Point(x,y);
                if(map.ContainsKey(p)) throw new Exception("Already seen this tile");
                map[p] = type;
            }

            return map;
        }

        public override string PartB()
        {
            throw new NotImplementedException();
        }
    }
}
