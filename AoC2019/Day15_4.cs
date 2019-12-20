using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day15_4
    {
        public string HashIt(bool partA)
        {
            MD5 md5 = MD5.Create();
            var suffix = 0; const string input = "bgvyzdsv";
            while (true)
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input + ++suffix));
                if (hash[0] > 0 || hash[1] > 0 || hash[2] > (partA ? 15 : 0)) continue;
                Console.WriteLine(suffix);
                break;
            }

            return "";
        }
    }
}
