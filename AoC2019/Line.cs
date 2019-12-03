using System;

namespace AoC2019
{
    public struct Line
    {
        public Point start;
        public Point end;

        public int Length()
        {
            return Horizontal() ? Math.Abs(end.x - start.x) : Math.Abs(end.y - start.y);
        }

        public bool Horizontal()
        {
            return start.y == end.y;
        }

        public int BiggestX()
        {
            return Math.Max(start.x, end.x);
        }

        public int BiggestY()
        {
            return Math.Max(start.y, end.y);
        }

        public int SmallestX()
        {
            return Math.Min(start.x, end.x);
        }

        public int SmallestY()
        {
            return Math.Min(start.y, end.y);
        }


        public Line(Point p1, Point p2)
        {
            start = p1;
            end = p2;
        }

        public bool TryIntersect(Line other, out Point result)
        {
            result = new Point(-1, -1);
            if (Horizontal() == other.Horizontal()) return false;
            if (Horizontal())
            {
                if (other.start.x > SmallestX() && other.start.x < BiggestX())
                {
                    if (other.SmallestY() < start.y && other.BiggestY() > start.y)
                    {
                        result = new Point(other.start.x, start.y);
                        return true;
                    }
                }

                return false;
            }

            return other.TryIntersect(this, out result);
        }
    }
}