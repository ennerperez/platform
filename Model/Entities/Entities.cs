using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
#if !(PORTABLE)
using System.Collections.Specialized;
#endif

namespace Platform.Model
{

#if PORTABLE
    namespace Core
    {
#endif

        public abstract class Entities<TEntity, TKey> :
#if !(PORTABLE)
 ObservableCollection<TEntity>
#else
 Collection<TEntity>
#endif
 where TEntity : IEntity<TKey>
        {

#if (!PORTABLE)
        public Entities() : base()
        {
        }

        public virtual Dictionary<TKey, TEntity> ToDictionary()
        {
            Dictionary<TKey, TEntity> _return = new Dictionary<TKey, TEntity>();
            foreach (TEntity item in this.ToList())
            {
                _return.Add(item.Id, item);
            }
            return _return;
        }

        public virtual void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }
        public event EventHandler Changed;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            if (!disposedValue)
                OnChanged();
        }

#endif

            public TEntity GetItem(TKey id)
            {
#if (!PORTABLE)
            return this.Items.FirstOrDefault(i => i.Id.Equals(id));
#else
                return this.FirstOrDefault(i => i.Id.Equals(id));
#endif
            }

            public Type Type()
            {
                return typeof(TEntity);
            }

            protected internal bool IsLoaded;
            public virtual void OnLoad(object e)
            {
                IsLoaded = true;
                if (Loaded != null) { Loaded(this, EventArgs.Empty); }
                IsLoading = false;
            }
            public event EventHandler Loaded;

            protected internal bool IsLoading;
            public virtual void OnLoading(object e)
            {
                IsLoaded = false;
                if (Loading != null) { Loading(this, EventArgs.Empty); }
                IsLoading = true;
            }
            public event EventHandler Loading;

            public virtual void Refresh(object e)
            {
                if (!IsLoading)
                {
                    IsLoaded = false;
                    this.Load();
                }
            }

            public abstract void Load();
            public virtual void Load(bool async) { }

            #region IDisposable Support

            // Para detectar llamadas redundantes
            private bool disposedValue;

            // IDisposable
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: eliminar estado administrado (objetos administrados).
                        this.IsLoaded = false;
                    }
                    // TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
                    // TODO: Establecer campos grandes como Null.
#if (!PORTABLE)
                this.ClearItems();
#else
                    this.Clear();
#endif
                }
                this.disposedValue = true;
            }

            public void Dispose()
            {
                // No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

        }

        public abstract class Entities<TEntity> : Entities<TEntity, long> where TEntity : IEntity<long>
        {
            public Entities() : base()
            {
            }
        }

#if PORTABLE
    }
#endif

}
