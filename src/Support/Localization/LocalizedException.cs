using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

    namespace Localization
    {
        public class LocalizedException : Exception
        {
            public LocalizedException()
            {
            }

            public LocalizedException(ResourceManager resources, string resourceId, params object[] args)
            {
                this.resources = resources;
                this.resourceId = resourceId;
                this.args = args;
            }

            public LocalizedException(string message) : base(message)
            {
            }

            public LocalizedException(string message, Exception innerException) : base(message, innerException)
            {
            }

            public virtual string NeutralMessage
            {
                get
                {
                    string result;
                    if (this.TryGetString(CultureInfo.InvariantCulture, out result))
                    {
                        return result;
                    }
                    return this.Message;
                }
            }

            public override string Message
            {
                get
                {
                    string result;
                    if (this.TryGetString(CultureInfo.CurrentCulture, out result))
                    {
                        return result;
                    }
                    return base.Message;
                }
            }

            private bool TryGetString(CultureInfo culture, out string message)
            {
                if (this.resources != null && !string.IsNullOrEmpty(this.resourceId))
                {
                    message = this.resources.GetString(this.resourceId, culture);
                    if (message != null)
                    {
                        if (this.args != null)
                        {
                            message = string.Format(culture, message, this.args);
                        }
                        return true;
                    }
                }
                message = null;
                return false;
            }

            private readonly ResourceManager resources;

            private readonly string resourceId;

            private readonly object[] args;
        }
    }

#if PORTABLE
    }
#endif
}