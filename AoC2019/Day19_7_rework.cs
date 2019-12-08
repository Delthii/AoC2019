using System.Collections.Generic;
using System.Linq;

namespace AoC2019
{
    class Day19_7_rework : AbstractDay
    {
        public Day19_7_rework() : base(7,2019)
        {
        }

        public override string PartA()
        {
            return Core(Phases(0,4));
        }

        public override string PartB()
        {
            return Core(Phases(5,9));
        }

        public string Core(IEnumerable<int[]> phases)
        {
            var program = Lines.First().Split(',').Select(int.Parse).ToArray();
            var outputs = new List<int>();

            foreach (var phase in phases)
            {
                var inpA = new List<int> { phase[0], 0 };
                var inpB = new List<int> { phase[1] };
                var inpC = new List<int> { phase[2] };
                var inpD = new List<int> { phase[3] };
                var inpE = new List<int> { phase[4] };
                var ca = new SeqCompiler(inpA, inpB, program.ToArray());
                var cb = new SeqCompiler(inpB, inpC, program.ToArray());
                var cc = new SeqCompiler(inpC, inpD, program.ToArray());
                var cd = new SeqCompiler(inpD, inpE, program.ToArray());
                var ce = new SeqCompiler(inpE, inpA, program.ToArray());
                do
                {
                    while (ca.Step() == State.Running) { }
                    while (cb.Step() == State.Running) { }
                    while (cc.Step() == State.Running) { }
                    while (cd.Step() == State.Running) { }
                    while (ce.Step() == State.Running) { }
                } while (!ce.Done());
                outputs.Add(ce.LastOut);
            }

            return outputs.Max() + "";
        }

        IEnumerable<int[]> Phases(int lower, int upper)
        {
            for (int A = lower; A <= upper; ++A)
            for (int B = lower; B <= upper; ++B)
            for (int C = lower; C <= upper; ++C)
            for (int D = lower; D <= upper; ++D)
            for (int E = lower; E <= upper; ++E)
            {
                var set = new HashSet<int>();
                set.Add(A);
                set.Add(B);
                set.Add(C);
                set.Add(D);
                set.Add(E);

                if (set.Count != 5) continue;
                yield return new[] { A, B, C, D, E };
            }
        }
    }
}
