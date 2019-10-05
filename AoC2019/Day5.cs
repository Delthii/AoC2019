using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    internal class Day5 : AbstractDay
    {
        public Day5() : base(5, 2018)
        {
        }

        public override string PartA()
        {
            var polymers = Lines.Single().ToCharArray().ToList();

            return FullyReact(polymers).ToString();
        }

        public override string PartB()
        {
            return Enumerable.Range('a', 'z' - 'a' + 1)
                .Select(c => (char) c)
                .Select(letter => FullyReact(
                    Lines
                        .Single()
                        .ToCharArray()
                        .Where(c => c != letter && c != char.ToUpper(letter))
                        .ToList()))
                .Min()
                .ToString();
        }

        private static int FullyReact(List<char> polymers)
        {
            var boom = true;
            while (boom)
            {
                boom = false;
                for (var i = 0; i < polymers.Count - 1; i++)
                {
                    var first = i;
                    var second = i + 1;
                    while (first >= 0 && second < polymers.Count && React(polymers[first], polymers[second]))
                    {
                        first--;
                        second++;
                        boom = true;
                    }

                    if (boom)
                    {
                        i = first;
                        polymers.RemoveRange(++first, second - first);
                        boom = false;
                    }
                }
            }

            return polymers.Count;
        }

        private static bool React(char c1, char c2)
        {
            const int delta = 'a' - 'A';
            return Math.Abs(c1 - c2) == delta;
        }

       
    }
}
