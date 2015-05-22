//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;

//namespace Platform.Support.Data
//{
//    public class PreparedInsertCommand : IDisposable
//    {
//        //internal static readonly IDbStatement NullStatement;
//        //private readonly ISQLitePlatform _sqlitePlatform;
//        public bool Initialized { get; set; }
//        protected IDbConnection Connection { get; set; }
        
//        public string CommandText {get;set;}

//        //protected IDbStatement Statement { get; set; }
//        internal PreparedInsertCommand(IDbConnection conn)
//        {
//            this.Connection = conn;
//        }
//        public void Dispose()
//        {
//            this.Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//        public int ExecuteNonQuery(object[] source)
//        {
//            //this.Connection.TraceListener.WriteLine("Executing: {0}", this.CommandText);
//            if (!this.Initialized)
//            {
//                //this.Statement = this.Prepare();
//                this.Initialized = true;
//            }

//            return 0;

//            //if (source != null)
//            //{
//            //    for (int i = 0; i < source.Length; i++)
//            //    {
//            //        SQLiteCommand.BindParameter(this._sqlitePlatform.SQLiteApi, this.Statement, i + 1, source[i], this.Connection.StoreDateTimeAsTicks, this.Connection.Serializer);
//            //    }
//            //}
//            //Result result = this._sqlitePlatform.SQLiteApi.Step(this.Statement);
//            //if (result == Result.Done)
//            //{
//            //    int arg_C9_0 = this._sqlitePlatform.SQLiteApi.Changes(this.Connection.Handle);
//            //    this._sqlitePlatform.SQLiteApi.Reset(this.Statement);
//            //    return arg_C9_0;
//            //}
//            //if (result == Result.Error)
//            //{
//            //    string message = this._sqlitePlatform.SQLiteApi.Errmsg16(this.Connection.Handle);
//            //    this._sqlitePlatform.SQLiteApi.Reset(this.Statement);
//            //    throw SQLiteException.New(result, message);
//            //}
//            //if (result == Result.Constraint && this._sqlitePlatform.SQLiteApi.ExtendedErrCode(this.Connection.Handle) == ExtendedResult.ConstraintNotNull)
//            //{
//            //    this._sqlitePlatform.SQLiteApi.Reset(this.Statement);
//            //    throw NotNullConstraintViolationException.New(result, this._sqlitePlatform.SQLiteApi.Errmsg16(this.Connection.Handle));
//            //}
//            //this._sqlitePlatform.SQLiteApi.Reset(this.Statement);
//            //throw SQLiteException.New(result, result.ToString());
//        }
//        //protected virtual IDbStatement Prepare()
//        //{
//        //    return this._sqlitePlatform.SQLiteApi.Prepare2(this.Connection.Handle, this.CommandText);
//        //}
//        private void Dispose(bool disposing)
//        {
//            //if (this.Statement != PreparedSqlLiteInsertCommand.NullStatement)
//            //{
//            //    try
//            //    {
//            //        this._sqlitePlatform.SQLiteApi.Finalize(this.Statement);
//            //    }
//            //    finally
//            //    {
//            //        this.Statement = PreparedSqlLiteInsertCommand.NullStatement;
//                    this.Connection = null;
//            //    }
//            //}
//        }

//        ~PreparedInsertCommand()
//        {
//            this.Dispose(false);
//        }
//    }
//}
