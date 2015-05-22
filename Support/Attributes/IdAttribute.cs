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

        namespace Attributes.AssemblyCompany
        {

            [AttributeUsage(AttributeTargets.Assembly)]
#if !CORE
            internal class IdAttribute : global::System.Attribute
#else
            public class IdAttribute : global::System.Attribute
#endif
            {

                private string companyid;

                public IdAttribute(string id)
                {
                    this.companyid = id;
                }

                public virtual string CompanyID
                {
                    get { return this.companyid; }
                }
            }

        }

#if !CORE
    }
#endif

}