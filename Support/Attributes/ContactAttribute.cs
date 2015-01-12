using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Attributes.AssemblyCompany
    {

        /// <summary>
        /// Contact information
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
        public class ContactAttribute : global::System.Attribute
        {

            private string description;
            private string value;

            public ContactAttribute(string description, string value)
            {
                this.description = description;
                this.value = value;
            }

            public ContactAttribute(string value)
            {
                if (value.IsNumeric()) { this.description = "number"; }
                this.value = value;
            }

            public virtual string[] Contact
            {
                get { return new string[] {this.description, this.value}; }
            }
        }
    }
