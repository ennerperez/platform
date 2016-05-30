using System.Resources;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyProduct("Platform")]

#if !PORTABLE
[assembly: ComVisible(false)]
#endif
[assembly: CLSCompliant(false)]
//[assembly: SecurityRules(SecurityRuleSet.Level2)] 

#if (DEBUG)
[assembly: AssemblyConfiguration("DEBUG")]
#else
[assembly: AssemblyConfiguration("RELEASE")]
#endif

[assembly: NeutralResourcesLanguage("en")]