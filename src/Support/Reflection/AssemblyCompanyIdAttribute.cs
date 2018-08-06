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
        /// Company Id, usually tax payer id, or a DNI.
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class AssemblyCompanyIdAttribute : global::System.Attribute
        {
            public AssemblyCompanyIdAttribute(string id) : base()
            {
                Id = id;
            }

            public string Id { get; internal set; }
        }
    }

#if PORTABLE
    }

#endif
}