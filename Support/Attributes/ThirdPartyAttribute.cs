using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Attributes.AssemblyProduct
    {

        [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
        public class ThirdPartyAttribute : global::System.Attribute
        {

            private string name;
            private string info;

            public ThirdPartyAttribute(string name, string info = null)
            {
                this.name = name;
                this.info = info;
            }

            public virtual string ThirdParty
            {
                get { return this.name; }
            }
            public virtual string Info
            {
                get { return this.info; }
            }

        }

    }
