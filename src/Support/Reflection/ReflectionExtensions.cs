using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

    namespace Reflection
    {
        public static partial class Extensions
        {
            #region Assembly

#if (!PORTABLE)
            internal static Assembly assembly = Assembly.GetEntryAssembly();
#elif NETFX_45
            internal static Assembly assembly = typeof(Extensions).GetTypeInfo().Assembly;
#else
            internal static Assembly assembly = Assembly.GetCallingAssembly();
#endif

            #endregion Assembly

            #region Attributes

            public static T GetAttribute<T>(Assembly assembly = null) where T : System.Attribute
            {
                return (T)GetAttribute(typeof(T), assembly);
            }

            public static T[] GetAttributes<T>(Assembly assembly = null) where T : System.Attribute
            {
                return (T[])GetAttributes(typeof(T), assembly);
            }

            public static object GetAttribute(Type AttributeType, Assembly assembly = null)
            {
                var result = GetAttributes(AttributeType, assembly);
                if (result != null) return result.FirstOrDefault();
                return null;
            }

            public static object[] GetAttributes(Type AttributeType, Assembly assembly = null)
            {
                if (assembly == null) { assembly = Extensions.assembly; }

#if NETFX_45
                var customAttributes = assembly.GetCustomAttributes(AttributeType);
                if (!customAttributes.Any())
                    return null;

                return customAttributes.ToArray();

#else
                object[] customAttributes = assembly.GetCustomAttributes(AttributeType, true);
                if (customAttributes.Length == 0)
                    return null;

                return customAttributes;
#endif
            }

            #endregion Attributes

            #region ReflectionService

#if !PORTABLE

            public static IEnumerable<PropertyInfo> GetPublicInstanceProperties(this Type typ)
            {
                ReflectionService _return = new ReflectionService();
                return _return.GetPublicInstanceProperties(typ);
            }

            public static IEnumerable<PropertyInfo> GetNonPublicInstanceProperties(this Type typ)
            {
                ReflectionService _return = new ReflectionService();
                return _return.GetNonPublicInstanceProperties(typ);
            }

            public static IEnumerable<PropertyInfo> GetStaticInstanceProperties(this Type typ)
            {
                ReflectionService _return = new ReflectionService();
                return _return.GetStaticInstanceProperties(typ);
            }

            public static IEnumerable<PropertyInfo> GetInstanceProperties(this Type typ)
            {
                ReflectionService _return = new ReflectionService();
                return _return.GetInstanceProperties(typ);
            }

            public static object GetMemberValue(this Type typ, Expression expr, MemberInfo member)
            {
                ReflectionService _return = new ReflectionService();
                return _return.GetMemberValue(typ, expr, member);
            }

#endif

            #endregion ReflectionService

            #region Generics

#if !PORTABLE

            public static T Clone<T>(this T item) where T : ICloneable
            {
                return (T)item.Clone();
            }

#endif

            public static T As<T>(this object source)
            {
                T target = default(T);

                var type = source.GetType();
                try
                {
                    target = (T)source;
                }
                catch (Exception)
                {
                    target = (T)Activator.CreateInstance(typeof(T), new[] { source });
                }

                return target;
            }

            public static T Map<T>(this object source, bool strict = false)
            {
#if NETFX_45
                    var p_source = source.GetType().GetRuntimeProperties();
                    var p_target = typeof(T).GetRuntimeProperties();
#else
                var p_source = source.GetType().GetProperties();
                var p_target = typeof(T).GetProperties();
#endif

                T target = default(T);

                var properties = from s_item in p_source
                                 join t_item in p_target on s_item.Name equals t_item.Name
                                 where (!strict) || (strict && s_item.PropertyType == t_item.PropertyType)
                                 select new { source = s_item, target = t_item };

                if (target == null) target = Activator.CreateInstance<T>();

                foreach (var item in properties)
                {
                    var input = item.source.GetValue(source, null);
                    try
                    {
                        if (item.source.PropertyType != item.target.PropertyType)
                            input = Convert.ChangeType(input, item.target.PropertyType, null);
                        item.target.SetValue(target, input, null);
                    }
                    catch (Exception ex)
                    {
                        //input = input == null ? null : input.ToString();
                        ex.DebugThis();
                    }
                    finally
                    {
                        item.target.SetValue(target, input, null);
                    }
                }

                return target;
            }

            #endregion Generics

            public static bool IsSingleton(this object obj)
            {
                var type = obj.GetType();

#if !PORTABLE || PROFILE_328
                var props = type.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                var instance = props.Any(p => p.Name == "Instance");

                var ctors = type.GetMembers(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).OfType<System.Reflection.ConstructorInfo>();
                var ctor = ctors.Where(m => m.IsFamily).Any();
#else
                    var props = type.GetRuntimeProperties();
                    var instance = props.Any(p => p.Name == "Instance");

                    var ctors = type.GetRuntimeMethods().Where(m => m.IsConstructor);
                    var ctor = ctors.Where(m => m.IsFamily).Any();
#endif
                return instance && ctor;
            }

            public static string GetNameSafe(this Assembly assembly)
            {
                if (assembly == null)
                    throw new ArgumentNullException("assembly");
                return new AssemblyName(assembly.FullName).Name;
            }

#if NETFX_40

            public static T GetCustomAttribute<T>(this Assembly assembly) where T : System.Attribute
            {
                return (T)GetAttribute<T>(assembly);
            }

#endif

#if !PORTABLE

            public static string GetDirectory(this Assembly assembly)
            {
                if (assembly == null) assembly = Assembly.GetEntryAssembly();
                string path = System.IO.Path.GetDirectoryName(assembly.GetName().CodeBase);
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                return file.Directory.FullName;
            }

#endif

            #region AssemblyInfo

            public static string Title(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyTitleAttribute>(assembly);
                if (result != null && !string.IsNullOrEmpty(result.Title))
                    return result.Title;
                return null;
            }

            public static string Description(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyDescriptionAttribute>(assembly);
                if (result != null && !string.IsNullOrEmpty(result.Description))
                    return result.Description;
                return null;
            }

            public static string Company(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyCompanyAttribute>(assembly);
                if (result != null && !string.IsNullOrEmpty(result.Company))
                    return result.Company;
                return null;
            }

            public static string Product(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyProductAttribute>(assembly);
                if (result != null && !string.IsNullOrEmpty(result.Product))
                    return result.Product;
                return null;
            }

            public static string Copyright(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyCopyrightAttribute>(assembly);
                if (result != null && !string.IsNullOrEmpty(result.Copyright))
                    return result.Copyright;
                return null;
            }

            public static string Trademark(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyTrademarkAttribute>(assembly);
                if (result != null && !string.IsNullOrEmpty(result.Trademark))
                    return result.Trademark;
                return null;
            }

            public static Version Version(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyVersionAttribute>(assembly);
                if (result != null)
                {
                    var version = result.Version;
#if !PORTABLE
                    if (string.IsNullOrEmpty(version))
                        return assembly.GetName().Version;
#endif
                    if (!string.IsNullOrEmpty(version))
                        return new Version(version);
                }
                else
                {
                    var assemblyName = new AssemblyName(assembly.FullName);
                    return assemblyName.Version;
                }

                return null;
            }

            public static Version FileVersion(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyFileVersionAttribute>(assembly);
                if (result != null)
                    return new Version(result.Version);
                return null;
            }

            public static Version InformationalVersion(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyInformationalVersionAttribute>(assembly);
                if (result != null)
                    return new Version(result.InformationalVersion);
                return null;
            }

            public static SemanticVersion SemanticVersion(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyInformationalVersionAttribute>(assembly);
                if (result != null)
                    return new SemanticVersion(result.InformationalVersion);
                return null;
            }

#if (!PORTABLE)

            public static Guid? GUID(this Assembly assembly)
            {
                var result = GetAttribute<GuidAttribute>(assembly);
                if (result != null && !string.IsNullOrEmpty(result.Value) && new Guid(result.Value) != Guid.Empty)
                    return new Guid(result.Value);
                return null;
            }

            public static string DirectoryPath(this Assembly assembly)
            {
                return new FileInfo(assembly.Location).Directory.FullName;
            }

            public static string ExecutablePath(this Assembly assembly)
            {
                return assembly.Location;
            }

            public static string ApplicationDataPath(this Assembly assembly)
            {
                var parts = new List<string>
                {
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    assembly.Company(),
                    assembly.Title(),
                    assembly.Version().ToString(3)
                };
                return Path.Combine(parts.Where(p => !string.IsNullOrEmpty(p)).ToArray());
            }

#endif

            public static DateTime? BuildDate(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyBuildDateAttribute>(assembly);
                if (result != null)
                    return result.Date;
                return null;
            }

            public static string CompanyId(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyCompanyIdAttribute>(assembly);
                if (result != null && !string.IsNullOrEmpty(result.Id))
                    return result.Id;
                return null;
            }

            public static string CompanyUrl(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyCompanyUrlAttribute>(assembly);
                if (result != null && !string.IsNullOrEmpty(result.Url))
                    return result.Url;
                return null;
            }

            public static IEnumerable<string> DevelopersNames(this Assembly assembly)
            {
                var result = GetAttributes<AssemblyDeveloperAttribute>(assembly);
                if (result != null && result.Any())
                    return result.Select(item => item.Name).AsEnumerable();
                return null;
            }

#if (!PORTABLE)

            public static IEnumerable<Process> ExternalReferences(this Assembly assembly)
            {
                var result = GetAttributes<AssemblyExternalRefAttribute>(assembly);
                if (result != null && result.Any())
                    return result.Select(d => new Process() { StartInfo = new ProcessStartInfo(d.File, d.Arguments) }).AsEnumerable();
                return null;
            }

#else

                public static IEnumerable<IEnumerable<string>> ExternalReferences(this Assembly assembly)
                {
                    var result = GetAttributes<AssemblyExternalRefAttribute>(assembly);
                    if (result != null && result.Any())
                        return result.Select(d => new string[] { d.File, d.Arguments }).AsEnumerable();
                    return null;
                }

#endif

            public static string License(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyLicenseAttribute>(assembly);
                if (result != null)
                    if (!string.IsNullOrEmpty(result.Name))
                        return result.Name;
                    else if (!string.IsNullOrEmpty(result.Url))
                        return result.Url;
                return null;
            }

            public static string MadeIn(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyMadeInAttribute>(assembly);
                if (result != null && !string.IsNullOrEmpty(result.Name))
                    return result.Name + (!string.IsNullOrEmpty(result.CountryCode) ? string.Format(" ({0})", result.CountryCode) : "");
                return null;
            }

            public static string Owner(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyOwnerAttribute>(assembly);
                if (result != null && !string.IsNullOrEmpty(result.Name))
                    return result.Name;
                return null;
            }

            public static ProductLevels? ProductLevel(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyProductLevelAttribute>(assembly);
                if (result != null)
                    return result.Level;
                return null;
            }

            public static short? ProductLevelNumber(this Assembly assembly)
            {
                var result = GetAttribute<AssemblyProductLevelAttribute>(assembly);
                if (result != null)
                    return result.Number;
                return null;
            }

            public static IEnumerable<string> ThirdParties(this Assembly assembly)
            {
                var result = GetAttributes<AssemblyThirdPartyAttribute>(assembly);
                if (result != null && result.Any())
                    return result.Select(item => item.Name + ": " + item.Info).AsEnumerable();
                return null;
            }

            public static IEnumerable<IEnumerable<string>> ContactInformation(this Assembly assembly)
            {
                var result = GetAttributes<AssemblyContactInformationAttribute>(assembly);
                if (result != null && result.Any())
                    return result.Select(item => item.Contact).AsEnumerable();
                return null;
            }

            #endregion AssemblyInfo
        }
    }

#if PORTABLE
    }

#endif
}