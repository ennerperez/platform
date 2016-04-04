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

            [AttributeUsage(AttributeTargets.Assembly)]
            public class UrlAttribute : global::System.Attribute
            {

                private Uri url;
                public UrlAttribute(Uri url)
                {
                    this.url = url;
                }
                public UrlAttribute(string url)
                {
                    this.url = new Uri(url);
                }

                public virtual Uri CompanyUrl
                {
                    get { return url; }
                }
            }

        }

#if !CORE
    }
#endif

}