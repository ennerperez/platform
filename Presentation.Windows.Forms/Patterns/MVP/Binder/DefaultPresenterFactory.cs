using System;
using System.Collections.Generic;
using Support.Collections;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP.Binder
{
    public class DefaultPresenterFactory : IPresenterFactory
    {
        private static readonly IDictionary<string, DynamicMethod> buildMethodCache = new Dictionary<string, DynamicMethod>();
        public IPresenter Create(Type presenterType, Type viewType, IView viewInstance)
        {
            if (presenterType == null)
            {
                throw new ArgumentNullException("presenterType");
            }
            if (viewType == null)
            {
                throw new ArgumentNullException("viewType");
            }
            if (viewInstance == null)
            {
                throw new ArgumentNullException("viewInstance");
            }
            DynamicMethod buildMethod = DefaultPresenterFactory.GetBuildMethod(presenterType, viewType);
            IPresenter result;
            try
            {
                result = (IPresenter)buildMethod.Invoke(null, new IView[]
				{
					viewInstance
				});
            }
            catch (Exception ex)
            {
                Exception innerException = ex;
                if (ex is TargetInvocationException && ex.InnerException != null)
                {
                    innerException = ex.InnerException;
                }
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "An exception was thrown whilst trying to create an instance of {0}. Check the InnerException for more information.", new object[]
				{
					presenterType.FullName
				}), innerException);
            }
            return result;
        }
        public void Release(IPresenter presenter)
        {
            IDisposable disposable = presenter as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
        internal static DynamicMethod GetBuildMethod(Type presenterType, Type viewType)
        {
            string key = string.Join("__:__", new string[]
			{
				presenterType.AssemblyQualifiedName,
				viewType.AssemblyQualifiedName
			});
            return DefaultPresenterFactory.buildMethodCache.GetOrCreateValue(key, () => DefaultPresenterFactory.GetBuildMethodInternal(presenterType, viewType));
        }
        internal static DynamicMethod GetBuildMethodInternal(Type presenterType, Type viewType)
        {
            if (presenterType.IsNotPublic)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "{0} does not meet accessibility requirements. For the WebFormsMvp framework to be able to call it, it must be public. Make the type public, or set PresenterBinder.Factory to an implementation that can access this type.", new object[]
				{
					presenterType.FullName
				}), "presenterType");
            }
            ConstructorInfo constructor = presenterType.GetConstructor(new Type[]
			{
				viewType
			});
            if (constructor == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "{0} is missing an expected constructor, or the constructor is not accessible. We tried to execute code equivalent to: new {0}({1} view). Add a public constructor with a compatible signature, or set PresenterBinder.Factory to an implementation that can supply constructor dependencies.", new object[]
				{
					presenterType.FullName,
					viewType.FullName
				}), "presenterType");
            }
            DynamicMethod dynamicMethod = new DynamicMethod("DynamicConstructor", presenterType, new Type[]
			{
				viewType
			}, presenterType.Module, false);
            ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
            iLGenerator.Emit(OpCodes.Nop);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Newobj, constructor);
            iLGenerator.Emit(OpCodes.Ret);
            return dynamicMethod;
        }
    }
}
