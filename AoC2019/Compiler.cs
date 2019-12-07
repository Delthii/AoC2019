using System;
using System.Collections.Concurrent;

namespace AoC2019
{
    class Compiler
    {
        public ConcurrentBag<int> Input { get; } = new ConcurrentBag<int>();
        public ConcurrentBag<int> Output { get; set; } = new ConcurrentBag<int>();
        public int LastOut = -123;

        int A = 0, B = 0, C = 0;

        public void Run(int[] program, int phase, bool a)
        {
            Input.Add(phase);

            for (var i = 0; i < program.Length; )
            {
                var code = ParseInput(program, i);

                switch (code)
                {
                    case 1:
                        Add(program, program[i + 1], program[i + 2], program[i + 3]);
                        i += 4;
                        break;
                    case 2:
                        Multiply(program, program[i + 1], program[i + 2], program[i + 3]);
                        i += 4;
                        break;
                    case 3:
                        int inp;
                        while (!Input.TryTake(out inp))
                        {
                        }
                        
                        program[program[i + 1]] = inp;
                        i += 2;
                        break;
                    case 4:
                        LastOut = C == 0 ? program[program[i + 1]] : program[i + 1];
                        Output.Add(LastOut);
                        if (a) return;
                        i += 2;
                        break;
                    case 5:
                        JumpIfTrue(program, program[i + 1], program[i + 2], ref i);
                        break;
                    case 6:
                        JumpIfFalse(program, program[i + 1], program[i + 2], ref i);
                        break;
                    case 7:
                        LessThan(program, program[i + 1], program[i + 2], program[i + 3]);
                        i += 4;
                        break;
                    case 8:
                        Equals(program, program[i + 1], program[i + 2], program[i + 3]);
                        i += 4;
                        break;
                    case 99:
                        return;
                    default:
                        throw new Exception("Invalid OP-Code");
                }
            }

            throw new Exception("No output :/");
        }

        private int ParseInput(int[] program, int i)
        {
            A = 0;
            B = 0;
            C = 0;
            var op = program[i].ToString();
            int code;
            if (op.Length == 5)
            {
                A = int.Parse(op.Substring(0, 1));
                B = int.Parse(op.Substring(1, 1));
                C = int.Parse(op.Substring(2, 1));
                code = int.Parse(op.Substring(3, 2));
            }
            else if (op.Length == 4)
            {
                B = int.Parse(op.Substring(0, 1));
                C = int.Parse(op.Substring(1, 1));
                code = int.Parse(op.Substring(2, 2));
            }
            else if (op.Length == 3)
            {
                C = int.Parse(op.Substring(0, 1));
                code = int.Parse(op.Substring(1, 2));
            }
            else
            {
                code = int.Parse(op.Substring(0));
            }

            return code;
        }

        public void Run(int[] program)
        {
            for (var i = 0; i < program.Length;)
            {
                var code = ParseInput(program, i);

                switch (code)
                {
                    case 1:
                        Add(program, program[i + 1], program[i + 2], program[i + 3]);
                        i += 4;
                        break;
                    case 2:
                        Multiply(program, program[i + 1], program[i + 2], program[i + 3]);
                        i += 4;
                        break;
                    case 3:
                        int inp;
                        Input.TryTake(out inp);
                        program[program[i + 1]] = inp;
                        i += 2;
                        break;
                    case 4:
                        var outp = C == 0 ? program[program[i + 1]] : program[i + 1];
                        Console.WriteLine(outp);
                        i += 2;
                        break;
                    case 5:
                        JumpIfTrue(program, program[i + 1], program[i + 2], ref i);
                        break;
                    case 6:
                        JumpIfFalse(program, program[i + 1], program[i + 2], ref i);
                        break;
                    case 7:
                        LessThan(program, program[i + 1], program[i + 2], program[i + 3]);
                        i += 4;
                        break;
                    case 8:
                        Equals(program, program[i + 1], program[i + 2], program[i + 3]);
                        i += 4;
                        break;
                    case 99:
                        return;
                    default:
                        throw new Exception("Invalid OP-Code");
                }
            }
        }

        private void Instruction(int[]program, int first, int second, int store, Func<int,int,int> func)
        {
            program[store] = func(C == 0 ? program[first] : first, B == 0 ? program[second] : second);
        }

        private void Instruction(int[] program, int first, int second, int third,int store, Func<int, int, int> func)
        {
            program[store] = func(C == 0 ? program[first] : first, B == 0 ? program[second] : second);
        }

        private void Add(int[] program, int first, int second, int store)
        {
            Instruction(program, first, second, store, (a, b) => a + b);
        }

        private void Multiply(int[] program, int first, int second, int store)
        {
            Instruction(program, first, second, store, (a, b) => a * b);
        }

        private void JumpIfTrue(int[] program, int first, int second, ref int i)
        {
            if ((C == 0 ? program[first] : first) != 0)
            {
                i = B == 0 ? program[second] : second;
            }
            else
            {
                i += 3;
            }
        }

        private void JumpIfFalse(int[] program, int first, int second, ref int i)
        {
            if ((C == 0 ? program[first] : first)== 0)
            {
                i = B == 0 ? program[second] : second;
            }
            else
            {
                i += 3;
            }
        }

        private void LessThan(int[] program, int first, int second,int store)
        {
            program[store] = ((C == 0 ? program[first] : first) < (B == 0 ? program[second] : second)) ? 1 : 0;
        }

        private void Equals(int[] program, int first, int second, int store)
        {
            program[store] = ((C == 0 ? program[first] : first) == (B == 0 ? program[second] : second)) ? 1 : 0;
        }

    }
}
