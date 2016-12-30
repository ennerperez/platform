using System;
using System.Linq;
using System.Reflection;
#if PORTABLE
using Platform.Support.Core.Collections;
#else
using Platform.Support.Collections;
#endif

namespace Platform.Support
{
#if PORTABLE
    namespace Core
    {
#endif
    public static class AttributeHelper
    {

        #region Assembly

#if (!PORTABLE)
        internal static Assembly _assembly = Assembly.GetEntryAssembly();
#elif NETFX_45
        internal static Assembly _assembly = typeof(AttributeHelper).GetTypeInfo().Assembly;
#else
            internal static Assembly _assembly = Assembly.GetCallingAssembly();
#endif

        #endregion

        public static T GetAttribute<T>(Assembly assembly = null) where T : System.Attribute
        {
            return (T)GetAttribute(typeof(T), assembly);
        }
        public static T[] GetAttributes<T>(Assembly assembly = null) where T : System.Attribute
        {
            return (T[])GetAttributes(typeof(T), assembly);
        }

        public static object GetAttribute(Type AttributeType, Assembly assembly = null)
        {
            var result = GetAttributes(AttributeType, assembly);
            if (result != null) return result.FirstOrDefault();
            return null;
        }
        public static object[] GetAttributes(Type AttributeType, Assembly assembly = null)
        {
            if (assembly == null) { assembly = _assembly; }

#if NETFX_45
            var customAttributes = assembly.GetCustomAttributes(AttributeType);
            if (customAttributes.Count() == 0)
                return null;

            return customAttributes.ToArray();

#else
            object[] customAttributes = assembly.GetCustomAttributes(AttributeType, true);
            if (customAttributes.Length == 0)
                return null;

            return customAttributes;
#endif
        }

    }
#if PORTABLE
    }
#endif
}
