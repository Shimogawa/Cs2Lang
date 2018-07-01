using System.IO;
using Newtonsoft.Json.Linq;

namespace Cs2Lang.Trash
{
    class LangWriter
    {

        public StreamWriter writer;

        public LangWriter(string path)
        {
            writer = new StreamWriter(new FileStream(path, FileMode.Create));
        }

        public void WriteLine(string line)
        {
            writer.WriteLine(line);
        }

        public void WriteThisFile(string file, TranslationTypes type)
        {
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
                if (name.Length == 0)
                    name = typeName;
                if (type == TranslationTypes.Buffs)
                {
                    tip = obj["Tip"].Value<string>().Replace("\n", "\\n");
                    writer.WriteLine($"BuffName.{typeName}={name}");
                    if (tip.Length != 0)
                        writer.WriteLine($"BuffDescription.{typeName}={tip}");
                }
                else if (type == TranslationTypes.Items)
                {
                    tip = obj["ToolTip"].Value<string>().Replace("\n", "\\n");
                    writer.WriteLine($"ItemName.{typeName}={name}");
                    if (tip.Length != 0)
                        writer.WriteLine($"ItemToolTip.{typeName}={tip}");
                }
                else if (type == TranslationTypes.NPCs)
                {
                    writer.WriteLine($"NPCName.{typeName}={name}");
                }
                else if (type == TranslationTypes.Tiles)
                {
                    writer.WriteLine($"MapObject.{typeName}={name}");
                }
                writer.WriteLine();
            }

            // flush writer
            writer.Flush();
        }

        public void Close()
        {
            writer.Close();
        }
    }
}
