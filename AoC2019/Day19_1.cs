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
            return Lines.Select(int.Parse).Sum(d => d / 3 - 2).ToString();
        }

        public override string PartB()
        {
            var fuels = Lines.Select(int.Parse);
            var sum = 0;
            var dsum = 0;
            do
            {
                fuels = fuels.Select(d => d / 3 - 2 > 0 ? d / 3 - 2 : 0);
                dsum = fuels.Sum();
                sum += dsum;
            } while (dsum > 0);

            return sum + "";
        }
    }
}
