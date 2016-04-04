using System;

namespace Platform.Support
{
#if !CORE
    namespace Core
    {
#endif

        namespace Attributes
        {

            [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
            public class MailAttribute : global::System.Attribute
            {

#if (!PORTABLE)


            private System.Net.Mail.MailAddress email;

            public MailAttribute(System.Net.Mail.MailAddress email)
            {
                this.email = email;
            }
            public MailAttribute(string email)
            {
                this.email = new System.Net.Mail.MailAddress(email);
            }

            public virtual System.Net.Mail.MailAddress CompanyEmail
            {
                get { return this.email; }
            }

#else

                private string email;

                public MailAttribute(string email)
                {
                    this.email = email;
                }

                public virtual string CompanyEmail
                {
                    get { return this.email; }
                }


#endif

            }
        }


#if !CORE
    }
#endif
}