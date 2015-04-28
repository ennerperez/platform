using Presentation.Windows.Forms.Patterns.MVP.Attributes;
using System;
using System.Collections.Generic;
using Support.Collections;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP.Binder
{
    public class AttributeBasedPresenterDiscoveryStrategy : IPresenterDiscoveryStrategy
    {
        private static readonly IDictionary<RuntimeTypeHandle, IEnumerable<PresenterBindingAttribute>> typeToAttributeCache = new Dictionary<RuntimeTypeHandle, IEnumerable<PresenterBindingAttribute>>();
        public PresenterDiscoveryResult GetBinding(IView viewInstance)
        {
            if (viewInstance == null)
            {
                throw new ArgumentNullException("viewInstance");
            }
            List<string> list = new List<string>();
            List<PresenterBinding> list2 = new List<PresenterBinding>();
            Type type = viewInstance.GetType();
            IEnumerable<PresenterBindingAttribute> attributes = AttributeBasedPresenterDiscoveryStrategy.GetAttributes(AttributeBasedPresenterDiscoveryStrategy.typeToAttributeCache, type);
            if (attributes.Empty<PresenterBindingAttribute>())
            {
                list.Add(string.Format(CultureInfo.InvariantCulture, "could not find a [PresenterBinding] attribute on view instance {0}", new object[]
				{
					type.FullName
				}));
            }
            if (attributes.Any<PresenterBindingAttribute>())
            {
                using (IEnumerator<PresenterBindingAttribute> enumerator = (
                    from a in attributes
                    orderby a.PresenterType.Name
                    select a).GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        PresenterBindingAttribute current = enumerator.Current;
                        if (!current.ViewType.IsAssignableFrom(type))
                        {
                            list.Add(string.Format(CultureInfo.InvariantCulture, "found, but ignored, a [PresenterBinding] attribute on view instance {0} (presenter type: {1}, view type: {2}, binding mode: {3}) because the view type on the attribute is not compatible with the type of the view instance", new object[]
							{
								type.FullName,
								current.PresenterType.FullName,
								current.ViewType.FullName,
								current.BindingMode
							}));
                        }
                        else
                        {
                            list.Add(string.Format(CultureInfo.InvariantCulture, "found a [PresenterBinding] attribute on view instance {0} (presenter type: {1}, view type: {2}, binding mode: {3})", new object[]
							{
								type.FullName,
								current.PresenterType.FullName,
								current.ViewType.FullName,
								current.BindingMode
							}));
                            list2.Add(new PresenterBinding(current.PresenterType, current.ViewType, current.BindingMode, viewInstance));
                        }
                    }
                    goto IL_1A1;
                }
                goto IL_19F;
            IL_1A1:
                return new PresenterDiscoveryResult(new IView[]
				{
					list2.Single<PresenterBinding>().ViewInstance
				}, "AttributeBasedPresenterDiscoveryStrategy:\r\n" + string.Join("\r\n", (
                    from m in list
                    select "- " + m).ToArray<string>()), list2);
            }
        IL_19F:
            return null;
        }
        private static IEnumerable<IView> GetViewInstancesToBind(IEnumerable<IView> pendingViewInstances, IView viewInstance, Type viewType, ICollection<string> messages, PresenterBindingAttribute attribute)
        {
            IEnumerable<IView> enumerable;
            switch (attribute.BindingMode)
            {
                case BindingMode.Default:
                    enumerable = new IView[]
				{
					viewInstance
				};
                    break;
                case BindingMode.SharedPresenter:
                    enumerable = (
                        from v in pendingViewInstances
                        where attribute.ViewType.IsAssignableFrom(viewType)
                        select v).ToArray<IView>();
                    messages.Add(string.Format(CultureInfo.InvariantCulture, "including {0} more view instances in the binding because the binding mode is {1} and they are compatible with the view type {2}", new object[]
				{
					enumerable.Count<IView>() - 1,
					attribute.BindingMode,
					attribute.ViewType.FullName
				}));
                    break;
                default:
                    throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Binding mode {0} is not supported", new object[]
				{
					attribute.BindingMode
				}));
            }
            return enumerable;
        }
        internal static IEnumerable<PresenterBindingAttribute> GetAttributes(IDictionary<RuntimeTypeHandle, IEnumerable<PresenterBindingAttribute>> cache, Type sourceType)
        {
            RuntimeTypeHandle typeHandle = sourceType.TypeHandle;
            return cache.GetOrCreateValue(typeHandle, delegate
            {
                PresenterBindingAttribute[] source = sourceType.GetCustomAttributes(typeof(PresenterBindingAttribute), true).OfType<PresenterBindingAttribute>().ToArray<PresenterBindingAttribute>();
                if (source.Any((PresenterBindingAttribute a) => a.BindingMode == BindingMode.SharedPresenter && a.ViewType == null))
                {
                    throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "When a {1} is applied with BindingMode={2}, the ViewType must be explicitly specified. One of the bindings on {0} violates this restriction.", new object[]
					{
						sourceType.FullName,
						typeof(PresenterBindingAttribute).Name,
						Enum.GetName(typeof(BindingMode), BindingMode.SharedPresenter)
					}));
                }
                return (
                    from pba in source
                    select new PresenterBindingAttribute(pba.PresenterType)
                    {
                        ViewType = pba.ViewType ?? sourceType,
                        BindingMode = pba.BindingMode
                    }).ToArray<PresenterBindingAttribute>();
            });
        }
    }

}
