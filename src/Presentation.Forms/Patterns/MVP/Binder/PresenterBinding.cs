using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP.Binder
{
    public class PresenterBinding
    {
        private readonly Type presenterType;
        private readonly Type viewType;
        private readonly BindingMode bindingMode;
        private readonly IView viewInstance;
        public Type PresenterType
        {
            get
            {
                return this.presenterType;
            }
        }
        public Type ViewType
        {
            get
            {
                return this.viewType;
            }
        }
        public BindingMode BindingMode
        {
            get
            {
                return this.bindingMode;
            }
        }
        public IView ViewInstance
        {
            get
            {
                return this.viewInstance;
            }
        }
        public PresenterBinding(Type presenterType, Type viewType, BindingMode bindingMode, IView viewInstance)
        {
            this.presenterType = presenterType;
            this.viewType = viewType;
            this.bindingMode = bindingMode;
            this.viewInstance = viewInstance;
        }
        public override bool Equals(object obj)
        {
            PresenterBinding presenterBinding = obj as PresenterBinding;
            return presenterBinding != null && (this.PresenterType == presenterBinding.PresenterType && this.ViewType == presenterBinding.ViewType && this.BindingMode == presenterBinding.BindingMode) && this.ViewInstance.Equals(presenterBinding.ViewInstance);
        }
        public override int GetHashCode()
        {
            return this.PresenterType.GetHashCode() | this.ViewType.GetHashCode() | this.BindingMode.GetHashCode() | this.ViewInstance.GetHashCode();
        }
    }
}
