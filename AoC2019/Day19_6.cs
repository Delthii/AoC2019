using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_6 : AbstractDay
    {
        private Node COM;
        private Node SAN;
        private Node YOU;

        public Day19_6() : base(6,2019)
        {
        }

        public override string PartA()
        {
            Init();
            return COM.Dfs(0).ToString();
        }
        
        public override string PartB()
        {
            Init();
            FindCommonNode()?.Dfs(0);
            return (SAN.Length + YOU.Length - 2).ToString();
        }

        private Node FindCommonNode()
        {
            var yourOrbits = new HashSet<Node>();
            foreach (var n in YOU.GetPath())
            {
                yourOrbits.Add(n);
            }

            return SAN.GetPath().FirstOrDefault(n => yourOrbits.Contains(n));
        }

        private void Init()
        {
            var t = File.ReadAllLines("..\\..\\..\\in.txt");
            var nodes = new Dictionary<string, Node>();

            foreach (var pairs in t)
            {
                var s = pairs.Split(')');
                if (!nodes.TryGetValue(s[0], out var n1))
                {
                    n1 = new Node();
                    nodes[s[0]] = n1;
                }

                if (!nodes.TryGetValue(s[1], out var n2))
                {
                    n2 = new Node();
                    nodes[s[1]] = n2;
                }

                n2.Prev = n1;
                n1.Nexts.Add(n2);

                if (s[0] == "COM") COM = n1;
                if (s[0] == "SAN") SAN = n1;
                if (s[0] == "YOU") YOU = n1;
                if (s[1] == "SAN") SAN = n2;
                if (s[1] == "YOU") YOU = n2;
            }
        }
    }

    class Node
    {
        public List<Node> Nexts = new List<Node>();
        public Node Prev { get; set; }
        public int Length { get; set; } = 0;

        public int Dfs(int currentLen)
        {
            Length = currentLen;
            int total = Nexts.Sum(n => n.Dfs(currentLen + 1));

            return total + (Length = currentLen);
        }

        public IEnumerable<Node> GetPath()
        {
            var n = Prev;
            while (n != null)
            {
                yield return n;
                n = n.Prev;
            }
        }
    }
}
