using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Cs2Lang.Trash
{
    class LangWriter
    {
        private const string unknown = "===UNKNWN===";

        private StreamWriter writer;

        private List<string> replaceWords;
        private bool needsReplacement;

        private string modName;

        public LangWriter(string path, string namesp, List<string> replaceWords = null)
        {
            writer = new StreamWriter(new FileStream(path, FileMode.Create));
            this.replaceWords = replaceWords;
            needsReplacement = replaceWords != null;
            modName = namesp;
            if (needsReplacement)
            {
                writer.WriteLine();
                writer.WriteLine("// Commons");
                writer.WriteLine();
                WriteFileCommon();
                writer.WriteLine();
            }
        }

        public void WriteLine(string line)
        {
            writer.WriteLine(line);
        }

        public void WriteThisFile(string file, TranslationTypes type)
        {
            // GetCommonThroughFile(file);

            writer.WriteLine();
            writer.WriteLine($"// {Path.GetFileNameWithoutExtension(file)}");
            writer.WriteLine("\n");

            string jstr;
            using (var reader = new StreamReader(new FileStream(file, FileMode.Open)))
            {
                jstr = reader.ReadToEnd();
            }

            var jarr = JArray.Parse(jstr);

            string typeName = null;
            string tip = null;
            string name = null;
            foreach (var obj in jarr)
            {
                typeName = obj["TypeName"].Value<string>();
                name = obj["Name"].Value<string>();
                name = needsReplacement ? CheckReplace(name) : name;
                if (type != TranslationTypes.Tiles && name.Length == 0)
                    writer.WriteLine(unknown);
                if (type == TranslationTypes.Buffs)
                {
                    tip = obj["Tip"].Value<string>().Replace("\n", "\\n");
                    tip = needsReplacement ? CheckReplace(tip) : tip;
                    writer.WriteLine($"BuffName.{typeName}={name}");
                    if (tip.Length != 0)
                        writer.WriteLine($"BuffDescription.{typeName}={tip}");
                    writer.WriteLine();
                }
                else if (type == TranslationTypes.Items)
                {
                    tip = obj["ToolTip"].Value<string>().Replace("\n", "\\n");
                    tip = needsReplacement ? CheckReplace(tip) : tip;
                    writer.WriteLine($"ItemName.{typeName}={name}");
                    if (tip.Length != 0)
                        writer.WriteLine($"ItemTooltip.{typeName}={tip}");
                    writer.WriteLine();
                }
                else if (type == TranslationTypes.NPCs)
                {
                    writer.WriteLine($"NPCName.{typeName}={name}");
                    writer.WriteLine();
                }
                else if (type == TranslationTypes.Tiles)
                {
                    if (name.Length != 0)
                    {
                        writer.WriteLine($"MapObject.{typeName}={name}");
                        writer.WriteLine();
                    }
                }
            }

            // flush writer
            writer.Flush();
        }

        private void WriteFileCommon()
        {
            for (int i = 0; i < replaceWords.Count; i++)
            {
                writer.WriteLine($"Common.Name{i:D}={replaceWords[i]}");
            }
        }

        private string CheckReplace(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            var newstr = str;
            for (int i = 0; i < replaceWords.Count; i++)
            {
                newstr = newstr.Replace(replaceWords[i],
                    $"{{$Mods.{modName}.Common.Name{i}}}");
            }

            return newstr;
        }

        // TODO: read through the file to store names and desc into a dict.
        private void GetCommonThroughFile(string file)
        {
            throw new NotImplementedException();
        }

        public void CloseStream()
        {
            writer.Close();
        }
    }
}
