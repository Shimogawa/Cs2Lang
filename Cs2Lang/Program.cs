using System;
using System.Text;
using Cs2Lang.Resources;
using Microsoft.Extensions.CommandLineUtils;

namespace Cs2Lang
{
    class Program
    {
        private static string modPath;
        private static string modName;

        private static bool needsCleanUp = true;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new[] { "-h" };
            }

            if (!ConfigureApp(args))
            {
                return;
            }

            var orig = Console.Title;
            Console.Title = "Cs2Lang - ModLocalizer";
            var process = new Cs2Lang(modPath, modName, needsCleanUp);
            process.Start();
            //Console.Title = orig;
        }

        static bool ConfigureApp(string[] args)
        {
            var version = typeof(Program).Assembly.GetName().Version;

            var app = new CommandLineApplication(false)
            {
                Name = typeof(Program).Namespace,
                FullName = typeof(Program).Namespace,
                ShortVersionGetter = () => version.ToString(3),
                LongVersionGetter = () => version.ToString(4),
                ExtendedHelpText = Strings.ProgramDetail + Environment.NewLine,
                
            };

            app.HelpOption("-h | --help");
            app.VersionOption("-v | --version", version.ToString(3), version.ToString(4));

            var cleanupOption = app.Option("-nc | --nocleanup", Strings.CleanupOption, CommandOptionType.NoValue);

            var modFilePathArg = app.Argument(Strings.PathArgument, Strings.PathArgumentDetail, true);

            app.OnExecute(() =>
            {
                if (modFilePathArg.Values.Count != 2)
                {
                    return -1;
                }

                if (cleanupOption.HasValue())
                {
                    Console.WriteLine();
                    needsCleanUp = false;
                }

                modPath = modFilePathArg.Values[0];
                modName = modFilePathArg.Values[1];
                return 0;
            });

            var r = app.Execute(args);
            if (r < 0)
                return false;

            if (string.IsNullOrWhiteSpace(modPath))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(modName))
            {
                return false;
            }

            return true;
        }
    }
}
