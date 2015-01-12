using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Attributes.AssemblyProduct
{

    /// <summary>
    /// Define the build <typeparamref name="datetime"/> for the project
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public class BuildDateAttribute : global::System.Attribute
    {
        private System.DateTime _AssemblyDate;
        public System.DateTime AssemblyDate { get { return _AssemblyDate; } }

        public BuildDateAttribute(System.String date)
        {
            this._AssemblyDate = System.DateTime.Parse(date);
        }

        public BuildDateAttribute(System.DateTime date)
        {
            this._AssemblyDate = date;
        }

    }

}
