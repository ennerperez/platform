using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP.Binder
{
    public class PresenterCreatedEventArgs : EventArgs
    {
        private readonly IPresenter presenter;
        public IPresenter Presenter
        {
            get
            {
                return this.presenter;
            }
        }
        public PresenterCreatedEventArgs(IPresenter presenter)
        {
            this.presenter = presenter;
        }
    }
}
