using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP.Binder
{
    internal interface ICompositeViewTypeFactory
    {
        Type BuildCompositeViewType(Type viewType);
    }
}
