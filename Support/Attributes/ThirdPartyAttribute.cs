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

            [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
            public class ThirdPartyAttribute : global::System.Attribute
            {

                private string name;
                private string info;

                public ThirdPartyAttribute(string name, string info = null)
                {
                    this.name = name;
                    this.info = info;
                }

                public virtual string ThirdParty
                {
                    get { return name; }
                }
                public virtual string Info
                {
                    get { return info; }
                }

            }

        }

#if PORTABLE
    }
#endif
}