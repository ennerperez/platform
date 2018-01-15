using System;

#if PORTABLE

using Platform.Model.Core.CRUD;

#else

using Platform.Model.CRUD;

#endif

namespace Platform.Model
{
#if PORTABLE

    namespace Core
    {
#endif

    namespace CRUD
    {
        public abstract class Entity<TKey> :
#if PORTABLE
            Model.Core.Entity<TKey>, IRecord
#else
            Model.Entity<TKey>, IRecord
#endif

        {
            #region Basic CRUD

            public virtual object Create()
            {
                throw new NotImplementedException();
            }

            public virtual object Read()
            {
                throw new NotImplementedException();
            }

            public virtual object Update()
            {
                throw new NotImplementedException();
            }

            public virtual object Delete()
            {
                throw new NotImplementedException();
            }

            #endregion Basic CRUD

            #region EventHandler

            public event EventHandler<RecordEventArgs> Changed;

            public void OnChanged(RecordEventArgs e)
            {
                Changed?.Invoke(this, e);
            }

            public event EventHandler<RecordEventArgs> Error;

            public void OnError(RecordEventArgs e)
            {
                Error?.Invoke(this, e);
            }

            #endregion EventHandler

            #region Extended LSE

            protected internal bool IsLoaded;

            public event EventHandler Loaded;

            public virtual void Load()
            {
                Loaded?.Invoke(this, new EventArgs());
            }

            public event EventHandler Saved;

            public virtual void Save()
            {
                Saved?.Invoke(this, new EventArgs());
            }

            public event EventHandler Erased;

            public virtual void Erase()
            {
                Erased?.Invoke(this, new EventArgs());
            }

            #endregion Extended LSE
        }

        public abstract class Entity : Entity<long>
        {
            public override object Create()
            {
                throw new NotImplementedException();
            }

            public override object Read()
            {
                throw new NotImplementedException();
            }

            public override object Update()
            {
                throw new NotImplementedException();
            }

            public override object Delete()
            {
                throw new NotImplementedException();
            }
        }
    }

#if PORTABLE
    }

#endif
}