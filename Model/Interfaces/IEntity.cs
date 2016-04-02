using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model
{
    public interface IEntity : IEntity<long> 
    {
        //new long Id { get; set; }
    }
    public interface IEntity<T> 
    {
        T Id { get; set; }
    }
}
