using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
            Changed?.Invoke(this, EventArgs.Empty);
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
            Loaded?.Invoke(this, EventArgs.Empty); IsLoading = false;
        }

        public event EventHandler Loaded;

        protected internal bool IsLoading;

        public virtual void OnLoading(object e)
        {
            IsLoaded = false;
            Loading?.Invoke(this, EventArgs.Empty); IsLoading = true;
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

        public virtual void Load(bool async)
        {
        }

        #region IDisposable Support

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.IsLoaded = false;
                }
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
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