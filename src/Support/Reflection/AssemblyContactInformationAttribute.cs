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
        /// Contact information
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
        public sealed class AssemblyContactInformationAttribute : global::System.Attribute
        {
            public AssemblyContactInformationAttribute(string description, string value)
            {
                Description = description;
                Value = value;
            }

            public AssemblyContactInformationAttribute(string value)
            {
                if (value.IsNumeric()) { Description = "number"; }
                if (value.IsEmail()) { Description = "email"; }
                Value = value;
            }

            public string Description { get; internal set; }
            public string Value { get; internal set; }

            public string[] Contact
            {
                get { return new string[] { Description, Value }; }
            }

            public override string ToString()
            {
                return $"{Description}: {Value}";
            }
        }
    }

#if PORTABLE
    }

#endif
}