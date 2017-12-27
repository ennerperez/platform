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
                Tags = tags;
            }

            public TagAttribute(string tag)
            {
                Tags = new string[] { tag };
            }

            public string[] Tags { get; internal set; }

            public override string ToString()
            {
                return string.Join(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator, Tags);
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