using System;

namespace Cs2Lang.Trash
{
    [ConsiderChanging]
    public enum TranslationTypes : byte
    {
        Buffs = 0,
        Items = 1,
        NPCs = 2,
        Tiles = 3,

        Error = 255
    }

    [Obsolete("Should not be used. Use hard code.")]
    public class TypeStringGetter
    {
        public static string GetObjName(TranslationTypes type)
        {
            switch (type)
            {
                case TranslationTypes.Buffs:
                    return "Buff";
                case TranslationTypes.Items:
                    return "Item";
                case TranslationTypes.NPCs:
                    return "NPC";
                case TranslationTypes.Tiles:
                    return "Tile";
                default:
                    return null;
            }
        }
    }
}
