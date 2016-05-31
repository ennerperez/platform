using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
#if PORTABLE
using Platform.Support.Core;
using Platform.Support.Core.Attributes;
#else
using Platform.Support;
using Platform.Support.Attributes;
#endif

#if (!COMPANY)

[assembly: AssemblyCompany("Enner Pérez")]
[assembly: Id("J-18384909-0")]
[assembly: AssemblyCopyright("Copyright © Enner Pérez")]
[assembly: AssemblyTrademark("Enner Pérez")]

[assembly: Contact("Twitter", "@ennerperez")]
[assembly: Contact("584146328236")]
[assembly: Mail("ennerperez@gmail.com")]

//[assembly: Url("")]

#else

[assembly: AssemblyCompany("Argument")]
[assembly: Id("J-18384909-0")]
[assembly: AssemblyCopyright("Copyright © Argument C.A.")]
[assembly: AssemblyTrademark("Argument C.A.")]

//[assembly: Url("")]

#endif

[assembly: MadeIn("Venezuela", "VE")]
[assembly: Developer("Enner Pérez", "@ennerperez")]
