using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
#if PORTABLE
    namespace Core
    {
#endif
    namespace Reflection
    {

        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class AssemblyCompanyUrlAttribute : global::System.Attribute
        {

            public AssemblyCompanyUrlAttribute(Uri uri)
            {
                this.url = uri.ToString();
            }
            public AssemblyCompanyUrlAttribute(string url)
            {
                this.url = url;
            }

            private string url;
            public string Url { get { return url; } }
        }

    }

#if PORTABLE
    }
#endif

}