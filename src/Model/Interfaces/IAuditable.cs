using System.Collections.Generic;

namespace Platform.Model
{
#if PORTABLE

    namespace Core
    {
#endif

        public interface IAuditable
        {
            IEnumerable<Audit> Audits { get; set; }
        }

        public struct Audit
        {
            System.DateTime Created { get; set; }
            System.DateTime Modified { get; set; }
            IEntity Owner { get; set; }

            string Property { get; set; }
            object Value { get; set; }
        }

#if PORTABLE
    }

#endif
}