using System;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

        namespace Reflection
        {
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
            public sealed class AssemblyExternalRefAttribute : global::System.Attribute
            {
                public AssemblyExternalRefAttribute(string description, string filename, string arguments = "", object optional = null)
                {
                    this.filename = filename;
                    this.description = description;
                    this.arguments = arguments;
                    this.optional = optional;
                }

                private string filename;
                public string FileName { get { return this.filename; } }
                private string description;
                public string Description { get { return this.description; } }
                private string arguments;
                public string Arguments { get { return this.arguments; } }
                private object optional;
                public object Optional { get { return this.optional; } }
            }
        }

#if PORTABLE
    }

#endif
}