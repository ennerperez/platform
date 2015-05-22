using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Platform.Support;

namespace Platform.Support.Reflection
{
    public static class Extensions
    {

        #region  ReflectionService

        public static IEnumerable<PropertyInfo> GetPublicInstanceProperties(this Type typ)
        {
            ReflectionService _return = new ReflectionService();
            return _return.GetPublicInstanceProperties(typ);
        }
        public static IEnumerable<PropertyInfo> GetNonPublicInstanceProperties(this Type typ)
        {
            ReflectionService _return = new ReflectionService();
            return _return.GetNonPublicInstanceProperties(typ);
        }
        public static IEnumerable<PropertyInfo> GetStaticInstanceProperties(this Type typ)
        {
            ReflectionService _return = new ReflectionService();
            return _return.GetStaticInstanceProperties(typ);
        }
        public static IEnumerable<PropertyInfo> GetInstanceProperties(this Type typ)
        {
            ReflectionService _return = new ReflectionService();
            return _return.GetInstanceProperties(typ);
        }

        public static object GetMemberValue(this Type typ, Expression expr, MemberInfo member)
        {
            ReflectionService _return = new ReflectionService();
            return _return.GetMemberValue(typ, expr, member);
        }

        #endregion

        #region Generics

        public static T Clone<T>(this T item) where T : ICloneable
        {
            return (T)item.Clone();
        }

        #endregion

        public static string GetNameSafe(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }
            return new AssemblyName(assembly.FullName).Name;
        }

        public static string GetDirectory(this Assembly assembly)
        {
            if (assembly == null) assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string path = System.IO.Path.GetDirectoryName(assembly.GetName().CodeBase);
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            return file.Directory.FullName;
        }

    }
}
