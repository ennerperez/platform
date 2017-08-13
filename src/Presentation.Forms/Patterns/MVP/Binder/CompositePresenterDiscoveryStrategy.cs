using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP.Binder
{
    public class CompositePresenterDiscoveryStrategy : IPresenterDiscoveryStrategy
    {
        private readonly IEnumerable<IPresenterDiscoveryStrategy> strategies;
        public CompositePresenterDiscoveryStrategy(params IPresenterDiscoveryStrategy[] strategies)
            : this((IEnumerable<IPresenterDiscoveryStrategy>)strategies)
        {
        }
        public CompositePresenterDiscoveryStrategy(IEnumerable<IPresenterDiscoveryStrategy> strategies)
        {
            if (strategies == null)
            {
                throw new ArgumentNullException("strategies");
            }
            this.strategies = strategies.ToArray<IPresenterDiscoveryStrategy>();
            if (!strategies.Any<IPresenterDiscoveryStrategy>())
            {
                throw new ArgumentException("You must supply at least one strategy.", "strategies");
            }
        }
        public PresenterDiscoveryResult GetBinding(IView viewInstance)
        {
            if (object.ReferenceEquals(viewInstance, null))
            {
                throw new ArgumentNullException("viewInstance");
            }
            List<PresenterDiscoveryResult> list = new List<PresenterDiscoveryResult>();
            foreach (IPresenterDiscoveryStrategy current in this.strategies)
            {
                PresenterDiscoveryResult binding = current.GetBinding(viewInstance);
                if (!object.ReferenceEquals(binding, null))
                {
                    list.Add(binding);
                }
            }
            return (
                from r in list
                group r by r.ViewInstances into r
                select CompositePresenterDiscoveryStrategy.BuildMergedResult(r.Key, r)).First<PresenterDiscoveryResult>();
        }
        private static PresenterDiscoveryResult BuildMergedResult(IEnumerable<IView> viewInstances, IEnumerable<PresenterDiscoveryResult> results)
        {
            IFormatProvider arg_48_0 = CultureInfo.InvariantCulture;
            string arg_48_1 = "CompositePresenterDiscoveryStrategy:\r\n\r\n{0}";
            object[] array = new object[1];
            array[0] = string.Join("\r\n\r\n", (
                from r in results
                select r.Message).ToArray<string>());
            return new PresenterDiscoveryResult(viewInstances, string.Format(arg_48_0, arg_48_1, array), results.SelectMany((PresenterDiscoveryResult r) => r.Bindings));
        }
    }
}
