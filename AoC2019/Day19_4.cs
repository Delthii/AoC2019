using System;
using System.Collections.Generic;
using System.IO;

namespace AoC2019
{
    class Day19_4 : AbstractDay
    {
        public Day19_4() : base(4, 2019)
        {
        }

        public override string PartA() => Core(AdjA);

        public override string PartB() => Core(AdjB);

        private static string Core(Func<char[], int, bool, bool> adjFunc)
        {
            var pws = 0;
            for (var i = 402328; i < 864247; i++)
            {
                var pw = i.ToString().ToCharArray();
                var adj = false;
                var inc = true;

                for (int c = 0; c < pw.Length - 1; c++)
                {
                    adj = adjFunc(pw, c, adj);
                    if (pw[c + 1] < pw[c])
                    {
                        inc = false;
                        break;
                    }
                }

                if (adj && inc) pws++;
            }

            return pws.ToString();
        }

        private bool AdjA(char[] pw, int c, bool adj)
        {
            if (pw[c] == pw[c + 1]) adj = true;

            return adj;
        }

        private bool AdjB(char[] pw, int c, bool adj)
        {
            if (pw[c] == pw[c + 1])
            {
                if (c > 0)
                {
                    if (pw[c - 1] == pw[c]) return adj;
                }
                if (c < pw.Length - 2)
                {
                    if (pw[c + 2] == pw[c]) return adj;
                }

                return true;
            }

            return adj;
        }

    }
}
