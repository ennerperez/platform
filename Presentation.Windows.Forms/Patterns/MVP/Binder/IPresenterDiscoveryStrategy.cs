using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP.Binder
{
    public interface IPresenterDiscoveryStrategy
    {
        PresenterDiscoveryResult GetBinding(IView viewInstance);
    }
}
