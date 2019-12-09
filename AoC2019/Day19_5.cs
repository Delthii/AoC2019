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
            var input = Lines.First().Split(',').Select(long.Parse).ToArray();
            var inp = new List<long> {1};
            var outp = new List<long>();
            var c = new SeqCompiler(inp,outp, input);
            c.Run();
            return outp[outp.Count - 1] + "";
        }

        public override string PartB()
        {
            var input = Lines.First().Split(',').Select(long.Parse).ToArray();
            var inp = new List<long> { 5 };
            var outp = new List<long>();
            var c = new SeqCompiler(inp, outp, input);
            c.Run();
            return outp[outp.Count - 1] + "";
        }
    }
}
