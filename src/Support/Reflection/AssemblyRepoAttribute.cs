using System;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

    namespace Reflection
    {
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public class AssemblyRepoAttribute : global::System.Attribute
        {
            public AssemblyRepoAttribute(string owner, string name, string assetName = "")
            {
                Owner = owner;
                Name = name;
            }

            public string Owner { get; private set; }
            public string Name { get; private set; }
        }

        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public class GitHubAttribute : AssemblyRepoAttribute
        {
            public GitHubAttribute(string owner, string name, string assetName = "") : base(owner, name)
            {
                AssetName = assetName;
            }

            public string AssetName { get; private set; }

            public override string ToString()
            {
#if NETFX_40
                return "https://github.com/" + Owner + "/" + Name;
#else
                return $"https://github.com/{Owner}/{Name}";
#endif
            }
        }
    }

#if PORTABLE
    }

#endif
}