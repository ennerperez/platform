using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public interface IEntity : IEntity<int> 
    {
        new int Id { get; set; }
    }
    public interface IEntity<T> 
    {
        T Id { get; set; }
    }
}
