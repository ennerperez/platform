﻿using Platform.Support.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.DataModel.Models
{

    [Table("library")]
    public class LibraryModel : Platform.Model.Entity<Guid>
    {

        //public Guid Guid { get; set; }

        public string Name { get; set; }

        public Version Version { get; set;  }

    }
}
