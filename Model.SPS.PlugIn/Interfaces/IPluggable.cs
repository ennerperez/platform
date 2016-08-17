using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model
{
#if PORTABLE
    namespace Core
    {
#endif

        namespace SPS
        {
            ///<summary>
            /// This interface is implemented by all plugIns and plugin based applications by default.
            /// Thay may be used to implement plugins and plugin based applications that does not defines any method.
            ///</summary>
            public interface IPluggable
            {
                //No properties or methods
            }
        }

#if PORTABLE
    }
#endif

}
