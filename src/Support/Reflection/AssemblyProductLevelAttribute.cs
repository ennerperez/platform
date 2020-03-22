using System;

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
                Level = level;
                Number = number;
            }

            public ProductLevels Level { get; internal set; }
            public short Number { get; internal set; }

            public override string ToString()
            {
#if NETFX_40
                return Enum.GetName(typeof(ProductLevels), Level) + " " + Number.ToString();
#else
                return $"{Enum.GetName(typeof(ProductLevels), Level)} {Number.ToString()}";
#endif
            }
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