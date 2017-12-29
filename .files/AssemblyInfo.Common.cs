using System.Reflection;

#if PORTABLE

using Platform.Support.Core;
using Platform.Support.Core.Reflection;
using Platform.Support.Core.Attributes;

#else

using Platform.Support;
using Platform.Support.Reflection;
using Platform.Support.Attributes;

#endif

#if !NETSTANDARD2_0 && !NETCOREAPP2_0
[assembly: AssemblyCompany("Enner Pérez")]
[assembly: AssemblyCopyright("Copyright © Enner Pérez")]
#endif
[assembly: AssemblyTrademark("Enner Pérez")]
[assembly: AssemblyCompanyId("J-18384909-0")]
[assembly: AssemblyCompanyUrl("http://www.ennerperez.com.ve/")]
[assembly: AssemblyMadeIn("Venezuela", "VE")]
[assembly: AssemblyDeveloper("Enner Pérez", "@ennerperez")]
[assembly: AssemblyLicense("The MIT License (MIT)")]
[assembly: AssemblyContactInformation("twitter", "@ennerperez")]
[assembly: AssemblyContactInformation("584146328236")]
[assembly: AssemblyContactInformation("ennerperez@gmail.com")]