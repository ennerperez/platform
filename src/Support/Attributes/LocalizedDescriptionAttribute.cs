#if !PORTABLE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Text;

namespace Platform.Support.Attributes
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly string _resourceKey;
        private readonly ResourceManager _resource;

        private static Dictionary<Type, ResourceManager> resources = new Dictionary<Type, ResourceManager>();

        public LocalizedDescriptionAttribute(string resourceKey, Type resourceType, bool cached = true)
        {
            if (cached)
            {
                if (!resources.ContainsKey(resourceType))
                    resources.Add(resourceType, new ResourceManager(resourceType));
                _resource = resources[resourceType];
            }
            else
                _resource = new ResourceManager(resourceType);
            _resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                string displayName = _resource.GetString(_resourceKey);

                return string.IsNullOrEmpty(displayName)
                    ? string.Format("[[{0}]]", _resourceKey)
                    : displayName;
            }
        }
    }
}

#endif