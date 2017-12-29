#if !PORTABLE

using Platform.Support.Reflection;

#else
using Platform.Support.Core.Reflection;
#endif

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Platform Samples Core")]
[assembly: AssemblyDescription("Platform Samples Core")]
#if !PORTABLE
[assembly: Guid("ddfa08aa-9ae7-4a07-b9fc-8fefccd82160")]
#endif
[assembly: AssemblyBuildDate("2017-12-28")]