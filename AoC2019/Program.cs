using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2019
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var d = new Day1();
            Console.WriteLine(d.PartA());
            Console.WriteLine(d.PartB());
            Console.ReadLine();
        }
    }
}
