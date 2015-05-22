using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model.MVC
{

    public class Controller<T> : IController<T> where T : IModel
    {

        IView<T> _view;
        IList _items;
        T _selectedItem;


        public Controller(IView<T> view, IList items)
        {
            _view = view;
            _items = items;
            view.SetController(this);
        }

        public IList Items
        {
#if PORTABLE
            get { return new List<T>((IEnumerable<T>)_items); }
#else
            get { return ArrayList.ReadOnly(_items); }
#endif
        }

        private void updateItemDetailValues(T item)
        {

            foreach (var field in typeof(T).GetFields())
            {
                var vfield = _view.GetType().GetField(field.Name);
                if (vfield != null)
                {
                    vfield.SetValue(_view, field.GetValue(item));
                }
            }

            foreach (var prop in typeof(T).GetProperties())
            {
                var vprop = _view.GetType().GetProperty(prop.Name);
                if (vprop != null)
                {
                    if (vprop.CanWrite)
                    {
                        vprop.SetValue(_view, prop.GetValue(item, null), null);
                    }
                }
            }

            //_view.FirstName   =  usr.FirstName;
            //_view.LastName    =  usr.LastName;
            //_view.ID          =  usr.ID;
            //_view.Department  =  usr.Department;
            //_view.Sex         =  usr.Sex;
        }

        private void updateItemWithViewValues(T item)
        {

            foreach (var field in _view.GetType().GetFields())
            {
                var vfield = typeof(T).GetField(field.Name);
                if (vfield != null)
                {
                    vfield.SetValue(_view, field.GetValue(item));
                }
            }

            foreach (var prop in _view.GetType().GetProperties())
            {
                var vprop = typeof(T).GetProperty(prop.Name);
                if (vprop != null)
                {
                    if (vprop.CanWrite)
                    {
                        vprop.SetValue(_view, prop.GetValue(item, null), null);
                    }
                }
            }

            //usr.FirstName     =  _view.FirstName;
            //usr.LastName      =  _view.LastName;
            //usr.ID            =  _view.ID;
            //usr.Department    =  _view.Department;
            //usr.Sex           =  _view.Sex;
        }

        public void LoadView()
        {
            _view.ClearGrid();
            foreach (T item in _items)
                _view.AddItemToGrid(item);

            _view.SetSelectedItemInGrid((T)_items[0]);

        }

        public void SelectedItemChanged(T selectedItem)
        {
            foreach (T item in this._items)
            {
                if (item.Equals(selectedItem))
                {
                    _selectedItem = item;
                    updateItemDetailValues(item);
                    _view.SetSelectedItemInGrid(item);
                    //this._view.CanModifyID = false;
                    break;
                }
            }
        }

        public void AddNewItem(object[] args)
        {

            _selectedItem = (T)Activator.CreateInstance(typeof(T), args);
            this.updateItemDetailValues(_selectedItem);
            //this._view.CanModifyID = true;
        }

        public void RemoveItem()
        {
            string id = this._view.GetIdOfSelectedItemInGrid();
            T itemToRemove = default(T);

            if (id != "")
            {
                foreach (T item in this._items)
                {
                    if (item.Equals(id))
                    {
                        itemToRemove = item;
                        break;
                    }
                }

                if (itemToRemove != null)
                {
                    int newSelectedIndex = this._items.IndexOf(itemToRemove);
                    this._items.Remove(itemToRemove);
                    this._view.RemoveItemFromGrid(itemToRemove);

                    if (newSelectedIndex > -1 && newSelectedIndex < _items.Count)
                    {
                        this._view.SetSelectedItemInGrid((T)_items[newSelectedIndex]);
                    }
                }
            }
        }

        public void Save()
        {
            updateItemWithViewValues(_selectedItem);
            if (!this._items.Contains(_selectedItem))
            {
                // Add new user
                this._items.Add(_selectedItem);
                this._view.AddItemToGrid(_selectedItem);
            }
            else
            {
                // Update existing
                this._view.UpdateGridWithChangedItem(_selectedItem);
            }
            _view.SetSelectedItemInGrid(_selectedItem);
            //this._view.CanModifyID = false;

        }

    }

}

