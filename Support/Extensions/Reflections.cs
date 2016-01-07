using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
    public static partial class Extensions
    {

        #region C#

        public static T GetAttribute<T>(this System.Reflection.Assembly assembly) where T : System.Attribute
        {
            return Helpers.GetAttribute<T>(assembly);
        }

        public static object GetAttribute(this System.Reflection.Assembly assembly, Type AttributeType)
        {
            return Helpers.GetAttribute(AttributeType, assembly);
        }

        public static String Title(this System.Reflection.Assembly assembly)
        {
            return Helpers.Title(assembly);
        }
        public static String Description(this System.Reflection.Assembly assembly)
        {
            return Helpers.Description(assembly);
        }
        public static String Company(this System.Reflection.Assembly assembly)
        {
            return Helpers.Company(assembly);
        }
        public static String Product(this System.Reflection.Assembly assembly)
        {
            return Helpers.Product(assembly);
        }
        public static String Copyright(this System.Reflection.Assembly assembly)
        {
            return Helpers.Copyright(assembly);
        }
        public static String Trademark(this System.Reflection.Assembly assembly)
        {
            return Helpers.Trademark(assembly);
        }
        public static Version Version(this System.Reflection.Assembly assembly)
        {
            return Helpers.Version(assembly);
        }
        public static Version FileVersion(this System.Reflection.Assembly assembly)
        {
            return Helpers.FileVersion(assembly);
        }

        #endregion

        #region VB


        #endregion

#if (!PORTABLE)
        public static Guid GUID(this System.Reflection.Assembly assembly)
        {
            return Helpers.GUID(assembly);
        }
        public static String DirectoryPath(this System.Reflection.Assembly assembly)
        {
            return Helpers.DirectoryPath(assembly);
        }
#endif


    }
}
