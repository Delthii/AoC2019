using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_16 : AbstractDay
    {
        public Day19_16() : base(16,2019)
        {
            
        }

        private int skip = 5973431;
        public override string PartA()
        {
            var input = File.ReadAllLines("..\\..\\in.txt").First().ToCharArray().Select(c => c - '0').ToArray();
            input = Repeat(input).SelectMany(e => e).ToArray();
            //Skip 5973431
            //input = "12345678".ToCharArray().Select(c => c - '0').ToArray();
            //0, 1, 0, -1   
            var pattern = new[] {0, 1, 0, -1};
            var next = new int[input.Length];
            for (int p = 0; p < 100; p++)
            {
                for (int i = 5973431; i < input.Length; i++)
                {
                    next[i] = Magic(input, pattern, i + 1);
                    if(p == 99 && i >= skip) Console.Write(next[i]);

                }

                input = next;
            }

            return "";
        }

        IEnumerable<int[]> Repeat(int[] input)
        {
            for (int i = 0; i < 10000; ++i)
            {
                yield return input;
            }
        }

        private int Magic(int[] input, int[] pattern, int step)
        {
            int rv = 0;
            var p = Pattern(pattern, step).Take(input.Length).ToArray();
            for (int i = skip; i < input.Length; ++i)
            {
                rv += input[i] * p[i];
            }

            return Math.Abs(rv) % 10;
        }

        private IEnumerable<int> Pattern(int[] pattern, int step)
        {
            bool skip = true;
            for (int i = 0; i < pattern.Length; i = (i + 1) % pattern.Length)
            {
                for (int s = 0; s < step; ++s)
                {
                    if (skip)
                    {
                        skip = false;
                        continue;
                    }

                    yield return pattern[i];
                }
                
            }
        }

        public override string PartB()
        {
            throw new NotImplementedException();
        }
    }
}
