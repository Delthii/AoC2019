using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_7 : AbstractDay
    {
        public override string PartA()
        {
            var program = Lines.First().Split(',').Select(int.Parse);

            var outputs = new List<int>();

            foreach (var phases in PhasesA())
            {
                var ca = InitCompilers(out var cb, out var cc, out var cd, out var ce);
                ca.Input.Add(0);
                var t1 = new Thread(d => { ca.Run(program.ToArray(), phases[0], true); });
                var t2 = new Thread(d => { cb.Run(program.ToArray(), phases[1], true); });
                var t3 = new Thread(d => { cc.Run(program.ToArray(), phases[2], true); });
                var t4 = new Thread(d => { cd.Run(program.ToArray(), phases[3], true); });
                var t5 = new Thread(d => { ce.Run(program.ToArray(), phases[4], true); });
                Start(t1, t2, t3, t4, t5);
                t5.Join();
                outputs.Add(ce.LastOut);
            }

            return outputs.Max() + "";
        }

        public override string PartB()
        {
            var program = Lines.First().Split(',').Select(int.Parse);

            var outputs = new List<int>();

            foreach (var phases in PhasesB())
            {
                var ca = InitCompilers(out var cb, out var cc, out var cd, out var ce);
                ca.Input.Add(0);
                var t1 = new Thread(d => { ca.Run(program.ToArray(), phases[0], false); });
                var t2 = new Thread(d => { cb.Run(program.ToArray(), phases[1], false); });
                var t3 = new Thread(d => { cc.Run(program.ToArray(), phases[2], false); });
                var t4 = new Thread(d => { cd.Run(program.ToArray(), phases[3], false); });
                var t5 = new Thread(d => { ce.Run(program.ToArray(), phases[4], false); });
                Start(t1, t2, t3, t4, t5);
                t5.Join();
                outputs.Add(ce.LastOut);
            }

            return outputs.Max() + "";
        }

        private static Compiler InitCompilers(out Compiler cb, out Compiler cc, out Compiler cd, out Compiler ce)
        {
            var ca = new Compiler();
            cb = new Compiler();
            cc = new Compiler();
            cd = new Compiler();
            ce = new Compiler();
            ca.Output = cb.Input;
            cb.Output = cc.Input;
            cc.Output = cd.Input;
            cd.Output = ce.Input;
            ce.Output = ca.Input;
            return ca;
        }

        private static void Start(Thread t1, Thread t2, Thread t3, Thread t4, Thread t5)
        {
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
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
