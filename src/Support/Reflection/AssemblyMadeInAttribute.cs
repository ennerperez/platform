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
        /// Assembly country made in information
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
        public sealed class AssemblyMadeInAttribute : global::System.Attribute
        {
            public AssemblyMadeInAttribute(string name, string code = null) : base()
            {
                Name = name;
                if (code.Length != 2)
                    throw new InvalidCastException("Can't convert a string into a ISO3166 Country Code");
                CountryCode = code.ToUpper();
            }

            public string Name { get; internal set; }
            public string CountryCode { get; internal set; }
        }
    }

#if PORTABLE
    }

#endif
}