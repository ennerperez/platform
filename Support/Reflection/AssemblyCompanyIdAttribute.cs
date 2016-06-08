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
        /// Company Id, usually tax payer id, or a DNI.
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class AssemblyCompanyIdAttribute : global::System.Attribute
        {

            public AssemblyCompanyIdAttribute(string id) : base()
            {
                companyid = id;
            }

            private string companyid;
            public string Id { get { return companyid; } }
        }

    }

#if PORTABLE
    }
#endif

}