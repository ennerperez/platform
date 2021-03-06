﻿using System.Diagnostics;

namespace Platform.Model
{
#if PORTABLE

    namespace Core
    {
#endif

    namespace CRUD
    {
        [DebuggerStepThrough()]
        public class RecordEventArgs : System.EventArgs
        {
            #region Constructors

            public RecordEventArgs()
            {
                Operation = Operation.Unknow;
                Result = null;
                Argument = null;
            }

            public RecordEventArgs(Operation operation, object result = null, object argument = null)
            {
                Operation = operation;
                Result = result;
                Argument = argument;
            }

            #endregion Constructors

            #region Properties

            public Operation Operation { get; set; }
            public object Result { get; set; }
            public object Argument { get; set; }

            #endregion Properties
        }
    }

#if PORTABLE
    }

#endif
}