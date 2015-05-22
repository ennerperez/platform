using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model
{
    public interface IAuditable
    {
        IEnumerable<Autid> Audits { get; set; }
    }

    public struct Autid
    {

        System.DateTime Created { get; set; }
        System.DateTime Modified { get; set; }
        IEntity Owner { get; set; }

        string Property { get; set; }
        object Value { get; set; }

    }

}
