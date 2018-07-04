using System;
using System.Collections.Generic;
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
        private const string langFile = ".\\original - {0}.lang";

        private string modNameSpace;
        private string jsonDir;

        private ProcessStartInfo info;

        private List<string> replaceWords = null;
        private string thePath;

        private bool needsCleanUp;
        private bool needsLog;
        private bool fromJson;
        private DateTime startTime;

        public Cs2Lang(string path, string modNameSpace,
            bool needsCleanUp = true, bool needsLog = false, bool fromJson = false, string replaceFile = null)
        {
            thePath = path;
            this.modNameSpace = fromJson ? (string.IsNullOrEmpty(path.Substring(path.LastIndexOf("\\") + 1)) ?
                    "Unknown" : path.Substring(path.LastIndexOf("\\") + 1))
                : modNameSpace;
            this.needsCleanUp = needsCleanUp;
            this.needsLog = needsLog;
            this.fromJson = fromJson;
            if (!string.IsNullOrWhiteSpace(replaceFile))
                replaceWords = ReplacementList(replaceFile);
            if (!fromJson && !File.Exists(executeFile))
            {
                throw new FileNotFoundException(string.Format(Strings.FileNotFound, executeFile));
            }

            if (fromJson && !Directory.Exists(path))
            {
                throw new FileNotFoundException(string.Format(Strings.FileNotFound, path));
            }

            if (!fromJson && !File.Exists(path))
            {
                throw new FileNotFoundException(string.Format(Strings.FileNotFound, path));
            }

            if (!fromJson)
            {
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
        }

        public void Start()
        {
            if (!fromJson)
                Dump();
            Convert();
            if (needsCleanUp && !needsLog)
                CleanUpLog();
            if (!fromJson && needsCleanUp)
                CleanUpOther();
        }

        private void Dump()
        {
            startTime = DateTime.Now;
            var process = Process.Start(info);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Strings.Output);
            Console.ResetColor();
            string str;
            while ((str = process.StandardOutput.ReadLine()) != null)
            {
                if (str.Contains("Fatal"))
                {
                    WriteLineWithForegroundColor(str, ConsoleColor.Magenta);
                }
                else if (str.Contains("Warn"))
                {
                    WriteLineWithForegroundColor(str, ConsoleColor.DarkRed);
                }
                else if (str.Contains("Error"))
                {
                    WriteLineWithForegroundColor(str, ConsoleColor.Yellow);
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
            jsonDir = fromJson ? thePath : Path.Combine(currentWorkingDir, modNameSpace);
            var writer = new LangWriter(string.Format(langFile, modNameSpace), modNameSpace, replaceWords);
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
            writer.CloseStream();
        }

        private void CleanUpOther()
        {
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
            Console.WriteLine(Strings.FileCleanedUp);
        }

        private void CleanUpLog()
        {
            foreach (var file in Directory.GetFiles(executePath))
            {
                //if (Path.GetExtension(file) == ".log")
                //    File.Delete(file);
                if (Path.GetFileName(file) == string.Format("localizer.{0:D2}_{1:D2}.{2:D2}_{3:D2}.log",
                        startTime.Month, startTime.Day,
                        startTime.Hour > 12 ? startTime.Hour - 12 : startTime.Hour, startTime.Minute))
                {
                    File.Delete(file);
                }
            }
        }

        private static List<string> ReplacementList(string file)
        {
            var list = new List<string>();
            using (var reader = new StreamReader(new FileStream(file, FileMode.Open)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("//"))
                    {
                        list.Add(line);
                    }
                }
            }

            return list;
        }

        public void WriteLineWithForegroundColor(string line, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ResetColor();
        }
    }
}
