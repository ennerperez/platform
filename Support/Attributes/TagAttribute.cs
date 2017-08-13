using System;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

        namespace Attributes
        {
            /// <summary>
            /// Tags
            /// </summary>
            [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
            public sealed class TagAttribute : global::System.Attribute
            {
                public TagAttribute(params string[] tags)
                {
                    this.tags = tags;
                }

                public TagAttribute(string tag)
                {
                    tags = new string[] { tag };
                }

                private string[] tags;
                public string[] Tags { get { return tags; } }
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