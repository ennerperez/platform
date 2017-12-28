using System.Resources;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

#if !PORTABLE

using Platform.Support;
using Platform.Support.Reflection;

#else

using Platform.Support.Core;
using Platform.Support.Core.Reflection;

#endif

#if !NETSTANDARD2_0
[assembly: AssemblyProduct("Platform")]
#endif
#if !PORTABLE || PROFILE_78
[assembly: ComVisible(false)]
#endif
[assembly: CLSCompliant(false)]
#if (DEBUG)
[assembly: AssemblyProductLevel(ProductLevels.Preview)]
#if !NETSTANDARD2_0
[assembly: AssemblyConfiguration("Debug")]
#endif
#else
[assembly: AssemblyProductLevel(ProductLevels.RTW)]
#if !NETSTANDARD2_0
[assembly: AssemblyConfiguration("Release")]
#endif
#endif

[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]
[assembly: GitHub("ennerperez", "platform")]