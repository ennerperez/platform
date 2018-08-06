using System;

namespace Platform.Model
{
#if PORTABLE

    namespace Core
    {
#endif

    namespace CRUD
    {
        internal delegate void ChangedDelegate(RecordEventArgs e);

        internal delegate void ErrorDelegate(RecordEventArgs e);

        public interface IRecord
        {
            #region Public data access

            void Load();

            void Save();

            void Erase();

            #endregion Public data access

            #region Private data access

            Object Create();

            Object Read();

            Object Update();

            Object Delete();

            #endregion Private data access

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