using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Samples.Core.Models
{
    public class Model1 //: Platform.Support.Core.Singleton<Demostrative>
    {
        public string Name { get; set; }
        private static Model1 instance;

        protected Model1()
        {
        }

        public static Model1 Instance
        {
            get
            {
                if (instance == null)
                    instance = new Model1();
                return instance;
            }
        }
    }
}