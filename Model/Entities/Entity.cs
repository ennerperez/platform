using Platform.Model.CRUD;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Platform.Model
{
    public abstract class Entity<TKey> : IEntity<TKey>, IRecord, INotifyPropertyChanged
#if !PORTABLE
, ICloneable, INotifyPropertyChanging
#endif
    {

        public event PropertyChangedEventHandler PropertyChanged;
#if !PORTABLE
        public event PropertyChangingEventHandler PropertyChanging;
#endif

        protected internal void SetField<U>(ref U field, U value)
        {
#if (!PORTABLE)
            if (!EqualityComparer<U>.Default.Equals(field, value))
            {
                if (PropertyChanging != null)
                {
                    PropertyChanging(this, new PropertyChangingEventArgs(field.ToString()));
                }
#endif
                field = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(field.ToString()));
                }
#if (!PORTABLE)
            }
#endif
        }
        protected internal void SetField(ref object field, object value)
        {
#if (!PORTABLE)
            if (!EqualityComparer<object>.Default.Equals(field, value))
            {
                if (PropertyChanging != null)
                {
                    PropertyChanging(this, new PropertyChangingEventArgs(field.ToString()));
                }
#endif
                field = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(field.ToString()));
                }
#if (!PORTABLE)
            }
#endif
        }

        #region Properties

        public virtual TKey Id { get; set; }

        #endregion

        #region Operators

        //public static bool operator ==(Entity<TKey> var1, Entity<TKey> var2)
        //{

        //    if (object.ReferenceEquals(var1, null))
        //    {
        //        return object.ReferenceEquals(var2, null);
        //    }

        //    if (object.ReferenceEquals(var2, null))
        //    {
        //        return object.ReferenceEquals(var1, null);
        //    }

        //    return var1.Id.Equals(var2.Id);

        //}
        //public static bool operator !=(Entity<TKey> var1, Entity<TKey> var2)
        //{
        //    if (object.ReferenceEquals(var1, null))
        //    {
        //        return object.ReferenceEquals(var2, null);
        //    }

        //    if (object.ReferenceEquals(var2, null))
        //    {
        //        return object.ReferenceEquals(var1, null);
        //    }

        //    return !var1.Id.Equals(var2.Id);
        //}

        public override bool Equals(Object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType()) return false;

            Entity<TKey> p = (Entity<TKey>)obj;
            return (Id.Equals(p.Id));
        }

        #endregion

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

        #endregion

        #region Basic CRUD

        public virtual object Create() { return null; }
        public virtual object Read() { return null; }
        public virtual object Update() { return null; }
        public virtual object Delete() { return null; }

        #endregion

        public virtual void Refresh(object e) { }

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

        #endregion

        public virtual IEnumerable AsEnumerable() { return null; }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

#if (!PORTABLE)

        public object Clone()
        {
            Type _ty = GetType();
            Entity _object = (Entity)Activator.CreateInstance(_ty);
            CopyTo(_object);
            return _object;
        }
        public void CopyTo(Entity target)
        {
            if (object.ReferenceEquals(GetType(), target.GetType()))
            {
                Type t = GetType();
                System.ComponentModel.PropertyDescriptorCollection properties = System.ComponentModel.TypeDescriptor.GetProperties(target);
                foreach (System.ComponentModel.PropertyDescriptor item in properties.OfType<System.ComponentModel.PropertyDescriptor>())
                {
                    if (!item.IsReadOnly)
                    {
                        item.SetValue(target, item.GetValue(this));
                    }
                    else
                    {
                        System.Reflection.FieldInfo _fi = t.GetField("_" + item.Name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                        if (_fi != null)
                            _fi.SetValue(this, item.GetValue(this));
                    }
                }
            }
            else
            {
                throw new InvalidCastException("Can't implicit convert in copy.");
            }
        }

#endif

    }

    public abstract class Entity : Entity<long>
    {

        public override object Create() { return Id == 0; }
        public override object Read() { return Id == 0; }
        public override object Update() { return Id != 0; }
        public override object Delete() { return Id != 0; }

    }

}
