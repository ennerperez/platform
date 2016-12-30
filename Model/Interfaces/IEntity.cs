﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model
{
#if PORTABLE
    namespace Core
    {
#endif
        public interface IEntity<T>
        {
            T Id { get; set; }
        }
        public interface IEntity : IEntity<long>
        {
        }
#if PORTABLE
    }
#endif
}
