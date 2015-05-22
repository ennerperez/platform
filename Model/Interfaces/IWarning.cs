using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model
{

    delegate void OnWarning(EventArgs e);

    public interface IWarning
    {
        event EventHandler<EventArgs> Warning;
    }
}
