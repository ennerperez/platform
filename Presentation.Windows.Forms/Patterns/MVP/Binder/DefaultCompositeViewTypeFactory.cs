using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Text;

namespace Presentation.Windows.Forms.Patterns.MVP.Binder
{
   	internal class DefaultCompositeViewTypeFactory : ICompositeViewTypeFactory
	{
		private static readonly object compositeViewTypeCacheLock = new object();
		private static readonly IDictionary<RuntimeTypeHandle, Type> compositeViewTypeCache = new Dictionary<RuntimeTypeHandle, Type>();
		public Type BuildCompositeViewType(Type viewType)
		{
			RuntimeTypeHandle typeHandle = viewType.TypeHandle;
			Type type;
			if (DefaultCompositeViewTypeFactory.compositeViewTypeCache.TryGetValue(typeHandle, out type))
			{
				return type;
			}
			lock (DefaultCompositeViewTypeFactory.compositeViewTypeCacheLock)
			{
				type = DefaultCompositeViewTypeFactory.BuildCompositeViewTypeInternal(viewType);
				DefaultCompositeViewTypeFactory.compositeViewTypeCache[typeHandle] = type;
			}
			return type;
		}
		internal static Type BuildCompositeViewTypeInternal(Type viewType)
		{
			DefaultCompositeViewTypeFactory.ValidateViewType(viewType);
			AssemblyName assemblyName = DefaultCompositeViewTypeFactory.BuildAssemblyName();
			AssemblyBuilder assembly = DefaultCompositeViewTypeFactory.BuildAssembly(assemblyName, AppDomain.CurrentDomain);
			ModuleBuilder module = DefaultCompositeViewTypeFactory.BuildModule(assembly, assemblyName);
			TypeBuilder typeBuilder = DefaultCompositeViewTypeFactory.BuildType(viewType, module);
			IEnumerable<PropertyInfo> properties = DefaultCompositeViewTypeFactory.DiscoverProperties(viewType);
			DefaultCompositeViewTypeFactory.BuildProperties(typeBuilder, viewType, properties);
			IEnumerable<EventInfo> events = DefaultCompositeViewTypeFactory.DiscoverEvents(viewType);
			DefaultCompositeViewTypeFactory.BuildEvents(typeBuilder, viewType, events);
			return typeBuilder.CreateType();
		}
		internal static void ValidateViewType(Type viewType)
		{
			if (!viewType.IsInterface)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "To be used with shared presenters, the view type must be an interface, but {0} was supplied instead.", new object[]
				{
					viewType.FullName
				}));
			}
			if (!typeof(IView).IsAssignableFrom(viewType))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "To be used with shared presenters, the view type must inherit from {0}. The supplied type ({1}) does not.", new object[]
				{
					typeof(IView).FullName,
					viewType.FullName
				}));
			}
			if (!viewType.IsPublic && !viewType.IsNestedPublic)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "To be used with shared presenters, the view type must be public. The supplied type ({0}) is not.", new object[]
				{
					viewType.FullName
				}));
			}
		}
		private static AssemblyName BuildAssemblyName()
		{
			return new AssemblyName("WebFormsMvp.CompositeViewTypes");
		}
		private static AssemblyBuilder BuildAssembly(AssemblyName assemblyName, AppDomain appDomain)
		{
			CustomAttributeBuilder[] assemblyAttributes = new CustomAttributeBuilder[]
			{
				new CustomAttributeBuilder(typeof(SecurityTransparentAttribute).GetConstructor(Type.EmptyTypes), new object[0])
			};
			return appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run, assemblyAttributes);
		}
		private static ModuleBuilder BuildModule(AssemblyBuilder assembly, AssemblyName assemblyName)
		{
			return assembly.DefineDynamicModule(assemblyName.Name);
		}
		private static TypeBuilder BuildType(Type viewType, ModuleBuilder module)
		{
			Type compositeViewParentType = DefaultCompositeViewTypeFactory.GetCompositeViewParentType(viewType);
			Type[] interfaces = new Type[]
			{
				viewType
			};
			return module.DefineType(viewType.FullName + "__@CompositeView", TypeAttributes.Public | TypeAttributes.Sealed, compositeViewParentType, interfaces);
		}
		internal static Type GetCompositeViewParentType(Type viewType)
		{
			return typeof(CompositeView<>).MakeGenericType(new Type[]
			{
				viewType
			});
		}
		private static IEnumerable<PropertyInfo> DiscoverProperties(Type type)
		{
			return 
				from p in type.GetProperties().Union(type.GetInterfaces().SelectMany(new Func<Type, IEnumerable<PropertyInfo>>(DefaultCompositeViewTypeFactory.DiscoverProperties)))
				select new
				{
					PropertyInfo = p,
					PropertyInfoFromCompositeViewBase = typeof(CompositeView<>).GetProperty(p.Name)
				} into p
				where p.PropertyInfoFromCompositeViewBase == null || (p.PropertyInfoFromCompositeViewBase.GetGetMethod() == null && p.PropertyInfoFromCompositeViewBase.GetSetMethod() == null)
				select p.PropertyInfo;
		}
		private static MethodBuilder BuildMethod(TypeBuilder type, string methodNamePrefix, string methodName, Type returnType, Type[] parameterTypes)
		{
			return type.DefineMethod(methodNamePrefix + "_" + methodName, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName, returnType, parameterTypes);
		}
		private static void EmitILForEachView(Type viewType, ILGenerator il, Action forEachAction)
		{
			LocalBuilder localBuilder = il.DeclareLocal(viewType);
			LocalBuilder localBuilder2 = il.DeclareLocal(typeof(IEnumerable<>).MakeGenericType(new Type[]
			{
				viewType
			}));
			LocalBuilder localBuilder3 = il.DeclareLocal(typeof(bool));
			il.Emit(OpCodes.Ldarg, localBuilder.LocalIndex);
			MethodInfo getMethod = typeof(CompositeView<>).MakeGenericType(new Type[]
			{
				viewType
			}).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic).First((PropertyInfo pi) => pi.Name == "Views" && pi.PropertyType == typeof(IEnumerable<>).MakeGenericType(new Type[]
			{
				viewType
			})).GetGetMethod(true);
			il.EmitCall(OpCodes.Call, getMethod, null);
			MethodInfo method = typeof(IEnumerable<>).MakeGenericType(new Type[]
			{
				viewType
			}).GetMethod("GetEnumerator", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			il.EmitCall(OpCodes.Callvirt, method, null);
			il.Emit(OpCodes.Stloc, localBuilder2.LocalIndex);
			il.BeginExceptionBlock();
			Label label = il.DefineLabel();
			Label label2 = il.DefineLabel();
			Label label3 = il.DefineLabel();
			Label label4 = il.DefineLabel();
			il.Emit(OpCodes.Br_S, label);
			il.MarkLabel(label2);
			il.Emit(OpCodes.Ldloc, localBuilder2);
			MethodInfo getMethod2 = typeof(IEnumerator<>).MakeGenericType(new Type[]
			{
				viewType
			}).GetProperty("Current").GetGetMethod();
			il.EmitCall(OpCodes.Callvirt, getMethod2, null);
			il.Emit(OpCodes.Stloc, localBuilder);
			il.Emit(OpCodes.Ldloc, localBuilder);
			il.Emit(OpCodes.Ldarg, 1);
			forEachAction();
			il.MarkLabel(label);
			il.Emit(OpCodes.Ldloc, localBuilder2);
			MethodInfo method2 = typeof(IEnumerator).GetMethod("MoveNext");
			il.EmitCall(OpCodes.Callvirt, method2, null);
			il.Emit(OpCodes.Stloc, localBuilder3.LocalIndex);
			il.Emit(OpCodes.Ldloc, localBuilder3.LocalIndex);
			il.Emit(OpCodes.Brtrue_S, label2);
			il.Emit(OpCodes.Leave_S, label4);
			il.BeginFinallyBlock();
			il.Emit(OpCodes.Ldloc, localBuilder2);
			il.Emit(OpCodes.Ldnull);
			il.Emit(OpCodes.Ceq);
			il.Emit(OpCodes.Stloc, localBuilder3);
			il.Emit(OpCodes.Ldloc, localBuilder3);
			il.Emit(OpCodes.Brtrue_S, label3);
			il.Emit(OpCodes.Ldloc, localBuilder2);
			MethodInfo method3 = typeof(IDisposable).GetMethod("Dispose");
			il.Emit(OpCodes.Callvirt, method3);
			il.MarkLabel(label3);
			il.EndExceptionBlock();
			il.MarkLabel(label4);
		}
		private static void BuildProperties(TypeBuilder type, Type viewType, IEnumerable<PropertyInfo> properties)
		{
			foreach (PropertyInfo current in properties)
			{
				DefaultCompositeViewTypeFactory.BuildProperty(type, viewType, current);
			}
		}
		private static void BuildProperty(TypeBuilder type, Type viewType, PropertyInfo propertyInfo)
		{
			MethodBuilder methodBuilder = null;
			if (propertyInfo.CanRead)
			{
				methodBuilder = DefaultCompositeViewTypeFactory.BuildPropertyGetMethod(type, viewType, propertyInfo);
			}
			MethodBuilder methodBuilder2 = null;
			if (propertyInfo.CanWrite)
			{
				methodBuilder2 = DefaultCompositeViewTypeFactory.BuildPropertySetMethod(type, viewType, propertyInfo);
			}
			PropertyBuilder propertyBuilder = type.DefineProperty(propertyInfo.Name, propertyInfo.Attributes, propertyInfo.PropertyType, Type.EmptyTypes);
			if (methodBuilder != null)
			{
				propertyBuilder.SetGetMethod(methodBuilder);
			}
			if (methodBuilder2 != null)
			{
				propertyBuilder.SetSetMethod(methodBuilder2);
			}
		}
		private static MethodBuilder BuildPropertyGetMethod(TypeBuilder type, Type viewType, PropertyInfo propertyInfo)
		{
			MethodBuilder methodBuilder = DefaultCompositeViewTypeFactory.BuildMethod(type, "get", propertyInfo.Name, propertyInfo.PropertyType, Type.EmptyTypes);
			ILGenerator iLGenerator = methodBuilder.GetILGenerator();
			LocalBuilder localBuilder = iLGenerator.DeclareLocal(propertyInfo.PropertyType);
			iLGenerator.Emit(OpCodes.Ldarg, localBuilder.LocalIndex);
			MethodInfo getMethod = typeof(CompositeView<>).MakeGenericType(new Type[]
			{
				viewType
			}).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic).First((PropertyInfo pi) => pi.Name == "Views" && pi.PropertyType == typeof(IEnumerable<>).MakeGenericType(new Type[]
			{
				viewType
			})).GetGetMethod(true);
			iLGenerator.EmitCall(OpCodes.Call, getMethod, null);
			MethodInfo methodInfo = (
				from mi in typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)
				where mi.Name == "First"
				select mi).Single(delegate(MethodInfo mi)
			{
				ParameterInfo[] parameters = mi.GetParameters();
				return parameters.Length == 1 && parameters[0].ParameterType.GUID == typeof(IEnumerable<>).GUID;
			}).MakeGenericMethod(new Type[]
			{
				viewType
			});
			iLGenerator.EmitCall(OpCodes.Call, methodInfo, null);
			MethodInfo getMethod2 = propertyInfo.GetGetMethod();
			iLGenerator.EmitCall(OpCodes.Callvirt, getMethod2, null);
			iLGenerator.Emit(OpCodes.Stloc, localBuilder.LocalIndex);
			iLGenerator.Emit(OpCodes.Ldloc, localBuilder.LocalIndex);
			iLGenerator.Emit(OpCodes.Ret);
			return methodBuilder;
		}
		private static MethodBuilder BuildPropertySetMethod(TypeBuilder type, Type viewType, PropertyInfo propertyInfo)
		{
			MethodBuilder methodBuilder = DefaultCompositeViewTypeFactory.BuildMethod(type, "set", propertyInfo.Name, typeof(void), new Type[]
			{
				propertyInfo.PropertyType
			});
			ILGenerator il = methodBuilder.GetILGenerator();
			DefaultCompositeViewTypeFactory.EmitILForEachView(viewType, il, delegate
			{
				MethodInfo setMethod = propertyInfo.GetSetMethod();
				il.EmitCall(OpCodes.Callvirt, setMethod, null);
			});
			il.Emit(OpCodes.Ret);
			return methodBuilder;
		}
		private static IEnumerable<EventInfo> DiscoverEvents(Type type)
		{
			return type.GetEvents().Union(type.GetInterfaces().SelectMany(new Func<Type, IEnumerable<EventInfo>>(DefaultCompositeViewTypeFactory.DiscoverEvents)));
		}
		private static void BuildEvents(TypeBuilder type, Type viewType, IEnumerable<EventInfo> events)
		{
			foreach (EventInfo current in events)
			{
				DefaultCompositeViewTypeFactory.BuildEvent(type, viewType, current);
			}
		}
		private static void BuildEvent(TypeBuilder type, Type viewType, EventInfo eventInfo)
		{
			MethodBuilder addOnMethod = DefaultCompositeViewTypeFactory.BuildEventAddMethod(type, viewType, eventInfo);
			MethodBuilder removeOnMethod = DefaultCompositeViewTypeFactory.BuildEventRemoveMethod(type, viewType, eventInfo);
			if (eventInfo.EventHandlerType == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The supplied event {0} from {1} does not have the event handler type specified.", new object[]
				{
					eventInfo.Name,
					eventInfo.ReflectedType.Name
				}), "eventInfo");
			}
			EventBuilder eventBuilder = type.DefineEvent(eventInfo.Name, eventInfo.Attributes, eventInfo.EventHandlerType);
			eventBuilder.SetAddOnMethod(addOnMethod);
			eventBuilder.SetRemoveOnMethod(removeOnMethod);
		}
		private static MethodBuilder BuildEventAddMethod(TypeBuilder type, Type viewType, EventInfo eventInfo)
		{
			MethodBuilder methodBuilder = DefaultCompositeViewTypeFactory.BuildMethod(type, "add", eventInfo.Name, typeof(void), new Type[]
			{
				eventInfo.EventHandlerType
			});
			ILGenerator il = methodBuilder.GetILGenerator();
			DefaultCompositeViewTypeFactory.EmitILForEachView(viewType, il, delegate
			{
				MethodInfo addMethod = eventInfo.GetAddMethod();
				il.EmitCall(OpCodes.Callvirt, addMethod, null);
			});
			il.Emit(OpCodes.Ret);
			return methodBuilder;
		}
		private static MethodBuilder BuildEventRemoveMethod(TypeBuilder type, Type viewType, EventInfo eventInfo)
		{
			MethodBuilder methodBuilder = DefaultCompositeViewTypeFactory.BuildMethod(type, "remove", eventInfo.Name, typeof(void), new Type[]
			{
				eventInfo.EventHandlerType
			});
			ILGenerator il = methodBuilder.GetILGenerator();
			DefaultCompositeViewTypeFactory.EmitILForEachView(viewType, il, delegate
			{
				MethodInfo removeMethod = eventInfo.GetRemoveMethod();
				il.EmitCall(OpCodes.Callvirt, removeMethod, null);
			});
			il.Emit(OpCodes.Ret);
			return methodBuilder;
		}
	}
}

