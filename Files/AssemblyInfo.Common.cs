using System.Reflection;


[assembly: AssemblyCompany("Argument")]

#if PERSONAL

[assembly: AssemblyCopyright("© Enner Pérez. All rights reserved., 2015")]
[assembly: AssemblyTrademark("Enner Pérez")]

#if CORE
[assembly: Support.Attributes.AssemblyCompany.Id("18384909")]
[assembly: Support.Attributes.AssemblyCompany.Contact("Twitter", "@ennerperez")]
[assembly: Support.Attributes.AssemblyCompany.Contact("04146328236")]
[assembly: Support.Attributes.AssemblyCompany.Mail("ennerperez@gmail.com")]
[assembly: Support.Attributes.AssemblyProduct.Developer("Enner Pérez")]
//[assembly: Support.Attributes.AssemblyCompany.Url("http://www.ennerperez.net/")]
#else
[assembly: Support.Core.Attributes.AssemblyCompany.Id("18384909")]
[assembly: Support.Core.Attributes.AssemblyCompany.Contact("Twitter", "@ennerperez")]
[assembly: Support.Core.Attributes.AssemblyCompany.Contact("04146328236")]
[assembly: Support.Core.Attributes.AssemblyCompany.Mail("ennerperez@gmail.com")]
[assembly: Support.Core.Attributes.AssemblyProduct.Developer("Enner Pérez")]
//[assembly: Support.Core.Attributes.AssemblyCompany.Url("http://www.ennerperez.net/")]
#endif

#else

[assembly: AssemblyCopyright("© Argument C.A. Todos los derechos reservados, 2015")]
[assembly: AssemblyTrademark("© Argument C.A.")]

#if !CORE && PORTABLE
[assembly: Support.Core.Attributes.AssemblyCompany.Id("J-18384909-0")]
#else
[assembly: Support.Attributes.AssemblyCompany.Id("J-18384909-0")]
#endif

#endif


