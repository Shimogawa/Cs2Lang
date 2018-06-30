using System;
using Cs2Lang.Resources;

namespace Cs2Lang
{
    class Program
    {
        static void Main(string[] args)
        {
            var version = typeof(Program).Assembly.GetName().Version;
            if (args.Length != 2)
            {
                Console.WriteLine(Strings.ProgramInfo, version.ToString(4));
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Strings.SpecificPath);
                Console.WriteLine();
                Console.WriteLine(Strings.ProgramDetail);
                Console.WriteLine();
                Console.ResetColor();
                return;
            }
            var process = new Cs2Lang(args[0], args[1]);
            process.Start();
        }
    }
}
