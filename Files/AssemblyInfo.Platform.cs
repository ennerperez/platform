using System.Reflection;

[assembly: AssemblyProduct("Platform")]

[assembly: AssemblyVersion("3.0.0.6")]
[assembly: AssemblyFileVersion("3.0.0.6")]

[assembly: AssemblyInformationalVersion("3.0.0 RTM")]

#if !CORE && PORTABLE
//[assembly: Platform.Support.Attributes.AssemblyProduct.ProductLevel(Platform.Support.ProductLevels.RTW, 1)]    
#elif !NUGET
[assembly: Platform.Support.Attributes.AssemblyProduct.ProductLevel(Platform.Support.ProductLevels.RTW, 1)]
#endif

