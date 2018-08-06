using System;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

    namespace Reflection
    {
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class AssemblyCompanyUrlAttribute : global::System.Attribute
        {
            public AssemblyCompanyUrlAttribute(Uri uri)
            {
                Url = uri.ToString();
            }

            public AssemblyCompanyUrlAttribute(string url)
            {
                Url = url;
            }

            public string Url { get; internal set; }
        }
    }

#if PORTABLE
    }

#endif
}