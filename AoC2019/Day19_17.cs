using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_17 : AbstractDay
    {
        public Day19_17() : base(17,2019)
        {
            
        }

        public override string PartA()
        {
            var program = File.ReadAllLines("..\\..\\in.txt").First().Split(',').Select(long.Parse).ToArray();
            var input = new List<long>();
            var output = new List<long>();
            var c = new SeqCompiler(input, output, program);
            c.Run();
            var o = new string(output.Select(ch => (char) ch).ToArray());
            int row = 0;
            var matrix = new List<List<char>>();
            matrix.Add(new List<char>());
            for (int i = 0; i < o.Length; i++)
            {
                if (o[i] == '\n')
                {
                    row++;
                    matrix.Add(new List<char>());
                    Console.WriteLine();
                }
                else
                {
                    matrix[row].Add(o[i]);
                    Console.Write(o[i]);
                }
            }
            Console.WriteLine();
            matrix.Remove(matrix.Last());
            matrix.Remove(matrix.Last());
            var sum = 0;
            for (int i = 1; i < matrix.Count - 1; i++)
            {
                for (int j = 1; j < matrix[0].Count - 1; j++)
                {
                    var N = 0;
                    if (matrix[i][j] == '#') N++;
                    if (matrix[i+1][j] == '#') N++;
                    if (matrix[i-1][j] == '#') N++;
                    if (matrix[i][j+1] == '#') N++;
                    if (matrix[i][j-1] == '#') N++;

                    if (N == 5) sum += i * j;
                }
            }
            Console.WriteLine();
            Console.WriteLine(sum);

            return "";
        }

        public override string PartB()
        {
            throw new NotImplementedException();
        }
    }
}
