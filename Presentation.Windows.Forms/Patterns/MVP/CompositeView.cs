using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP
{
    public abstract class CompositeView<TView> : ICompositeView, IView where TView : class, IView
    {
        private readonly ICollection<TView> views = new List<TView>();
        public abstract event EventHandler Load;
        protected internal IEnumerable<TView> Views
        {
            get
            {
                return this.views;
            }
        }
        public bool ThrowExceptionIfNoPresenterBound
        {
            get
            {
                return true;
            }
        }
        public void Add(IView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (!(view is TView))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Expected a view of type {0} but {1} was supplied.", new object[]
				{
					typeof(TView).FullName,
					view.GetType().FullName
				}));
            }
            this.views.Add((TView)((object)view));
        }
    }
}
