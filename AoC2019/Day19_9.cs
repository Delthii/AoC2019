using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_9 : AbstractDay
    {

        public Day19_9() : base(9,2019)
        {
            
        }

        public override string PartA()
        {
            var program = Lines.First().Split(',').Select(long.Parse).ToArray();//Lines.First().Split(',').Select(long.Parse).ToArray();
            var inpA = new List<long> {1};
            var outA = new List<long>();
            var c = new SeqCompiler(inpA, outA, program);
            c.Run();

            return "";
        }

        public override string PartB()
        {
            throw new NotImplementedException();
        }
    }
}
