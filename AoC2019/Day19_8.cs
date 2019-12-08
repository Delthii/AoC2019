using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_8 : AbstractDay
    {
        private int width = 25;
        private int height = 6;

        public Day19_8() : base(8,2019)
        {
        }

        public override string PartA()
        {
            var digits = Lines.First().ToCharArray().Select(c => (int)(c - '0')).ToArray();
            var layers = new List<List<int>>();
            var area = width * height;
            List<int> currentLayer = null;
            int cnt = 0;
            foreach (var d in digits)
            {
                if (cnt % area == 0)
                {
                    currentLayer = new List<int>();
                    layers.Add(currentLayer);
                }
                currentLayer.Add(d);
                ++cnt;
            }

            var max = layers.OrderBy(l => l.Count(d => d == 0)).First();
            var ans = max.Count(d => d == 1) * max.Count(d => d == 2);
            return "" + ans;
        }

        public override string PartB()
        {
            var digits = Lines.First().ToCharArray().Select(c => (int)(c - '0')).ToArray();
            var layers = new List<List<int>>();
            var area = width * height;
            List<int> currentLayer = null;
            int cnt = 0;
            foreach (var d in digits)
            {
                if (cnt % area == 0)
                {
                    currentLayer = new List<int>();
                    layers.Add(currentLayer);
                }
                currentLayer.Add(d);
                ++cnt;
            }

            var msg = new List<int>();
            for(int i = 0; i< area; i++)msg.Add(2);
            foreach (var layer in layers)
            {
                for (int i = 0; i < layer.Count; i++)
                {
                    if (msg[i] == 2) msg[i] = layer[i];
                }
            }

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Console.Write(msg[width*j +i] == 0? " " : "|");
                }

                Console.WriteLine();
            }

            return "";
        }
    }
}
