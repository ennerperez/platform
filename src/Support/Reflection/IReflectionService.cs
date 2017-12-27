using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

        namespace Reflection
        {
            public interface IReflectionService
            {
                IEnumerable<PropertyInfo> GetPublicInstanceProperties(Type mappedType);

                object GetMemberValue(object obj, Expression expr, MemberInfo member);
            }
        }

#if PORTABLE
    }

#endif
}