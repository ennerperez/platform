using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing.Icons
{
    [Flags]
    public enum IconOutputFormat
    {
        None = 0,
        Vista = 1,
        WinXP = 2,
        WinXPUnpopular = 4,
        Win95 = 8,
        Win95Unpopular = 16,
        Win31 = 32,
        Win31Unpopular = 64,
        Win30 = 128,
        Win30Unpopular = 256,
        FromWinXP = 3,
        FromWin95 = 11,
        FromWin31 = 43,
        FromWin30 = 171,
        All = 127
    }
}