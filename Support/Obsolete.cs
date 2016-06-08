using System;

namespace Platform.Support
{
#if PORTABLE
    namespace Core
    {
#endif

        namespace Attributes
        {

            [Obsolete("Use AssemblyCompanyIdAttribute instead")]
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
            public class IdAttribute : global::System.Attribute
            {
                public IdAttribute(string id)
                {
                }
            }

            [Obsolete("Use AssemblyBuildDateAttribute instead")]
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
            public class BuildDateAttribute : global::System.Attribute
            {
                public BuildDateAttribute(string date)
                {
                }
            }

            [Obsolete("Use AssemblyMadeInAttribute instead")]
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
            public class MadeInAttribute : global::System.Attribute
            {
                public MadeInAttribute(string name, string code = null)
                {
                }
            }

            [Obsolete("Use AssemblyDeveloperAttribute instead")]
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
            public class DeveloperAttribute : global::System.Attribute
            {
                public DeveloperAttribute(string name, object aditional = null)
                {
                }
            }

            [Obsolete("Use AssemblyCompanyUrlAttribute instead")]
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
            public class UrlAttribute : global::System.Attribute
            {
                public UrlAttribute(string url)
                {
                }
                public UrlAttribute(Uri uri)
                {
                }
            }

            [Obsolete("Use ContactInformationAttribute instead")]
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
            public class ContactAttribute : global::System.Attribute
            {
                public ContactAttribute(string description, string value)
                {
                }
                public ContactAttribute(string value)
                {
                }
            }

            [Obsolete("Use ContactInformationAttribute instead")]
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
            public class MailAttribute : global::System.Attribute
            {
                public MailAttribute(string email)
                {
                }
            }

            [Obsolete("Use AssemblyExternalRefAttribute instead")]
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
            public class ExternalRefAttribute : global::System.Attribute
            {
                public ExternalRefAttribute(string description, string filename, string arguments = "", object optional = null)
                {
                }
            }

            [Obsolete("Use AssemblyProductLevelAttribute instead")]
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
            public class ProductLevelAttribute : global::System.Attribute
            {
                public ProductLevelAttribute(ProductLevels level, int number = 1)
                {
                }

            }

            [Obsolete("Use AssemblyThirdPartyAttribute instead")]
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
            public class ThirdPartyAttribute : global::System.Attribute
            {
                public ThirdPartyAttribute(string name, string info)
                {
                }
            }


        }

        public static partial class Extensions
        {

            [Obsolete("Avoid using RNull")]
            public static string RNull(object value, string replacement = "")
            {
#if PORTABLE
                return Platform.Support.Core.IsNull.Cast(value, replacement);
#else
                return Platform.Support.IsNull.Cast(value, replacement);
#endif
            }
        }

#if PORTABLE
    }
#endif

}
