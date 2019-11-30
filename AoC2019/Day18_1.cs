using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    internal class Day18_1 : AbstractDay
    {

        public Day18_1() : base(1, 2018)
        {
        }

        public override string PartA()
        {
            return Lines.Select(int.Parse).Sum().ToString();
        }

        public override string PartB()
        {
            var frequencies = Lines.Select(int.Parse).ToArray();
            var sum = 0;
            var hashset = new HashSet<int> {sum};
            while (true)
            {
                foreach (var frequency in frequencies)
                {
                    sum += frequency;
                    if (!hashset.Add(sum)) return sum.ToString();
                }
            }
        }
    }
}
