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
        /// Define the build <typeparamref name="datetime"/> for the project
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class AssemblyBuildDateAttribute : global::System.Attribute
        {

            public AssemblyBuildDateAttribute() : base()
            {
                buildDate = DateTime.Now;
            }

            public AssemblyBuildDateAttribute(string date) : base()
            {
                buildDate = DateTime.Parse(date);
            }

            public AssemblyBuildDateAttribute(int year, int month, int day = 0) : base()
            {
                buildDate = new DateTime(year, month, day == 0 ? 1 : day);
            }

            public AssemblyBuildDateAttribute(DateTime date) : base()
            {
                buildDate = date;
            }

            private DateTime buildDate;
            public DateTime Date { get { return buildDate; } }

        }

    }
#if PORTABLE
    }
#endif

}
