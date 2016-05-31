using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP
{
    public interface IView
    {
        event EventHandler Load;
        bool ThrowExceptionIfNoPresenterBound
        {
            get;
        }
    }
    public interface IView<TModel> : IView
    {
        TModel Model
        {
            get;
            set;
        }
    }

}
