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
        public sealed class AssemblyThirdPartyAttribute : global::System.Attribute
        {
            public AssemblyThirdPartyAttribute(string name, string info = null)
            {
                Name = name;
                Info = info;
            }

            public string Name { get; internal set; }

            public string Info { get; internal set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }

#if PORTABLE
    }

#endif
}