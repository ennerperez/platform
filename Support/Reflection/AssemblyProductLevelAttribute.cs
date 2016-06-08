using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
#if PORTABLE
    namespace Core
    {
#endif

    namespace Reflection
    {

        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class AssemblyProductLevelAttribute : global::System.Attribute
        {

            public AssemblyProductLevelAttribute(ProductLevels level, short number = 1)
            {
                this.level = level;
                this.number = number;
            }

            private ProductLevels level;
            public ProductLevels Level { get { return level; } }

            private short number;
            public short Number { get { return number; } }

        }

    }

    /// <summary>
    /// Product type levels
    /// </summary>
    /// <remarks></remarks>
    public enum ProductLevels : short
    {
        Milestone = -3,
        Alpha = -2,
        Beta = -1,
        Preview = -1,
        RC = 0,
        Release = 1,
        RTM = 1,
        RTW = 1,
        GA = 1
    }

#if PORTABLE
    }
#endif

}