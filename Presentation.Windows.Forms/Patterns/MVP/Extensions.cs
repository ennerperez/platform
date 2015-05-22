using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Support.Collections;

namespace Presentation.Windows.Forms.Patterns.MVP
{
    public static class Extensions
    {

        private static readonly IDictionary<RuntimeTypeHandle, IEnumerable<Type>> implementationTypeToViewInterfacesCache = new Dictionary<RuntimeTypeHandle, IEnumerable<Type>>();
        internal static IEnumerable<Type> GetViewInterfaces(this Type implementationType)
        {
            RuntimeTypeHandle typeHandle = implementationType.TypeHandle;
            return implementationTypeToViewInterfacesCache.GetOrCreateValue(typeHandle, () => implementationType.GetInterfaces().Where(new Func<Type, bool>(typeof(IView).IsAssignableFrom)).ToArray<Type>());
        }

    }
}
