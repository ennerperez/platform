using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Model
{
    public abstract class Entity<T> : IEntity<T>, IRecord, INotifyPropertyChanged
#if (!PORTABLE)
, ICloneable, INotifyPropertyChanging
#endif
    {

        public event PropertyChangedEventHandler PropertyChanged;
#if (!PORTABLE)
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

        public T Id { get; set; }

        #endregion

        public event EventHandler<EventArgs> Changed;
        public void OnChanged(EventArgs e)
        {
            if (Changed != null) { Changed(this, e); }
        }

        public event EventHandler<EventArgs> Error;
        public void OnError(EventArgs e)
        {
            if (Error != null) { Error(this, e); }
        }

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

        public virtual void Refresh(object e) { }

        public virtual object Insert() { return null; }//this.Id == 0; }
        public virtual object Update() { return null; }//this.Id != 0; }
        public virtual object Delete() { return null; }// this.Id != 0; }

        public virtual IEnumerable AsEnumerable() { return null; }

#if (!PORTABLE)

        public object Clone()
        {
            Type _ty = this.GetType();
            Entity _object = (Entity)Activator.CreateInstance(_ty);
            this.CopyTo(ref _object);
            return _object;
        }
        public void CopyTo(ref Entity target)
        {
            if (object.ReferenceEquals(this.GetType(), target.GetType()))
            {
                Type t = this.GetType();
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


    public abstract class Entity : Entity<int>
    {

        public override object Insert() { return this.Id == 0; }
        public override object Update() { return this.Id != 0; }
        public override object Delete() { return this.Id != 0; }

    }

}
