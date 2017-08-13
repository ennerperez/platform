using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Platform.Support.Reflection
{
    public class ReflectionService : IReflectionService
    {
        public IEnumerable<PropertyInfo> GetPublicInstanceProperties(Type mappedType)
        {
            return mappedType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
        }

        public IEnumerable<PropertyInfo> GetNonPublicInstanceProperties(Type mappedType)
        {
            return mappedType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty);
        }

        public IEnumerable<PropertyInfo> GetStaticInstanceProperties(Type mappedType)
        {
            return mappedType.GetProperties(BindingFlags.Static | BindingFlags.Instance | BindingFlags.SetProperty);
        }

        public IEnumerable<PropertyInfo> GetInstanceProperties(Type mappedType)
        {
            return mappedType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty);
        }

        public object GetMemberValue(object obj, Expression expr, MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                PropertyInfo m = (PropertyInfo)member;
                return m.GetValue(obj, null);
            }
            if (member.MemberType == MemberTypes.Field)
            {
                FieldInfo m = (FieldInfo)member;
                return m.GetValue(obj);
            }
            throw new NotSupportedException("MemberExpr: " + member.MemberType);
        }
    }
}