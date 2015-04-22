using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Model
{
    public class ISynchronizable
    {

        //string SynchronizationKey { get; set; }
        //bool Synchronize(object updateObj);

        DataRowVersion RowVersion {get;set;}

        DataRowState RowState {get;set;}

    }
}
