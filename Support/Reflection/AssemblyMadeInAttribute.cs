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

        /// <summary>
        /// Assembly country made in information
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
        public sealed class AssemblyMadeInAttribute : global::System.Attribute
        {

            public AssemblyMadeInAttribute(string name, string code = null) : base()
            {
                this.name = name;
                this.countryCode = code;
            }

            private string name;
            public string Name { get { return name; } }
            private string countryCode;
            public string CountryCode { get { return countryCode; } }

        }
    }

#if PORTABLE
    }
#endif

}