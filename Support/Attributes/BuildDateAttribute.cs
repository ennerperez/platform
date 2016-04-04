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
        namespace Attributes
        {
            /// <summary>
            /// Define the build <typeparamref name="datetime"/> for the project
            /// </summary>
            [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
#if !CORE
            internal sealed class BuildDateAttribute : global::System.Attribute
#else
        public sealed class BuildDateAttribute : global::System.Attribute
#endif
            {
                private DateTime assemblyDate;
                public DateTime AssemblyDate { get { return assemblyDate; } }
                       
                public BuildDateAttribute(String date)
                {
                    assemblyDate = DateTime.Parse(date);
                }

                public BuildDateAttribute(DateTime date)
                {
                    assemblyDate = date;
                }

            }

        }
#if !CORE
    }
#endif

}
