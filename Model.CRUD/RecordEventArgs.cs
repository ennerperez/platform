using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Platform.Model.CRUD
{
    [DebuggerStepThrough()]
    public class RecordEventArgs : System.EventArgs
    {

        #region  Constructors

        public RecordEventArgs()
        {
            Operation = Operations.Unknow;
            Result = null;
            Argument = null;
        }

        public RecordEventArgs(Operations operation, object result = null, object argument = null)
        {
            Operation = operation;
            Result = result;
            Argument = argument;
        }

        #endregion

        #region Properties

        public Operations Operation { get; set; }
        public object Result { get; set; }
        public object Argument { get; set; }

        #endregion

    }
}
