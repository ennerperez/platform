﻿using System;
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

}
