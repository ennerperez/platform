using System;
using System.Collections.Generic;
using Platform.Support.Collections;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP.Binder
{
    public class PresenterDiscoveryResult
    {
        private readonly IEnumerable<IView> viewInstances;
        private readonly string message;
        private readonly IEnumerable<PresenterBinding> bindings;
        public IEnumerable<IView> ViewInstances
        {
            get
            {
                return this.viewInstances;
            }
        }
        public string Message
        {
            get
            {
                return this.message;
            }
        }
        public IEnumerable<PresenterBinding> Bindings
        {
            get
            {
                return this.bindings;
            }
        }
        public PresenterDiscoveryResult(IEnumerable<IView> viewInstances, string message, IEnumerable<PresenterBinding> bindings)
        {
            this.viewInstances = viewInstances;
            this.message = message;
            this.bindings = bindings;
        }
        public override bool Equals(object obj)
        {
            PresenterDiscoveryResult presenterDiscoveryResult = obj as PresenterDiscoveryResult;
            return presenterDiscoveryResult != null && (this.ViewInstances.SetEqual(presenterDiscoveryResult.ViewInstances) && this.Message.Equals(presenterDiscoveryResult.Message, StringComparison.OrdinalIgnoreCase)) && this.Bindings.SetEqual(presenterDiscoveryResult.Bindings);
        }
        public override int GetHashCode()
        {
            return this.ViewInstances.GetHashCode() | this.Message.GetHashCode() | this.Bindings.GetHashCode();
        }
    }
}
