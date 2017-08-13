using System.Runtime.InteropServices;

namespace Platform.Support.Windows
{
#if !INTEROP

    internal class Dwmapi
#else

    public class Dwmapi
#endif
    {
        [DllImport(ExternDll.Dwmapi)]
        public static extern int DwmIsCompositionEnabled(out bool isEnabled);
    }
}