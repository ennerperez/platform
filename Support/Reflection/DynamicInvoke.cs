using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Platform.Support.Reflection
{
    /// <summary>
    /// A class that allows you to call to methods and get/set properties on a dynamically loaded assembly.
    /// </summary>
    public class DynamicInvoke
    {

        /// <summary>
        /// Now do it the efficient way by holding references to the assembly 
        /// and the class this is an inner class which holds the class instance info
        /// </summary>
        internal class DynamicClassInfo
        {
            public Type type;
            public Object ClassObject;

            public DynamicClassInfo()
            {
            }

            public DynamicClassInfo(Type t, Object c)
            {
                type = t;
                ClassObject = c;
            }
        }

        private static Dictionary<String, Assembly> AssemblyReferences = new Dictionary<String, Assembly>();
        private static Dictionary<String, DynamicClassInfo> ClassReferences = new Dictionary<String, DynamicClassInfo>();

        internal static DynamicClassInfo GetClassReference(string AssemblyName, string ClassName, Object[] cArgs = null)
        {
            if (PreLoadAssembly(AssemblyName, ClassName, cArgs))
            {
                return (ClassReferences[AssemblyName]);
            }
            return null;
        }

        /// <summary>
        /// Preload an Assembly
        /// </summary>
        /// <param name="AssemblyName">The Filename of the Assembly</param>
        /// <param name="ClassName">The Classname to instantiate</param>
        /// <param name="cArgs">Constructor Parameters (or null)</param>
        /// <returns>true if succeeeded</returns>
        public static Boolean PreLoadAssembly(string AssemblyName, string ClassName, Object[] cArgs = null)
        {
            if (ClassReferences.ContainsKey(AssemblyName) == false)
            {
                Assembly assembly;
                if (AssemblyReferences.ContainsKey(AssemblyName) == false)
                {
                    AssemblyReferences.Add(AssemblyName,
                          assembly = Assembly.LoadFrom(AssemblyName));
                }
                else
                    assembly = AssemblyReferences[AssemblyName];

                // Walk through each type in the assembly
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsClass == true)
                    {
                        // Doing it this way means that you don't have
                        // to specify the full namespace and class (just the class)

                        if (type.FullName.EndsWith("." + ClassName))
                        {
                            try
                            {
                                DynamicClassInfo ci = new DynamicClassInfo(type,
                                                   Activator.CreateInstance(type, cArgs));
                                ClassReferences.Add(AssemblyName, ci);
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine(e.Message);
                            }
                        }
                    }
                }
            }

            return ClassReferences.ContainsKey(AssemblyName);
        }

        private static T CallMethod<T>(DynamicClassInfo ci,
                             string MethodName, Object[] mArgs)
        {
            // Dynamically Invoke the method
            Object Result = ci.type.InvokeMember(MethodName,
              BindingFlags.Default | BindingFlags.InvokeMethod,
                   null,
                   ci.ClassObject,
                   mArgs);
            return (T)Result;
        }

        private static T SetProperty<T>(DynamicClassInfo ci,
                            string PropName, T arg)
        {
            // Dynamically Invoke the method
            PropertyInfo pi = ci.type.GetProperty(PropName);

            //Type type = typeof(T);
            Type ptype = pi.PropertyType;

            if (ptype.IsGenericType && ptype.GetGenericArguments().Length > 0 && ptype.GetGenericArguments()[0].IsEnum)
            {
                // Convert Int32 to Enum?
                TypeConverter convertSet = TypeDescriptor.GetConverter(pi.PropertyType);
                pi.SetValue(ci.ClassObject, convertSet.ConvertFrom(arg.ToString() + ""), new Object[] { });

                // Convert Enum? to Int32
                TypeConverter convertGet = TypeDescriptor.GetConverter(typeof(T));
                int tmp = (int)(pi.GetValue(ci.ClassObject, new Object[] { }));
                return (T)convertGet.ConvertFrom(tmp.ToString() + "");
            }
            else
            {
                pi.SetValue(ci.ClassObject, (T)arg, new Object[] { });

                return (T)(pi.GetValue(ci.ClassObject, new Object[] { }));
            }
        }

        private static T GetProperty<T>(DynamicClassInfo ci,
                           string PropName)
        {
            // Dynamically Get a Property Value
            PropertyInfo pi = ci.type.GetProperty(PropName);

            Object value = pi.GetValue(ci.ClassObject, new Object[] { });
            if (value != null)
            {
                return (T)(value);
            }

            return default(T);
        }

        // --- these is the method that you invoke ------------

        /// <summary>
        /// Call a Generic typed Method on an (cached) Assembly.
        /// </summary>
        /// <typeparam name="T">The return Type</typeparam>
        /// <param name="AssemblyName">The Filename of the Assembly</param>
        /// <param name="ClassName">The Classname to instantiate</param>
        /// <param name="MethodName">The Method name to invoke</param>
        /// <param name="mArgs">Method Parameters</param>
        /// <param name="cArgs">Constructor Parameters (or null)</param>
        /// <returns>The method result if any</returns>
        public static T CallMethod<T>(String AssemblyName, String ClassName, String MethodName, Object[] mArgs, Object[] cArgs = null)
        {
            DynamicClassInfo ci = GetClassReference(AssemblyName, ClassName, cArgs);

            return CallMethod<T>(ci, MethodName, mArgs);
        }

        /// <summary>
        /// Call a Method on an (cached) Assembly.
        /// </summary>
        /// <param name="AssemblyName">The Filename of the Assembly</param>
        /// <param name="ClassName">The Classname to instantiate</param>
        /// <param name="MethodName">The Method name to invoke</param>
        /// <param name="mArgs">Method Parameters</param>
        /// <param name="cArgs">Constructor Parameters (or null)</param>
        public static void CallMethod(String AssemblyName, String ClassName, String MethodName, Object[] mArgs, Object[] cArgs = null)
        {
            DynamicClassInfo ci = GetClassReference(AssemblyName, ClassName, cArgs);

            CallMethod<Object>(ci, MethodName, mArgs);
        }

        /// <summary>
        /// Set a Property value on an (cached) Assembly.
        /// </summary>
        /// <typeparam name="T">The Property Type</typeparam>
        /// <param name="AssemblyName">The Filename of the Assembly</param>
        /// <param name="ClassName">The Classname to instantiate</param>
        /// <param name="PropName">The Property to set</param>
        /// <param name="pArg">Property Value</param>
        /// <param name="cArgs">Constructor Parameters (or null)</param>
        /// <returns>The (new) Property Value</returns>
        public static T SetProperty<T>(String AssemblyName, String ClassName, String PropName, T pArg, Object[] cArgs = null)
        {
            DynamicClassInfo ci = GetClassReference(AssemblyName, ClassName, cArgs);

            return SetProperty<T>(ci, PropName, pArg);
        }

        /// <summary>
        /// Return a Property value of an (cached) Assembly.
        /// </summary>
        /// <typeparam name="T">The Property Type</typeparam>
        /// <param name="AssemblyName">The Filename of the Assembly</param>
        /// <param name="ClassName">The Classname to instantiate</param>
        /// <param name="PropName">The Property to set</param>
        /// <param name="cArgs">Constructor Parameters (or null)</param>
        /// <returns>The Property Value</returns>
        public static T GetProperty<T>(String AssemblyName, String ClassName, String PropName, Object[] cArgs = null)
        {
            DynamicClassInfo ci = GetClassReference(AssemblyName, ClassName, cArgs);

            return GetProperty<T>(ci, PropName);
        }
    }
}
