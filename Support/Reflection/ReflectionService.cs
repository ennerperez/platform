using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Support.Reflection
{

    public class ReflectionService : IReflectionService
    {
        public IEnumerable<PropertyInfo> GetPublicInstanceProperties(Type mappedType)
        {
            return mappedType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
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
