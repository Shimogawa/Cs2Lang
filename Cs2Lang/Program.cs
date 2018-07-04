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
        private static bool needsLog = false;
        private static bool fromJson = false;
        private static string replaceFile = null;

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
            var process = new Cs2Lang(modPath, modName, needsCleanUp, needsLog, fromJson, replaceFile);
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

            var helpOption = app.HelpOption("-h | --help");
            helpOption.Description = Strings.HelpOption;
            var versionOption = app.VersionOption("-v | --version", version.ToString(3), version.ToString(4));
            versionOption.Description = Strings.VersionOption;

            var cleanupOption = app.Option("-nc | --nocleanup", Strings.CleanupOption, CommandOptionType.NoValue);
            var logOption = app.Option("-l | --log", Strings.LogOption, CommandOptionType.NoValue);
            var replaceOption = app.Option("-r | --replace", Strings.ReplaceOption, CommandOptionType.SingleValue);
            var fromJsonOption = app.Option("-j | --usejson", Strings.FromJsonOption, CommandOptionType.NoValue);
            var secret = app.Option("--password", "", CommandOptionType.SingleValue);

            var modFilePathArg = app.Argument(Strings.PathArgument, Strings.PathArgumentDetail, true);

            app.OnExecute(() =>
            {
                if (fromJsonOption.HasValue())
                {
                    fromJson = true;
                }

                if (!fromJson && modFilePathArg.Values.Count != 2)
                {
                    return -1;
                }

                if (fromJson && (modFilePathArg.Values.Count > 2 || modFilePathArg.Values.Count < 1))
                {
                    return -1;
                }

                if (cleanupOption.HasValue())
                {
                    needsCleanUp = false;
                }

                if (replaceOption.HasValue())
                {
                    replaceFile = replaceOption.Value();
                }

                //if (fromJsonOption.HasValue())
                //{
                //    fromJson = true;
                //}

                if (logOption.HasValue())
                    needsLog = true;

                modPath = modFilePathArg.Values[0];
                modName = fromJson ? null : modFilePathArg.Values[1];
                return 0;
            });

            var finish = args.Length == 1 &&
                         (args[0] == "-v" || args[0] == "--version" || args[0] == "-h" || args[0] == "--help");
            var r = app.Execute(args);
            if (r < 0)
            {
                if (!finish)
                    Console.WriteLine(Strings.WrongArgument);
                return false;
            }

            if (string.IsNullOrWhiteSpace(modPath))
            {
                if (!finish)
                    Console.WriteLine(Strings.WrongArgument);
                return false;
            }

            if (!fromJson && string.IsNullOrWhiteSpace(modName))
            {
                if (!(helpOption.HasValue() || versionOption.HasValue()))
                    Console.WriteLine(Strings.WrongArgument);
                return false;
            }

            return true;
        }
    }
}
