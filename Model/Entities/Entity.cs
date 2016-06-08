using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Platform.Model
{
#if PORTABLE
    namespace Core
    {
#endif
        public abstract class Entity<TKey> : IEntity<TKey>, INotifyPropertyChanged
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

            public override bool Equals(Object obj)
            {
                // Check for null values and compare run-time types.
                if (obj == null || GetType() != obj.GetType()) return false;

                Entity<TKey> p = (Entity<TKey>)obj;
                return (Id.Equals(p.Id));
            }

            #endregion

            public virtual void Refresh(object e) { }

            public virtual IEnumerable AsEnumerable() { return null; }
            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }

#if !PORTABLE

        public object Clone()
        {
            Type _ty = GetType();
            Entity<TKey> _object = (Entity<TKey>)Activator.CreateInstance(_ty);
            CopyTo(_object);
            return _object;
        }
        public void CopyTo(Entity<TKey> target)
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
#if PORTABLE
    }
#endif
}
