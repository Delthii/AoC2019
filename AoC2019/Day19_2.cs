using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_2 : AbstractDay
    {
        private readonly Compiler compiler;

        public Day19_2() : base(2, 2019)
        {
            compiler = new Compiler();
        }

        public override string PartA()
        {
            var program = Lines.First().Split(',').Select(int.Parse).ToArray();
            program[1] = 12;
            program[2] = 2;
            compiler.Run(program);
            return program.First().ToString();
        }

        public override string PartB()
        {
            var init = Lines.First().Split(',').Select(int.Parse).ToArray();
            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    var program = init.ToArray();
                    program[1] = i;
                    program[2] = j;
                    compiler.Run(program);
                    if (program[0] == 19690720)
                    {
                        return i * 100 + j + "";
                    }
                }
            }

            throw new Exception("No answer");
        }
    }
}
