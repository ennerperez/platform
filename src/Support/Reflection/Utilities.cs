using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

        namespace Reflection
        {
            public static partial class Utilities
            {
#if !PORTABLE

            /// <summary>
            /// Gets an attribute from a MemberInfo object.
            /// </summary>
            /// <typeparam name="T">Type of searched Attribute</typeparam>
            /// <param name="memberInfo">MemberInfo object</param>
            /// <returns>Specified Attribute, if found, else null</returns>
            public static T GetAttribute<T>(MemberInfo memberInfo) where T : class
            {
                var attributes = memberInfo.GetCustomAttributes(typeof(T), true);
                if (attributes.Length <= 0)
                {
                    return null;
                }

                return attributes[0] as T;
            }

#else

                /// <summary>
                /// Gets an attribute from a MemberInfo object.
                /// </summary>
                /// <typeparam name="T">Type of searched Attribute</typeparam>
                /// <param name="memberInfo">MemberInfo object</param>
                /// <returns>Specified Attribute, if found, else null</returns>
                public static T GetAttribute<T>(Type memberInfo) where T : class
                {
#if PROFILE_78
                    var info = memberInfo.GetTypeInfo();
                    var attributes = info.GetCustomAttributes(typeof(T), true);
#else
                    var attributes = memberInfo.GetCustomAttributes(typeof(T), true);
#endif
                    if (attributes.Count() <= 0)
                        return null;

                    return attributes.FirstOrDefault() as T;
                }

#endif
            }
        }

#if PORTABLE
    }

#endif
}