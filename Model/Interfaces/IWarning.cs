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
        delegate void OnWarning(EventArgs e);

        public interface IWarning
        {
            event EventHandler<EventArgs> Warning;
        }
#if PORTABLE
    }
#endif
}
