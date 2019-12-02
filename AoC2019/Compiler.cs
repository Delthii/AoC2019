using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    class Compiler
    {
        public void Run(int[] program)
        {
            for (var i = 0; i < program.Length; i += 4)
            {
                switch (program[i])
                {
                    case 1:
                        Add(program, program[i + 1], program[i + 2], program[i + 3]);
                        break;
                    case 2:
                        Multiply(program, program[i + 1], program[i + 2], program[i + 3]);
                        break;
                    case 99:
                        return;
                    default:
                        throw new Exception("Invalid OP-Code");
                }
            }
        }

        private static void Instruction(int[]program, int first, int second, int store, Func<int,int,int> func)
        {
            program[store] = func(program[first], program[second]);
        }

        private static void Add(int[] program, int first, int second, int store)
        {
            Instruction(program, first, second, store, (a, b) => a + b);
        }

        private static void Multiply(int[] program, int first, int second, int store)
        {
            Instruction(program, first, second, store, (a, b) => a * b);
        }
    }
}
