using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model.MVC
{
    public interface IController<T> where T : IModel
    {

        IList Items { get; }

        void LoadView();

        void SelectedItemChanged(T selectedItem);

        void AddNewItem(object[] args);

        void RemoveItem();

        void Save();

    }
}
