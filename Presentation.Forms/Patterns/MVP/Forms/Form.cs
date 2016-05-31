using Presentation.Windows.Forms.Patterns.MVP.Binder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP
{
    public class Form<TModel> : System.Windows.Forms.Form, IView<TModel>, IView where TModel : class
    {
        private readonly PresenterBinder presenterBinder = new PresenterBinder();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TModel Model
        {
            get;
            set;
        }
        public bool ThrowExceptionIfNoPresenterBound
        {
            get;
            private set;
        }
        public Form()
        {
            this.presenterBinder.PerformBinding(this);
            this.ThrowExceptionIfNoPresenterBound = true;
        }
        //void add_Load(EventHandler value)
        //{
        //    base.Load += value;
        //}
        //void remove_Load(EventHandler value)
        //{
        //    base.Load -= value;
        //}
    }
}
