using System;

namespace Platform.Model
{
#if PORTABLE

    namespace Core
    {
#endif

        internal delegate void OnWarning(EventArgs e);

        public interface IWarning
        {
            event EventHandler<EventArgs> Warning;
        }

#if PORTABLE
    }

#endif
}