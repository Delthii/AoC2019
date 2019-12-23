using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019
{
    enum State
    {
        Waiting,
        Done,
        Running
    };

    class SeqCompiler
    {

        private List<long> Input { get; }
        private List<long> Output { get; }
        private readonly Dictionary<long,long> program;
        public long LastOut = -123132123;
        private long i;
        private long r;
        private State state;
        long A = 0, B = 0, C = 0;

        public SeqCompiler(List<long> input, List<long> output, long[] program)
        {
            Input = input;
            Output = output;
            this.program = new Dictionary<long, long>();
            for (int i = 0; i < program.Length; i++)
            {
                this.program[i] = program[i];
            }
        }

        public void Run()
        {
            while (Step() != State.Done)
            {
            }
        }

        public void RunIt()
        {
            while (Step() != State.Running)
            {
            }
        }

        public State GetState()
        {
            return state;
        }

        public State Step()
        {
            var code = ParseInput();

            switch (code)
            {
                case 1:
                    Add( program[i + 1], program[i + 2], program[i + 3]);
                    i += 4;
                    break;
                case 2:
                    Multiply( program[i + 1], program[i + 2], program[i + 3]);
                    i += 4;
                    break;
                case 3:
                    if (Input.Count == 0) 
                    {
                        state = State.Waiting;
                        return state;
                    }
                    program[GetStoreIndex(program[i+1], C)] = Input[0];
                    Input.RemoveAt(0);
                    i += 2;
                    break;
                case 4:
                    Output.Add(LastOut = GetValue(program[i + 1], C));
                    i += 2;
                    break;
                case 5:
                    JumpIfTrue( program[i + 1], program[i + 2]);
                    break;
                case 6:
                    JumpIfFalse( program[i + 1], program[i + 2]);
                    break;
                case 7:
                    LessThan(program[i + 1], program[i + 2], program[i + 3]);
                    i += 4;
                    break;
                case 8:
                    Equals(program[i + 1], program[i + 2], program[i + 3]);
                    i += 4;
                    break;
                case 9:
                    RelativeBase(program[i + 1]);
                    break;
                case 99:
                    state = State.Done;
                    return state;
                default:
                    throw new Exception("Invalid OP-Code");
            }

            state = State.Running;
            return state;
        }

        public bool Done()
        {
            return Step() == State.Done;
        }

        private long ParseInput()
        {
            A = 0;
            B = 0;
            C = 0;
            var op = program[i].ToString();
            long code;
            if (op.Length == 5)
            {
                A = long.Parse(op.Substring(0, 1));
                B = long.Parse(op.Substring(1, 1));
                C = long.Parse(op.Substring(2, 1));
                code = long.Parse(op.Substring(3));
            }
            else if (op.Length == 4)
            {
                B = long.Parse(op.Substring(0, 1));
                C = long.Parse(op.Substring(1, 1));
                code = long.Parse(op.Substring(2));
            }
            else if (op.Length == 3)
            {
                C = long.Parse(op.Substring(0, 1));
                code = long.Parse(op.Substring(1));
            }
            else
            {
                code = long.Parse(op);
            }

            return code;
        }

        private long GetValue(long parameter, long mode)
        {
            if (mode == 0) return GetMemory(parameter);
            if (mode == 1) return parameter;
            return GetMemory(parameter + r);
        }

        private long GetMemory(long index)
        {
            if (!program.ContainsKey(index)) program[index] = 0;
            return program[index];
        }

        private void Instruction(long first, long second, long store, Func<long, long, long> func)
        {
            program[GetStoreIndex(store, A)] = func(GetValue(first, C), GetValue(second, B));
        }

        private long GetStoreIndex(long store, long mode)
        {
            return store + (mode == 2 ? r : 0);
        }

        private void Add(long first, long second, long store)
        {
            Instruction(first, second, store, (a, b) => a + b);
        }

        private void Multiply(long first, long second, long store)
        {
            Instruction(first, second, store, (a, b) => a * b);
        }

        private void JumpIfTrue(long first, long second)
        {
            if (GetValue(first, C) != 0) i = GetValue(second, B);
            else i += 3;
        }

        private void JumpIfFalse(long first, long second)
        {
            if (GetValue(first, C) == 0) i = GetValue(second, B);
            else i += 3;
        }

        private void LessThan(long first, long second, long store)
        {
            program[GetStoreIndex(store, A)] = (GetValue(first, C) < GetValue(second, B)) ? 1 : 0;
        }

        private void Equals(long first, long second, long store)
        {
            program[GetStoreIndex(store, A)] = (GetValue(first, C) == GetValue(second, B)) ? 1 : 0;
        }

        private void RelativeBase(long first)
        {
            r += GetValue(first, C);
            i += 2;
        }

    }
}
