using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day2019_5 : AbstractDay
    {
        public override string PartA()
        {
            var input = File.ReadAllLines("..\\..\\in.txt").First().Split(',').Select(int.Parse).ToArray();
            var c = new Compiler();
            c.Run(input);
            return "";
        }

        public override string PartB()
        {
            throw new NotImplementedException();
        }
    }
}
