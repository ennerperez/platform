using System.Resources;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyProduct("Platform")]

#if !PORTABLE
[assembly: ComVisible(false)]
#endif
[assembly: CLSCompliant(false)]

#if (DEBUG)
[assembly: AssemblyConfiguration("DEBUG")]
#else
[assembly: AssemblyConfiguration("RELEASE")]
#endif

[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]