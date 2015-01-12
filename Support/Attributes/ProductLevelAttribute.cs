using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support
{ 

    namespace Attributes.AssemblyProduct
    {

        [AttributeUsage(AttributeTargets.Assembly)]
        public class ProductLevelAttribute : global::System.Attribute
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
                get { return this.productlevel; }
            }

            public virtual int LevelNumber
            {
                get { return this.levelnumber; }
            }

        }

    }
}
