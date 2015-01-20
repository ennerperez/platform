using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Support;

namespace Support.Reflection
{
    public static class Extensions
    {

        #region  ReflectionService 
                
        public static IEnumerable<PropertyInfo> GetPublicInstanceProperties(this Type typ)
        {
            ReflectionService _return = new ReflectionService();
            return _return.GetPublicInstanceProperties(typ);
        }

        public static object GetMemberValue(this Type typ, Expression expr, MemberInfo member)
        {
            ReflectionService _return = new ReflectionService();
            return _return.GetMemberValue(typ, expr, member);
        }

        #endregion

    }
}
