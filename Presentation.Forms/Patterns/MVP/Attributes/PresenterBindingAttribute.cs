using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class PresenterBindingAttribute : Attribute
    {
        public Type PresenterType
        {
            get;
            private set;
        }
        public Type ViewType
        {
            get;
            set;
        }
        public BindingMode BindingMode
        {
            get;
            set;
        }
        public PresenterBindingAttribute(Type presenterType)
        {
            this.PresenterType = presenterType;
            this.ViewType = null;
            this.BindingMode = BindingMode.Default;
        }
    }
}
