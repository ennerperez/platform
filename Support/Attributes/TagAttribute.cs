using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
#if !CORE
    namespace Core
    {
#endif
    namespace Attributes
    {

        public class Tags
        {
            public const string Important = "Important";
        }

        /// <summary>
        /// Tag
        /// </summary>
        [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
        public class TagAttribute : global::System.Attribute
        {
            
            private string[] tags;

            public TagAttribute(params string[] tags)
            {
                this.tags = tags;
            }

            public TagAttribute(string tag)
            {
                this.tags = new string[] {tag};
            }

            public virtual string[] Tags
            {
                get { return this.tags; }
            }
        }
    }
}

#if !CORE
    }
#endif