using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

#if (!COMPANY)

[assembly: AssemblyCompany("Enner Pérez")]
[assembly: Platform.Support.Attributes.Id("J-18384909-0")]
[assembly: AssemblyCopyright("© Enner Pérez. All rights reserved.")]
[assembly: AssemblyTrademark("© Enner Pérez")]

[assembly: Platform.Support.Attributes.Contact("Twitter", "@ennerperez")]
[assembly: Platform.Support.Attributes.Contact("584146328236")]
[assembly: Platform.Support.Attributes.Mail("ennerperez@gmail.com")]

//[assembly: Platform.Support.Attributes.Url("")]

#else

[assembly: AssemblyCompany("Argument")]
[assembly: Platform.Support.Attributes.Id("J-18384909-0")]
[assembly: AssemblyCopyright("© Argument C.A. All rights reserved.")]
[assembly: AssemblyTrademark("© Argument C.A.")]

//[assembly: Platform.Support.Attributes.Url("")]

#endif

[assembly: Platform.Support.Attributes.MadeIn("Venezuela", "VE")]
[assembly: Platform.Support.Attributes.Developer("Enner Pérez", "@ennerperez")]
