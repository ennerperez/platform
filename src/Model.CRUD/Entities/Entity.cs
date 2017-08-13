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
                return null;
            }

            public virtual object Read()
            {
                return null;
            }

            public virtual object Update()
            {
                return null;
            }

            public virtual object Delete()
            {
                return null;
            }

            #endregion Basic CRUD

            #region EventHandler

            public event EventHandler<RecordEventArgs> Changed;

            public void OnChanged(RecordEventArgs e)
            {
                if (Changed != null) { Changed(this, e); }
            }

            public event EventHandler<RecordEventArgs> Error;

            public void OnError(RecordEventArgs e)
            {
                if (Error != null) { Error(this, e); }
            }

            #endregion EventHandler

            #region Extended LSE

            protected internal bool IsLoaded;

            public event EventHandler Loaded;

            public virtual void Load()
            {
                if (Loaded != null) { Loaded(this, new EventArgs()); }
            }

            public event EventHandler Saved;

            public virtual void Save()
            {
                if (Saved != null) { Saved(this, new EventArgs()); }
            }

            public event EventHandler Erased;

            public virtual void Erase()
            {
                if (Erased != null) { Erased(this, new EventArgs()); }
            }

            #endregion Extended LSE
        }

        public abstract class Entity : Entity<long>
        {
            public override object Create()
            {
                return Id == 0;
            }

            public override object Read()
            {
                return Id == 0;
            }

            public override object Update()
            {
                return Id != 0;
            }

            public override object Delete()
            {
                return Id != 0;
            }
        }
    }

#if PORTABLE
    }

#endif
}