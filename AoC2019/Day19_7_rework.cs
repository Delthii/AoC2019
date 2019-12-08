using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_7_rework : AbstractDay
    {
        public Day19_7_rework() : base(7,2019)
        {
        }

        public override string PartA()
        {
            var program = Lines.First().Split(',').Select(int.Parse).ToArray();

            var outputs = new List<int>();

            foreach (var phase in PhasesB())
            {
                InitCompilers(out var ca, out var cb, out var cc, out var cd, out var ce, phase, program);
                do
                {
                    while (ca.Step() == State.Running) { }
                    while (cb.Step() == State.Running) { }
                    while (cc.Step() == State.Running) { }
                    while (cd.Step() == State.Running) { }
                    while (ce.Step() == State.Running) { }
                } while (!ce.Done());
                Console.WriteLine(ce.LastOut);
                outputs.Add(ce.LastOut);
            }
            Console.WriteLine(outputs.Max());

            return outputs.Max() + "";
        }

        public override string PartB()
        {
            var program = Lines.First().Split(',').Select(int.Parse);

            var outputs = new List<int>();

            foreach (var phases in PhasesB())
            {
             //outputs.Add(ce.LastOut);
            }

            return outputs.Max() + "";
        }

        private static void InitCompilers(out SeqCompiler ca, out SeqCompiler cb, out SeqCompiler cc, out SeqCompiler cd, out SeqCompiler ce, int[] phase, int[] program)
        {
            var inpA = new List<int>{0, phase[0]};   
            var inpB = new List<int> { phase[1] };
            var inpC = new List<int> { phase[2] };
            var inpD = new List<int> { phase[3] };
            var inpE = new List<int> { phase[4] };
            ca = new SeqCompiler(inpA, inpB, program.ToArray());
            cb = new SeqCompiler(inpB, inpC, program.ToArray());
            cc = new SeqCompiler(inpC, inpD, program.ToArray());
            cd = new SeqCompiler(inpD, inpE, program.ToArray());
            ce = new SeqCompiler(inpE, inpA, program.ToArray());
        }

        IEnumerable<int[]> PhasesA()
        {
            for (int A = 0; A < 5; ++A)
            for (int B = 0; B < 5; ++B)
            for (int C = 0; C < 5; ++C)
            for (int D = 0; D < 5; ++D)
            for (int E = 0; E < 5; ++E)
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

        IEnumerable<int[]> PhasesB()
        {
            for (int A = 5; A < 10; ++A)
            for (int B = 5; B < 10; ++B)
            for (int C = 5; C < 10; ++C)
            for (int D = 5; D < 10; ++D)
            for (int E = 5; E < 10; ++E)
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
