using System;

namespace AoC2019
{
    public struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int ManhattanDistance(Point other)
        {
            return Math.Abs(x - other.x) + Math.Abs(y - other.y);
        }

        public override string ToString()
        {
            return x + " " + y;
        }
    }
}