using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP
{
    public interface IPresenter
    {
    }
    public interface IPresenter<out TView> : IPresenter where TView : class, IView
    {
        TView View
        {
            get;
        }
    }

}
