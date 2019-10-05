using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AoC2019
{
    public abstract class AbstractDay : IDay
    {
        private IEnumerable<string> _lines;
        private readonly int _day = 0;
        private readonly int _year = 0;

        protected AbstractDay(int day = 0, int year = 0)
        {
            _day  = day  == 0 ? DateTime.Now.Day  : day;
            _year = year == 0 ? DateTime.Now.Year : year;
        }

        public IEnumerable<string> Lines => _lines ?? InitLines();

        public abstract string PartA();
        public abstract string PartB();

        private IEnumerable<string> InitLines() => _lines = ShellHelper.Init(_year, _day).Result;
    }

    internal static class ShellHelper
    {
        public static Task<int> Bash(this string cmd)
        {
            var source = new TaskCompletionSource<int>();
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Program Files\Git\git-bash.exe",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };
            process.Exited += (sender, args) =>
            {
                if (process.ExitCode == 0)
                    source.SetResult(0);
                else
                    source.SetException(new Exception($"Command `{cmd}` failed with exit code `{process.ExitCode}`"));
                process.Dispose();
            };

            try
            {
                process.Start();
            }
            catch (Exception e)
            {
                source.SetException(e);
            }

            return source.Task;
        }

        public static async Task<IEnumerable<string>> Init(int year, int day)
        {
            var file = $"y{year}d{day}.txt";
            if (!File.Exists(file)) await $"get-input {year} {day} {file}".Bash();

            return File.ReadAllLines(file);
        }
    }
}
