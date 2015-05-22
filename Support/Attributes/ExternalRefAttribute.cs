using System;

namespace Platform.Support
{
#if !CORE
    namespace Core
    {
#endif

        namespace Attributes.AssemblyProduct
        {

            [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]

            public class ExternalRefAttribute : global::System.Attribute
            {

                private string filename;
                private string description;
                private string arguments;
                private object optional;

                public ExternalRefAttribute(string description, string filename, string arguments = "", object optional = null)
                {
                    this.filename = filename;
                    this.description = description;
                    this.arguments = arguments;
                    this.optional = optional;
                }

                public virtual string FileName
                {
                    get { return this.filename; }
                }
                public virtual string Description
                {
                    get { return this.description; }
                }
                public virtual string Arguments
                {
                    get { return this.arguments; }
                }
                public virtual object Optional
                {
                    get { return this.optional; }
                }

            }

        }

#if !CORE
    }
#endif

}
