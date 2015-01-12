using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueAttribute : IndexedAttribute
    {
        public override bool Unique
        {
            get { return true; }
            set { }
        }
    }
}
