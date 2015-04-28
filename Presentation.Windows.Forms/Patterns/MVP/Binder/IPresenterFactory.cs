using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP.Binder
{
    public interface IPresenterFactory
    {
        IPresenter Create(Type presenterType, Type viewType, IView viewInstance);
        void Release(IPresenter presenter);
    }
}
