using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_5 : AbstractDay
    {
        public Day19_5() : base(5,2019)
        {
            
        }

        public override string PartA()
        {
            var input = Lines.First().Split(',').Select(int.Parse).ToArray();
            var c = new Compiler();
            c.Input.Add(1);
            c.Run(input);
            return "";
        }

        public override string PartB()
        {
            var input = Lines.First().Split(',').Select(int.Parse).ToArray();
            var c = new Compiler();
            c.Input.Add(5);
            c.Run(input);
            return "";
        }
    }
}
