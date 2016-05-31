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
            [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]

            public class DeveloperAttribute : global::System.Attribute
            {

                private string name;
                private object aditional;

                public DeveloperAttribute(string name, object aditional = null)
                {
                    this.name = name;
                    this.aditional = aditional;
                }

                public virtual string DeveloperName
                {
                    get { return name; }
                }
                public virtual string AditionalInfo
                {
                    get { return this.aditional.ToString(); }
                }

            }
        }

#if PORTABLE
    }
#endif

}