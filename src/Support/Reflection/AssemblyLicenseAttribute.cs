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
        /// Assembly license
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
        public sealed class AssemblyLicenseAttribute : global::System.Attribute
        {
            public AssemblyLicenseAttribute(string name, Uri uri) : base()
            {
                Name = name;
                if (uri != null)
                    Url = uri.ToString();
            }

            public AssemblyLicenseAttribute(string name, string url = null) : base()
            {
                Name = name;
                Url = url;
            }

            public string Name { get; internal set; }

            public string Url { get; internal set; }
        }
    }

#if PORTABLE
    }

#endif
}