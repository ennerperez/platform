using System.Reflection;

[assembly: AssemblyVersion("3.0.0.8")]
[assembly: AssemblyFileVersion("3.0.0.8")]

[assembly: AssemblyInformationalVersion("3.0.0 RTM UNRELEASED")]

#if (!DEBUG)
[assembly: Platform.Support.Attributes.AssemblyProduct.ProductLevel(Platform.Support.ProductLevels.RTW)]
#else
[assembly: Platform.Support.Attributes.AssemblyProduct.ProductLevel(Platform.Support.ProductLevels.Preview)]
#endif
