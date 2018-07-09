using System.Runtime.InteropServices;

namespace Cs2Lang.Trash
{
    class Pwtcl
    {
        [DllImport("Pwtcl.dll", EntryPoint = "pwe", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int pwe(string str);
    }
}
