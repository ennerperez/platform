using Platform.Support.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Core
{
    public class Demostrative //: Platform.Support.Core.Singleton<Demostrative>
    {

        public string Name { get; set; }
        private static Demostrative instance;

        protected Demostrative() { }

        public static Demostrative Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Demostrative();
                }
                return instance;
            }
        }
    }
}
