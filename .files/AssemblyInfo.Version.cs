using System.Reflection;

[assembly: AssemblyVersion("3.0.1.0")]
[assembly: AssemblyFileVersion("3.0.1.0")]

[assembly: AssemblyInformationalVersion("3.0.1 RTM")]

#if (!DEBUG)
[assembly: Platform.Support.Attributes.AssemblyProduct.ProductLevel(Platform.Support.ProductLevels.RTW)]
#else
[assembly: Platform.Support.Attributes.AssemblyProduct.ProductLevel(Platform.Support.ProductLevels.Preview)]
#endif
