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
            /// Developer information
            /// </summary>
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
            public sealed class AssemblyDeveloperAttribute : global::System.Attribute
            {
                public AssemblyDeveloperAttribute(string name, object info = null) : base()
                {
                    this.name = name;
                    this.info = info;
                }

                private string name;
                public string Name { get { return name; } }
                private object info;
                public string Info { get { return info.ToString(); } }
            }
        }

#if PORTABLE
    }

#endif
}