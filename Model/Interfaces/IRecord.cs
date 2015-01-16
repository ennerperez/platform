using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Model
{

    delegate void ChangedDelegate(EventArgs e);
    delegate void ErrorDelegate(EventArgs e);

    public interface IRecord
    {

        #region Public data access

        void Load();
        void Save();
        void Erase();

        #endregion

        #region Private data access

        Object Update();
        Object Insert();
        Object Delete();

        #endregion

        event EventHandler<EventArgs> Changed;
        void OnChanged(EventArgs e);

        event EventHandler<EventArgs> Error;
        void OnError(EventArgs e);

    }

    [DebuggerStepThrough()]
    public class EventArgs : System.EventArgs
    {

        #region  Constructors

        public EventArgs()
        {
            this.Operation = -1;
            this.Result = null;
            this.Argument = null;
        }

        public EventArgs(int operation, object result = null, object argument = null)
        {
            this.Operation = operation;
            this.Result = result;
            this.Argument = argument;
        }

        #endregion

        #region Properties

        public int Operation { get; set; }
        public object Result { get; set; }
        public object Argument { get; set; }

        #endregion

    }

}
