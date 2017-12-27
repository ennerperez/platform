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
                File = filename;
                Description = description;
                Arguments = arguments;
                Optional = optional;
            }

            public string File { get; internal set; }
            public string Description { get; internal set; }
            public string Arguments { get; internal set; }
            public object Optional { get; internal set; }

            public override string ToString()
            {
                return $"{File}{(!string.IsNullOrEmpty(Arguments) ? " " + Arguments : "")}";
            }
        }
    }

#if PORTABLE
    }

#endif
}