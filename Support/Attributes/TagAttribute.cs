using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
#if PORTABLE
    namespace Core
    {
#endif
    namespace Attributes
    {

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
                tags = new string[] { tag };
            }

            public virtual string[] Tags
            {
                get { return tags; }
            }
        }
    }

    public sealed class Tags
    {
        public const string Important = "Important";
    }

#if PORTABLE
    }
#endif

}

