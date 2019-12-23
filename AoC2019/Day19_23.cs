using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Day19_23 : AbstractDay
    {
        public Day19_23() : base(23,2019)
        {
            
        }
        List<SeqCompiler> NICs = new List<SeqCompiler>();
        List<List<long>> Inputs = new List<List<long>>();
        List<List<long>> Outputs = new List<List<long>>();

        private long NatX = 0;
        private long NatY = 0;
        private long Y = -123123;
        public override string PartA()
        {
            var program = File.ReadAllText("..\\..\\in.txt").Split(',').Select(long.Parse).ToArray();
            for (int i = 0; i < 50; i++)
            {
                var input = new List<long>{i};
                var output = new List<long>();
                var NIC = new SeqCompiler(input,output,program.ToArray());
                NICs.Add(NIC);
                Outputs.Add(output);
                Inputs.Add(input);
            }
            bool idle = false;
            var cnt = 0;
            while (true)
            {
                if (idle)
                {
                    cnt++;
                    if (cnt > 20000)
                    {
                        cnt = 0;
                        Inputs[0].Add(NatX);
                        Inputs[0].Add(NatY);
                        if (Y == NatY)
                        {

                        }
                        Console.WriteLine(NatY);
                        Y = NatY;
                    }
                }
                idle = true;
                for (int i = 0; i < 50; i++)
                {
                    var nic = NICs[i];
                    var input = Inputs[i];
                    var output = Outputs[i];
                    nic.RunIt();

                    if (input.Count == 0)
                    {
                        input.Add(-1);
                    }

                    if (output.Count == 3)
                    {
                        if ((int) output[0] == 255)
                        {
                            NatX = output[1];
                            NatY = output[2];
                        }
                        else
                        {
                            Inputs[(int)output[0]].Add(output[1]);
                            Inputs[(int)output[0]].Add(output[2]);
                            idle = false;
                            cnt = 0;
                        }
                        
                        output.Clear();
                    }
                }
            }

            return "";
        }

        public override string PartB()
        {
            throw new NotImplementedException();
        }
    }
}
