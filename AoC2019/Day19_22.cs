using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_22 : AbstractDay
    {
        public Day19_22() : base(22,2019)
        {
        }

        private static void Print(int nc, int[] deck)
        {
            for (int i = 0; i < nc; i++)
            {
                Console.Write(deck[i] + " ");
                if (deck[i] == 2019) Console.WriteLine(i);
            }
            Console.WriteLine();
        }

        public override string PartA()
        {
            var lines = File.ReadAllLines("..\\..\\in.txt");
            var nc = 10007;
            var deck = Enumerable.Range(0, nc).ToArray();
            var dir = 1;
            var index = 0;
            foreach (var line in lines)
            {
                if (line.Contains("deal into new stack"))
                {
                    deck = deck.Reverse().ToArray();
                }
                else if (line.Contains("cut "))
                {
                    var N = int.Parse(line.Substring(4));
                    var card = N;
                    if (card < 0) card += nc;
                    var newDeck = new int[nc];
                    for (int i = 0; i < nc; i++, card = (card + 1) % nc)
                    {
                        newDeck[i] = deck[card];
                    }

                    deck = newDeck;
                }
                else if (line.Contains("deal with increment "))
                {
                    var N = int.Parse(line.Substring("deal with increment ".Length));
                    var card = 0;
                    var newDeck = new int[nc];
                    for (int i = 0; i < nc; i++, card = (card + N) % nc)
                    {
                        newDeck[card] = deck[i];
                    }

                    deck = newDeck;
                }
            }

            for (int i = 0; i < nc; i++)
            {
                if (deck[i] == 2019) return i + "";
            }

            return "";
        }

        public override string PartB()
        {
            var lines = File.ReadAllLines("..\\..\\in.txt");
            var nc = 10;
            var index = 0;
            var offset = 1;
            var multiplier = 1;
            foreach (var line in lines)
            {
                if (line.Contains("deal into new stack"))
                {
                    offset += (nc - 2);
                    offset %= nc;


                    int i = index;
                    for (int cnt = 0, inc = 0; cnt < (nc-1) % nc; cnt++)
                    {
                        inc += multiplier;
                        i = (index + offset * (multiplier * (cnt + 1))) % nc;
                    }

                    index = i;



                }
                else if (line.Contains("cut "))
                {
                    var N = int.Parse(line.Substring(4));
                    int i = index;
                    if (N < 0)
                    {
                        for (int cnt = 0, inc = 0; cnt < (N + nc) % nc; cnt++)
                        {
                            inc += multiplier;
                            i = (index + offset * (multiplier * (cnt + 1))) % nc;
                        }
                    }
                    else
                    {
                        for (int cnt = 0, inc = 0; cnt < N % nc; cnt++)
                        {
                            inc += multiplier;
                            i = (index + offset * (multiplier * (cnt + 1))) % nc;
                        }
                    }
                    

                    index = i;

                }
                else if (line.Contains("deal with increment "))
                {
                    var N = int.Parse(line.Substring("deal with increment ".Length));
                    multiplier *= N;
                    multiplier %= nc;
                }
                for (int i = index, cnt = 0, inc = 0; cnt < nc; cnt++)
                {
                    Console.Write(i);
                    Console.Write(" ");
                    inc += multiplier;
                    i = (index + offset * (multiplier * (cnt + 1))) % nc;
                }
                Console.WriteLine();
            }


            return "";
        }
    }
}
