﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
#if PORTABLE
    namespace Core
    {
#endif
    namespace Reflection
    {

        /// <summary>
        /// Contact information
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
        public sealed class ContactInformationAttribute : global::System.Attribute
        {

            public ContactInformationAttribute(string description, string value)
            {
                this.description = description;
                this.value = value;
            }

            public ContactInformationAttribute(string value)
            {
                if (value.IsNumeric()) { this.description = "number"; }
                if (value.IsEmail()) { this.description = "email"; }
                this.value = value;
            }

            private string description;
            private string value;
            public string[] Contact
            {
                get { return new string[] { this.description, this.value }; }
            }
        }
    }

#if PORTABLE
    }
#endif
}
