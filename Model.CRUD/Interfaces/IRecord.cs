using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Platform.Model
{
#if PORTABLE
    namespace Core
    {
#endif
        namespace CRUD
        {
            delegate void ChangedDelegate(RecordEventArgs e);
            delegate void ErrorDelegate(RecordEventArgs e);

            public interface IRecord
            {

                #region Public data access

                void Load();
                void Save();
                void Erase();

                #endregion

                #region Private data access

                Object Create();

                Object Read();

                Object Update();

                Object Delete();

                #endregion

                event EventHandler<RecordEventArgs> Changed;
                void OnChanged(RecordEventArgs e);

                event EventHandler<RecordEventArgs> Error;
                void OnError(RecordEventArgs e);

            }

        }
#if PORTABLE
    }
#endif

        }
