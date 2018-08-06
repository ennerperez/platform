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
        /// Main assembly Owner (Only if differ from Company or Developers)
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
        public sealed class AssemblyOwnerAttribute : global::System.Attribute
        {
            public AssemblyOwnerAttribute(string name) : base()
            {
                Name = name;
            }

            public string Name { get; internal set; }

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