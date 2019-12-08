using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_8 : AbstractDay
    {
        private const int Width = 25;
        private const int Height = 6;
        private static int Area => Width * Height;
        public Day19_8() : base(8,2019)
        {
        }

        public override string PartA()
        {
            var layers = GetLayers();

            var max = layers.OrderBy(l => l.Count(d => d == 0)).First();
            var ans = max.Count(d => d == 1) * max.Count(d => d == 2);
            return ans.ToString();
        }

        public override string PartB()
        {
            var msg = GetEmptyMessage();

            Decrypt(GetLayers(), msg);
            PrintImage(msg);

            return "";
        }

        private List<List<int>> GetLayers()
        {
            var digits = Lines.First().ToCharArray().Select(c => c - '0').ToArray();
            var layers = new List<List<int>>();
            List<int> currentLayer = null;
            int cnt = 0;
            foreach (var d in digits)
            {
                if (cnt % Area == 0)
                {
                    currentLayer = new List<int>();
                    layers.Add(currentLayer);
                }
                currentLayer.Add(d);
                ++cnt;
            }

            return layers;
        }

        private static void Decrypt(List<List<int>> layers, List<int> msg)
        {
            foreach (var layer in layers)
            {
                for (int i = 0; i < layer.Count; i++)
                {
                    if (msg[i] == 2) msg[i] = layer[i];
                }
            }
        }

        private static void PrintImage(List<int> msg)
        {
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    Console.Write(msg[Width * j + i] == 0 ? " " : "|");
                }
                Console.WriteLine();
            }
        }

        private static List<int> GetEmptyMessage()
        {
            var msg = new List<int>();
            for (var i = 0; i < Area; i++) msg.Add(2);
            return msg;
        }
    }
}
