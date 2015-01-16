using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{

    delegate void OnWarning(EventArgs e);

    public interface IWarning
    {
        event EventHandler<EventArgs> Warning;
    }
}
