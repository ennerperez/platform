using System.Reflection;

// <major version>.<minor version>.<build number>.<revision>

[assembly: AssemblyVersion("3.0.1.10")]
[assembly: AssemblyFileVersion("3.0.1.10")]

[assembly: AssemblyInformationalVersion("3.0.1 RTM")]

#if (!DEBUG)
[assembly: Platform.Support.Attributes.AssemblyProduct.ProductLevel(Platform.Support.ProductLevels.RTW)]
#else
[assembly: Platform.Support.Attributes.AssemblyProduct.ProductLevel(Platform.Support.ProductLevels.Preview)]
#endif
