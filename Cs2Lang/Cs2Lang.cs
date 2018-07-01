using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Cs2Lang.Resources;
using Cs2Lang.Trash;

namespace Cs2Lang
{
    class Cs2Lang
    {
        private const string executeFile = @".\Mod.Localizer_0.2\Mod.Localizer.exe";
        private const string executePath = @".\Mod.Localizer_0.2\";
        private const string currentWorkingDir = @".\";
        private const string langFile = ".\\en-US - {0}.lang";

        private string modNameSpace;
        private string jsonDir;

        private ProcessStartInfo info;
        private string path;

        private bool needsCleanUp;

        public Cs2Lang(string path, string modNameSpace, bool needsCleanUp = true)
        {
            this.modNameSpace = modNameSpace;
            this.needsCleanUp = needsCleanUp;
            if (!File.Exists(executeFile))
            {
                throw new FileNotFoundException(string.Format(Strings.FileNotFound, executeFile));
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(string.Format(Strings.FileNotFound, path));
            }
            this.path = path;
            info = new ProcessStartInfo
            {
                FileName = executeFile,
                Arguments = path,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                StandardOutputEncoding = Encoding.UTF8
            };
        }

        public void Start()
        {
            Dump();
            Convert();
            if (needsCleanUp)
                CleanUp();
        }

        private void Dump()
        {
            var process = Process.Start(info);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Strings.Output);
            Console.ResetColor();
            string str;
            while ((str = process.StandardOutput.ReadLine()) != null)
            {
                if (str.Contains("Fatal"))
                {
                    if (str.Contains("处理完毕"))
                    {
                        Console.WriteLine(str);
                    }
                    else
                    {
                        throw new Exception(Strings.ErrorInDump);
                    }
                }
            }
            process.WaitForExit(100000);
            if (!process.HasExited)
            {
                process.Kill();
                throw new Exception(Strings.ProcessKilled);
            }
            var error = process.StandardError.ReadToEnd();
            if (error.Length != 0)
            {
                throw new Exception("Error: " + error);
            }
            process.Close();
        }

        private void Convert()
        {
            jsonDir = Path.Combine(currentWorkingDir, modNameSpace);
            var writer = new LangWriter(string.Format(langFile, modNameSpace));
            foreach (var dir in Directory.GetDirectories(jsonDir))
            {
                if (dir.Contains(TranslationTypes.Buffs.ToString()))
                {
                    foreach (var file in Directory.GetFiles(dir))
                    {
                        writer.WriteThisFile(file, TranslationTypes.Buffs);
                    }
                }
                else if (dir.Contains(TranslationTypes.Items.ToString()))
                {
                    foreach (var file in Directory.GetFiles(dir))
                    {
                        if (!file.Contains("Items"))
                            continue;
                        writer.WriteThisFile(file, TranslationTypes.Items);
                    }
                }
                else if (dir.Contains(TranslationTypes.NPCs.ToString()))
                {
                    foreach (var file in Directory.GetFiles(dir))
                    {
                        writer.WriteThisFile(file, TranslationTypes.NPCs);
                    }
                }
                else if (dir.Contains(TranslationTypes.Tiles.ToString()))
                {
                    foreach (var file in Directory.GetFiles(dir))
                    {
                        writer.WriteThisFile(file, TranslationTypes.Tiles);
                    }
                }
            }
            writer.Close();
        }

        private void CleanUp()
        {
            foreach (var file in Directory.GetFiles(executePath))
            {
                if (Path.GetExtension(file) == ".log")
                    File.Delete(file);
            }

            foreach (var file in Directory.GetFiles(currentWorkingDir))
            {
                if (Path.GetFileNameWithoutExtension(file) == modNameSpace &&
                    (Path.GetExtension(file) == ".dll" || Path.GetExtension(file) == ".pdb"))
                    File.Delete(file);
            }

            foreach (var directory in Directory.GetDirectories(currentWorkingDir))
            {
                if (directory.Contains(modNameSpace))
                    Directory.Delete(directory, true);
            }
        }
    }
}
