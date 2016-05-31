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
    namespace Attributes
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
                countryName = countryname;
                countryCode = countrycode;
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

#if PORTABLE
    }
#endif

}