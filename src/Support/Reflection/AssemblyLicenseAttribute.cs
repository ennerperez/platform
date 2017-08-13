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
                    this.name = name;
                    if (uri != null)
                        this.url = url.ToString();
                }

                public AssemblyLicenseAttribute(string name, string url = null) : base()
                {
                    this.name = name;
                    this.url = url;
                }

                private string name;
                public string Name { get { return name; } }

                private string url;
                public string Url { get { return url; } }
            }
        }

#if PORTABLE
    }

#endif
}