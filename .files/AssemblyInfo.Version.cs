using System.Reflection;

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//

[assembly: AssemblyVersion("3.1.0.0")]
[assembly: AssemblyFileVersion("3.1.0.0")]
[assembly: AssemblyInformationalVersion("3.1.0.0")]

#if (!DEBUG)
[assembly: Platform.Support.Attributes.ProductLevel(Platform.Support.ProductLevels.RTW)]
#else
[assembly: Platform.Support.Attributes.ProductLevel(Platform.Support.ProductLevels.Preview)]
#endif
