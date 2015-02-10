﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support
{
#if !CORE
    namespace Core
    {
#endif

        namespace Attributes.AssemblyCompany
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

#if !CORE
    }
#endif

}