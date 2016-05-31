using System.Reflection;
#if PORTABLE
using Platform.Support.Core;
using Platform.Support.Core.Attributes;
#else
using Platform.Support;
using Platform.Support.Attributes;
#endif

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//

[assembly: AssemblyVersion("3.1.0.*")]
[assembly: AssemblyFileVersion("3.1.0")]
//[assembly: AssemblyInformationalVersion("3.1.0-master")]

#if (!DEBUG)
[assembly: ProductLevel(ProductLevels.RTW)]
#else
[assembly: ProductLevel(ProductLevels.Preview)]
#endif
