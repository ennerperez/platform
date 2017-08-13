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

[assembly: AssemblyProduct("Platform")]
#if !PORTABLE || PROFILE_78
[assembly: ComVisible(false)]
#endif
[assembly: CLSCompliant(false)]
#if (DEBUG)
[assembly: AssemblyProductLevel(ProductLevels.Preview)]
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyProductLevel(ProductLevels.RTW)]
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]