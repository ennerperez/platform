using System;
using System.Collections.Generic;
using System.Globalization;
using Support;
using Support.Reflection;
using Support.Collections;
using System.Linq;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP.Binder
{
    public class ConventionBasedPresenterDiscoveryStrategy : IPresenterDiscoveryStrategy
    {
        private class ConventionSearchResult
        {
            private readonly string message;
            private readonly Type presenterType;
            public string Message
            {
                get
                {
                    return this.message;
                }
            }
            public Type PresenterType
            {
                get
                {
                    return this.presenterType;
                }
            }
            public ConventionSearchResult(string message, Type presenterType)
            {
                this.message = message;
                this.presenterType = presenterType;
            }
        }
        private static readonly IEnumerable<string> defaultViewInstanceSuffixes = new string[]
		{
			"UserControl",
			"Control",
			"View",
			"Form"
		};
        private static readonly IEnumerable<string> defaultCandidatePresenterTypeFullNameFormats = new string[]
		{
			"{namespace}.Logic.Presenters.{presenter}",
			"{namespace}.Presenters.{presenter}",
			"{namespace}.Logic.{presenter}",
			"{namespace}.{presenter}"
		};
        private static readonly IDictionary<RuntimeTypeHandle, ConventionBasedPresenterDiscoveryStrategy.ConventionSearchResult> viewTypeToPresenterTypeCache = new Dictionary<RuntimeTypeHandle, ConventionBasedPresenterDiscoveryStrategy.ConventionSearchResult>();
        protected virtual IEnumerable<string> ViewInstanceSuffixes
        {
            get
            {
                return ConventionBasedPresenterDiscoveryStrategy.defaultViewInstanceSuffixes;
            }
        }
        public virtual IEnumerable<string> CandidatePresenterTypeFullNameFormats
        {
            get
            {
                return ConventionBasedPresenterDiscoveryStrategy.defaultCandidatePresenterTypeFullNameFormats;
            }
        }
        public virtual PresenterDiscoveryResult GetBinding(IView viewInstance)
        {
            if (viewInstance == null)
            {
                throw new ArgumentNullException("viewInstance");
            }
            return ConventionBasedPresenterDiscoveryStrategy.GetBinding(viewInstance, this.ViewInstanceSuffixes, this.CandidatePresenterTypeFullNameFormats);
        }
        internal static PresenterDiscoveryResult GetBinding(IView viewInstance, IEnumerable<string> viewInstanceSuffixes, IEnumerable<string> presenterTypeFullNameFormats)
        {
            Type type = viewInstance.GetType();
            ConventionBasedPresenterDiscoveryStrategy.ConventionSearchResult orCreateValue = ConventionBasedPresenterDiscoveryStrategy.viewTypeToPresenterTypeCache.GetOrCreateValue(type.TypeHandle, () => ConventionBasedPresenterDiscoveryStrategy.PerformSearch(viewInstance, viewInstanceSuffixes, presenterTypeFullNameFormats));
            return new PresenterDiscoveryResult(new IView[]
			{
				viewInstance
			}, orCreateValue.Message, (orCreateValue.PresenterType == null) ? new PresenterBinding[0] : new PresenterBinding[]
			{
				new PresenterBinding(orCreateValue.PresenterType, type, BindingMode.Default, viewInstance)
			});
        }
        private static ConventionBasedPresenterDiscoveryStrategy.ConventionSearchResult PerformSearch(IView viewInstance, IEnumerable<string> viewInstanceSuffixes, IEnumerable<string> presenterTypeFullNameFormats)
        {
            Type type = viewInstance.GetType();
            Type type2 = null;
            List<string> list = new List<string>
			{
				ConventionBasedPresenterDiscoveryStrategy.GetPresenterTypeNameFromViewTypeName(type, viewInstanceSuffixes)
			};
            list.AddRange(ConventionBasedPresenterDiscoveryStrategy.GetPresenterTypeNamesFromViewInterfaceTypeNames(type.GetViewInterfaces()));
            IEnumerable<string> source = ConventionBasedPresenterDiscoveryStrategy.GenerateCandidatePresenterTypeFullNames(type, list, presenterTypeFullNameFormats);
            List<string> list2 = new List<string>();
            foreach (string current in source.Distinct<string>())
            {
                type2 = type.Assembly.GetType(current);
                if (type2 == null)
                {
                    list2.Add(string.Format(CultureInfo.InvariantCulture, "could not find a presenter with type name {0}", new object[]
					{
						current
					}));
                }
                else
                {
                    if (typeof(IPresenter).IsAssignableFrom(type2))
                    {
                        list2.Add(string.Format(CultureInfo.InvariantCulture, "found presenter with type name {0}", new object[]
						{
							current
						}));
                        break;
                    }
                    list2.Add(string.Format(CultureInfo.InvariantCulture, "found, but ignored, potential presenter with type name {0} because it does not implement IPresenter", new object[]
					{
						current
					}));
                    type2 = null;
                }
            }
            return new ConventionBasedPresenterDiscoveryStrategy.ConventionSearchResult("ConventionBasedPresenterDiscoveryStrategy:\r\n" + string.Join("\r\n", (
                from m in list2
                select "- " + m).ToArray<string>()), type2);
        }
        internal static IEnumerable<string> GetPresenterTypeNamesFromViewInterfaceTypeNames(IEnumerable<Type> viewInterfaces)
        {
            return
                from i in viewInterfaces
                where i.Name != "IView" && i.Name != "IView`1"
                select i.Name.TrimStart(new char[]
				{
					'I'
				}).TrimFromEnd("View");
        }
        internal static string GetPresenterTypeNameFromViewTypeName(Type viewType, IEnumerable<string> viewInstanceSuffixes)
        {
            string text = (
                from suffix in viewInstanceSuffixes
                where viewType.Name.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)
                select viewType.Name.TrimFromEnd(suffix)).FirstOrDefault<string>();
            return (string.IsNullOrEmpty(text) ? viewType.Name : text) + "Presenter";
        }
        private static IEnumerable<string> GenerateCandidatePresenterTypeFullNames(Type viewType, IEnumerable<string> presenterTypeNames, IEnumerable<string> presenterTypeFullNameFormats)
        {
            string nameSafe = viewType.Assembly.GetNameSafe();
            foreach (string current in presenterTypeNames)
            {
                yield return viewType.Namespace + "." + current;
                foreach (string current2 in presenterTypeFullNameFormats)
                {
                    yield return current2.Replace("{namespace}", nameSafe).Replace("{presenter}", current);
                }
            }
            yield break;
        }
    }
}
