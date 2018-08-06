using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP
{
    public interface ICompositeView : IView
    {
        void Add(IView view);
    }
}
