using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
#if !CORE
    namespace Core
    {
#endif

        namespace Attributes
        {

            [AttributeUsage(AttributeTargets.Assembly)]
#if !CORE
            internal class ProductLevelAttribute : global::System.Attribute
#else
        public class ProductLevelAttribute : global::System.Attribute
#endif
            {

                private ProductLevels productlevel;
                private int levelnumber;

                public ProductLevelAttribute(ProductLevels level, int number = 1)
                {
                    productlevel = level;
                    levelnumber = number;
                }

                public virtual ProductLevels ProductLevel
                {
                    get { return productlevel; }
                }

                public virtual int LevelNumber
                {
                    get { return levelnumber; }
                }

            }

        }

        /// <summary>
        /// Product type levels
        /// </summary>
        /// <remarks></remarks>
        public enum ProductLevels
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

#if !CORE
    }
#endif

}