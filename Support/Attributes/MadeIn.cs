using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
#if !CORE
    namespace Core
    {
#endif
    namespace Attributes.AssemblyProduct
    {

        /// <summary>
        /// Developer information
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]

        public class MadeInAttribute : global::System.Attribute
        {

            private string countryName;
            private string countryCode;

            public MadeInAttribute(string countryname, string countrycode = null)
            {
                this.countryName = countryname;
                this.countryCode = countrycode;
            }

            public virtual string CountryName
            {
                get { return countryName; }
            }
            public virtual string CountryCode
            {
                get { return this.countryCode.ToString(); }
            }

        }
    }

#if !CORE
    }
#endif

}