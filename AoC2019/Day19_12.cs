using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_12 : AbstractDay
    {
        public Day19_12() : base(12, 2019) { }

        public override string PartA()
        {
            var moons = Parse();
            for (long t = 0; t < 1000; t++)
            {
                foreach (var m1 in moons)
                foreach (var m2 in moons)
                {
                    if (m1 == m2) continue;
                    m1.ApplyAllGravity(m2);
                }
                foreach (var m in moons) m.ApplyAllVelocity();
            }
            return moons.Sum(m => m.Energy()).ToString();
        }

        public override string PartB()
        {
            var moons = Parse();
            var X = GetTimestepForAxis(moons, 0);
            var Y = GetTimestepForAxis(moons, 1);
            var Z = GetTimestepForAxis(moons, 2);
            return LCM(LCM(X, Y), Z).ToString();
        }

        private static List<Moon> Parse()
        {
            var input = File.ReadAllLines("..\\..\\in.txt");
            var moons = new List<Moon>();
            foreach (var l in input)
            {
                var m = new Regex(@"<x=([\S]+), y=([\S]+), z=([\S]+)>").Match(l);
                moons.Add(new Moon(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value)));
            }

            return moons;
        }

        public long GetTimestepForAxis(List<Moon> moons, int axis)
        {
            var init = moons.Aggregate("", (current, m) => ConcatAxisState(current, m, axis));
            for (long t = 1; ; t++)
            {
                foreach (var m1 in moons)
                foreach (var m2 in moons)
                {
                    if (m1 == m2) continue;
                    m1.ApplyGravity(m2, axis);
                }
                foreach (var m in moons) m.ApplyVelocity(axis);
                var now = moons.Aggregate("", (current, m) => ConcatAxisState(current, m, axis));
                if (init == now) return t;
            }
        }

        private static string ConcatAxisState(string init, Moon m, int axis)
        {
            if (axis == 0)
            {
                init += m.x + " ";
                init += m.vx + " ";
                return init;
            }
            if (axis == 1)
            {
                init += m.y + " ";
                init += m.vy + " ";
                return init;
            }
            init += m.z + " ";
            init += m.vz + " ";
            return init;
        }

        private static long LCM(long a, long b)
        {
            return a * b / GCD(a, b);
        }

        private static long GCD(long a, long b)
        {
            if (a % b == 0) return b;
            return GCD(b, a % b);
        }
    }

    

    class Moon
    {
        public int x;
        public int y;
        public int z;
        public int vx;
        public int vy;
        public int vz;

        public Moon(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void ApplyGravity(Moon moon, int i)
        {
            if (i == 0)
                if (x < moon.x) vx++;
                else if (x > moon.x) vx--;
            if (i == 1)
                if (y < moon.y) vy++;
                else if (y > moon.y) vy--;
            if (i == 2)
                if (z < moon.z) vz++;
                else if (z > moon.z) vz--;
        }

        public void ApplyAllGravity(Moon moon)
        {
            if (x < moon.x) vx++;
            else if (x > moon.x) vx--;
            if (y < moon.y) vy++;
            else if (y > moon.y) vy--;
            if (z < moon.z) vz++;
            else if (z > moon.z) vz--;
        }

        public void ApplyVelocity(int axis)
        {
            if (axis == 0) x += vx;
            if (axis == 1) y += vy;
            if (axis == 2) z += vz;
        }
        public void ApplyAllVelocity()
        {
            x += vx;
            y += vy;
            z += vz;
        }

        public int Energy() => Pos() * Vel();
        public int Vel() => Math.Abs(vx) + Math.Abs(vy) + Math.Abs(vz);
        public int Pos() => Math.Abs(x) + Math.Abs(y) + Math.Abs(z);

        public override string ToString() => $"pos=<x={x}, y=  {x}, z= {x}>, vel=<x= {vx}, y= {vy}, z= {vz}> pos= {Pos()} vel= {Vel()}";
    }
}
