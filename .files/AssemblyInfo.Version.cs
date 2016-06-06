using System.Reflection;
#if PORTABLE
using Platform.Support.Core;
using Platform.Support.Core.Attributes;
#else
using Platform.Support;
using Platform.Support.Attributes;
#endif

[assembly: AssemblyVersion("3.1.11.*")]
[assembly: AssemblyFileVersion("3.1")]

#if (!DEBUG)
[assembly: ProductLevel(ProductLevels.RTW)]
#else
[assembly: ProductLevel(ProductLevels.Preview)]
#endif
