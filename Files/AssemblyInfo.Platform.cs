using System.Reflection;

[assembly: AssemblyProduct("Platform")]

// La información de versión de un ensamblado consta de los cuatro valores siguientes:
//
//      Versión principal
//      Versión secundaria 
//      Número de compilación
//      Revisión
//
// Puede especificar todos los valores o establecer como predeterminados los números de compilación y de revisión 
// mediante el carácter '*', como se muestra a continuación:
// [assembly: AssemblyVersion("1.0.0.*")]
[assembly: AssemblyVersion("3.0.0.5")]
[assembly: AssemblyFileVersion("3.0.0.5")]

[assembly: AssemblyInformationalVersion("3.0.0 RTM")]

#if !CORE && PORTABLE
//[assembly: Platform.Support.Attributes.AssemblyProduct.ProductLevel(Platform.Support.ProductLevels.RTW, 1)]    
#elif !NUGET
[assembly: Platform.Support.Attributes.AssemblyProduct.ProductLevel(Platform.Support.ProductLevels.RTW, 1)]
#endif

