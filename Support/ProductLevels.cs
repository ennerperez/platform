using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support
{
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
