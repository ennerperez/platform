using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP
{
    public abstract class Presenter<TView> : IPresenter<TView>, IPresenter where TView : class, IView
    {
        private readonly TView view;
        public TView View
        {
            get
            {
                return this.view;
            }
        }
        protected Presenter(TView view)
        {
            this.view = view;
        }
    }

}
