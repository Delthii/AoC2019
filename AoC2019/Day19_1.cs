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

        public override string PartA() => Lines.Select(int.Parse).Sum(CalcFuel).ToString();
        public override string PartB() => Lines.Select(int.Parse).Sum(RecCalcFuel).ToString();

        private int CalcFuel(int weight) => weight / 3 - 2;

        private int RecCalcFuel(int weight)
        {
            switch (CalcFuel(weight))
            {
                case int w when w < 0:
                    return 0;
                case int w:
                    return w + RecCalcFuel(w);
            }
        }
    }
}
