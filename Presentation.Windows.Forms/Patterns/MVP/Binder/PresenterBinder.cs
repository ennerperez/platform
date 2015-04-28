using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Support.Collections;
using System.Globalization;

namespace Presentation.Windows.Forms.Patterns.MVP.Binder
{
    public sealed class PresenterBinder
    {
        private static IPresenterFactory factory;
        private static IPresenterDiscoveryStrategy discoveryStrategy;
        public event EventHandler<PresenterCreatedEventArgs> PresenterCreated;
        public static IPresenterFactory Factory
        {
            get
            {
                IPresenterFactory arg_14_0;
                if ((arg_14_0 = PresenterBinder.factory) == null)
                {
                    arg_14_0 = (PresenterBinder.factory = new DefaultPresenterFactory());
                }
                return arg_14_0;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (PresenterBinder.factory != null)
                {
                    throw new InvalidOperationException((PresenterBinder.factory is DefaultPresenterFactory) ? "The factory has already been set, and can be not changed at a later time. In this case, it has been set to the default implementation. This happens if the factory is used before being explicitly set. If you wanted to supply your own factory, you need to do this in your Application_Start event." : "You can only set your factory once, and should really do this in Application_Start.");
                }
                PresenterBinder.factory = value;
            }
        }
        public static IPresenterDiscoveryStrategy DiscoveryStrategy
        {
            get
            {
                IPresenterDiscoveryStrategy arg_2C_0;
                if ((arg_2C_0 = PresenterBinder.discoveryStrategy) == null)
                {
                    arg_2C_0 = (PresenterBinder.discoveryStrategy = new CompositePresenterDiscoveryStrategy(new IPresenterDiscoveryStrategy[]
					{
						new AttributeBasedPresenterDiscoveryStrategy(),
						new ConventionBasedPresenterDiscoveryStrategy()
					}));
                }
                return arg_2C_0;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                PresenterBinder.discoveryStrategy = value;
            }
        }
        public void PerformBinding(IView viewInstance)
        {
            try
            {
                PresenterBinder.PerformBinding(viewInstance, PresenterBinder.DiscoveryStrategy, delegate(IPresenter p)
                {
                    this.OnPresenterCreated(new PresenterCreatedEventArgs(p));
                }, PresenterBinder.Factory);
            }
            catch (Exception)
            {
            }
        }
        private void OnPresenterCreated(PresenterCreatedEventArgs args)
        {
            if (this.PresenterCreated != null)
            {
                this.PresenterCreated(this, args);
            }
        }
        private static IPresenter PerformBinding(IView candidate, IPresenterDiscoveryStrategy presenterDiscoveryStrategy, Action<IPresenter> presenterCreatedCallback, IPresenterFactory presenterFactory)
        {
            PresenterBinding bindings = PresenterBinder.GetBindings(candidate, presenterDiscoveryStrategy);
            return PresenterBinder.BuildPresenter(presenterCreatedCallback, presenterFactory, new PresenterBinding[]
			{
				bindings
			});
        }
        private static PresenterBinding GetBindings(IView candidate, IPresenterDiscoveryStrategy presenterDiscoveryStrategy)
        {
            PresenterDiscoveryResult binding = presenterDiscoveryStrategy.GetBinding(candidate);
            PresenterBinder.ThrowExceptionsForViewsWithNoPresenterBound(binding);
            return binding.Bindings.Single<PresenterBinding>();
        }
        private static void ThrowExceptionsForViewsWithNoPresenterBound(PresenterDiscoveryResult result)
        {
            if (result.Bindings.Empty<PresenterBinding>())
            {
                if ((
                    from v in result.ViewInstances
                    where v.ThrowExceptionIfNoPresenterBound
                    select v).Any<IView>())
                {
                    IFormatProvider arg_94_0 = CultureInfo.InvariantCulture;
                    string arg_94_1 = "Failed to find presenter for view instance of {0}.{1} If you do not want this exception to be thrown, set ThrowExceptionIfNoPresenterBound to false on your view.";
                    object[] array = new object[2];
                    array[0] = (
                        from v in result.ViewInstances
                        where v.ThrowExceptionIfNoPresenterBound
                        select v).Single<IView>().GetType().FullName;
                    array[1] = result.Message;
                    throw new InvalidOperationException(string.Format(arg_94_0, arg_94_1, array));
                }
            }
        }
        private static IPresenter BuildPresenter(Action<IPresenter> presenterCreatedCallback, IPresenterFactory presenterFactory, IEnumerable<PresenterBinding> bindings)
        {
            return (
                from binding in bindings
                select PresenterBinder.BuildPresenters(presenterCreatedCallback, presenterFactory, binding)).ToList<IPresenter>().First<IPresenter>();
        }
        private static IPresenter BuildPresenters(Action<IPresenter> presenterCreatedCallback, IPresenterFactory presenterFactory, PresenterBinding binding)
        {
            IView viewInstance = binding.ViewInstance;
            return PresenterBinder.BuildPresenter(presenterCreatedCallback, presenterFactory, binding, viewInstance);
        }
        private static IPresenter BuildPresenter(Action<IPresenter> presenterCreatedCallback, IPresenterFactory presenterFactory, PresenterBinding binding, IView viewInstance)
        {
            IPresenter presenter = presenterFactory.Create(binding.PresenterType, binding.ViewType, viewInstance);
            if (presenterCreatedCallback != null)
            {
                presenterCreatedCallback(presenter);
            }
            return presenter;
        }
    }
}
