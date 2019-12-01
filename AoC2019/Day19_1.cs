using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_1 : AbstractDay
    {
        public Day19_1() : base(1, 2019)
        {
        }

        public override string PartA()
        {
            return Lines.Select(int.Parse).Sum(CalcFuel).ToString();
        }

        private int CalcFuel(int weight)
        {
            return weight / 3 - 2;
        }

        public override string PartB()
        {
            return Lines.Select(int.Parse).Sum(RecCalcFuel).ToString();
        }

        private int RecCalcFuel(int weight)
        {
            if (weight < 6) return 0;
            var w = CalcFuel(weight);
            return w + RecCalcFuel(w);
        }
    }
}
