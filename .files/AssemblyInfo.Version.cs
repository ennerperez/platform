using System.Reflection;

#if PORTABLE

using Platform.Support.Core;
using Platform.Support.Core.Attributes;

#else

using Platform.Support;
using Platform.Support.Attributes;

#endif

#if !NETSTANDARD2_0
[assembly: AssemblyVersion("3.3.33.0")]
[assembly: AssemblyFileVersion("3.3.33")]
#endif