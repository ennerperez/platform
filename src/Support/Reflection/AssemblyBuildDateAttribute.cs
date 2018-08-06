using System;

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
                Date = DateTime.Now;
            }

            public AssemblyBuildDateAttribute(string date) : base()
            {
                Date = DateTime.Parse(date);
            }

            public AssemblyBuildDateAttribute(int year, int month, int day = 0) : base()
            {
                Date = new DateTime(year, month, day == 0 ? 1 : day);
            }

            public AssemblyBuildDateAttribute(DateTime date) : base()
            {
                Date = date;
            }

            public DateTime Date { get; internal set; }
        }
    }

#if PORTABLE
    }

#endif
}