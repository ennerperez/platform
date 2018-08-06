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
                Name = name;
                Info = info.ToString();
            }

            public string Name { get; internal set; }

            public string Info { get; internal set; }

            public override string ToString()
            {
                return $"{Name}";
            }
        }
    }

#if PORTABLE
    }

#endif
}