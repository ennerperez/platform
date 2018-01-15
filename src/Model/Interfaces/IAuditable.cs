using System;
using System.Collections.Generic;

namespace Platform.Model
{
#if PORTABLE

    namespace Core
    {
#endif

    public interface IAuditable
    {
        DateTime CreatedAt { get; set; }
        DateTime? ModificatedAt { get; set; }
    }

#if PORTABLE
    }

#endif
}