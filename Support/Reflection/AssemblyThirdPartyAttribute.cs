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

        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
        public sealed class AssemblyThirdPartyAttribute : global::System.Attribute
        {

            public AssemblyThirdPartyAttribute(string name, string info = null)
            {
                this.name = name;
                this.info = info;
            }

            private string name;
            public string Name { get { return name; } }

            private string info;
            public string Info { get { return info; } }

        }

    }

#if PORTABLE
    }
#endif
}