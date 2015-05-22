using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Platform.Model
{

    public abstract partial class Entities<TEntity, TKey> :
#if !(PORTABLE)
 ObservableCollection<TEntity>
#else
 List<TEntity>
#endif
 where TEntity : IEntity<TKey>
    {

#if (!PORTABLE)
        public Entities()
        {
            this.CollectionChanged += OnCollectionChanged;
        }
#endif

        public TEntity GetItem(TKey id)
        {
#if (!PORTABLE)
            return this.Items.Where(i => i.Id.Equals(id)).FirstOrDefault();
#else
            return this.Where(i => i.Id.Equals(id)).FirstOrDefault();
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
            if (Loaded != null) { Loaded(this, new EventArgs()); }
            IsLoading = false;
        }
        public event EventHandler Loaded;

        protected internal bool IsLoading;
        public virtual void OnLoading(object e)
        {
            IsLoaded = false;
            if (Loading != null) { Loading(this, new EventArgs()); }
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

#if (!PORTABLE)

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
            if (Changed != null) { Changed(this, new EventArgs()); }
        }
        public event EventHandler Changed;

        private void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (!this.disposedValue)
            {
                this.OnChanged();
            }
        }

#endif

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

        // TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
        //Protected Overrides Sub Finalize()
        //    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
        //    Dispose(False)
        //    MyBase.Finalize()
        //End Sub

        // Visual Basic agregó este código para implementar correctamente el modelo descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }


    public abstract partial class Entities<TEntity> : Entities<TEntity, long> where TEntity : IEntity<long>
    {
        public Entities():base()
        {
        }
    }

}
