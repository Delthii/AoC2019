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
        public Day19_6() : base(6,2019)
        {
            
        }
        public override string PartA()
        {
            var t = Lines.ToArray();
            var nodes = new Dictionary<string, Node>();
            Node COM = null;
            Node SAN = null;
            Node YOU = null;
            foreach (var pairs in t)
            {
                var s = pairs.Split(')');
                Node n1, n2;
                if (!nodes.TryGetValue(s[0], out n1))
                {
                    n1 = new Node();
                    nodes[s[0]] = n1;
                }
                if (!nodes.TryGetValue(s[1], out n2))
                {
                    n2 = new Node();
                    nodes[s[1]] = n2;
                }

                n2.prev = n1;
                n1.Nexts.Add(n2);

                if (s[0] == "COM") COM = n1;
                if (s[0] == "SAN") SAN = n1;
                if (s[0] == "YOU") YOU = n1;
                if (s[1] == "SAN") SAN = n2;
                if (s[1] == "YOU") YOU = n2;
            }

            var len = COM.Dfs(0);

            Node common = null;

            HashSet<Node> visited = new HashSet<Node>();
            foreach (var n in YOU.GetPath())
            {
                visited.Add(n);
            }
            foreach (var n in SAN.GetPath())
            {
                if (visited.Contains(n))
                {
                    common = n;
                    break;
                }
            }

            len = common.Dfs(0);

            var sl = SAN.length;
            var yl = YOU.length;

            var ans = sl + yl - 2;








            return "";
        }

        public override string PartB()
        {
            throw new NotImplementedException();
        }
    }

    class Node
    {
        public List<Node> Nexts = new List<Node>();
        public Node prev;
        public int length = 0;

        public int Dfs(int currentLen)
        {
            length = currentLen;
            int total = 0;
            foreach (var n in Nexts)
            {
                total += n.Dfs(currentLen + 1);
            }

            return total + length;
        }

        public IEnumerable<Node> GetPath()
        {
            var n = prev;
            while (n != null)
            {
                yield return n;
                n = n.prev;
            }
        }
    }
}
