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

        [AttributeUsage(AttributeTargets.Assembly)]
        public class IdAttribute : global::System.Attribute
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

#if PORTABLE
    }
#endif

}