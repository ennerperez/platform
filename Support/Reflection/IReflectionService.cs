using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Support.Reflection
{

    public interface IReflectionService
    {
        IEnumerable<PropertyInfo> GetPublicInstanceProperties(Type mappedType);
        object GetMemberValue(object obj, Expression expr, MemberInfo member);
    }
}
