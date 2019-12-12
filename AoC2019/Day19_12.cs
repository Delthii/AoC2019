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
        public Day19_12() : base (12,2019)
        {
            
        }

        public override string PartA()
        {
            /*  <x=-7, y=-1, z=6>
                <x=6, y=-9, z=-9>
                <x=-12, y=2, z=-7>
                <x=4, y=-17, z=-12> */
            var input = File.ReadAllLines("..\\..\\in.txt");
            var moons = new List<Moon>();
            foreach (var l in Lines)
            {
                var r = new Regex(@"<x=([\S]+), y=([\S]+), z=([\S]+)>");
                var m = r.Match(l);
                var x = int.Parse(m.Groups[1].Value);
                var y = int.Parse(m.Groups[2].Value);
                var z = int.Parse(m.Groups[3].Value);
                moons.Add(new Moon(x,y,z));
            }

            var init = "";
            foreach (var moon in moons) init += moon.ToString();

            for (long t = 0; ; t++)
            {
                foreach (var m1 in moons)
                {
                    foreach (var m2 in moons)
                    {
                        if (m1 == m2) continue;
                        m1.ApplyGravity(m2);
                    }
                }

                var now = "";
                foreach (var m in moons)
                {
                    m.ApplyVelocity();
                    now += m.ToString();
                }
                if(t % 1000000 == 0) Console.WriteLine(t);
                if (now == init)
                {
                    Console.WriteLine(t+1);
                    break;
                }
            }

            var e = moons.Sum(m => m.Energy());

            return "";
        }

        public override string PartB()
        {
            throw new NotImplementedException();
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

        public void ApplyGravity(Moon m2)
        {
            if (x < m2.x) vx++;
            else if (x > m2.x) vx--;
            if (y < m2.y) vy++;
            else if (y > m2.y) vy--;
            if (z < m2.z) vz++;
            else if (z > m2.z) vz--;
        }

        public void ApplyVelocity()
        {
            x += vx;
            y += vy;
            z += vz;
        }

        public int Energy()
        {
            var pos = Pos();
            var vel = Vel();
            return pos * vel;
        }

        public int Vel()
        {
            return Math.Abs(vx) + Math.Abs(vy) + Math.Abs(vz);
        }

        public int Pos()
        {
            return Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
        }

        public override string ToString()
        {
            //return $"pos=<x={x}, y=  {x}, z= {x}>, vel=<x= {vx}, y= {vy}, z= {vz}>    pos= {Pos()}    vel= {Vel()}";
            return $"{x}{y}{z}{Pos()}{Vel()}";
        }
    }
}
