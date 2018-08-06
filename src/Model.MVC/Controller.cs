using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Platform.Model
{
#if PORTABLE

    namespace Core
    {
#endif

        namespace MVC
        {
            public class Controller<T> : IController<T> where T : IModel
            {
                private IView<T> view;
                private IList items;
                private T selectedItem;

                public Controller(IView<T> view, IList items)
                {
                    this.view = view;
                    this.items = items;
                    view.SetController(this);
                }

                public IList Items
                {
#if PORTABLE
                    get { return new List<T>((IEnumerable<T>)items); }
#else
                get { return ArrayList.ReadOnly(items); }
#endif
                }

                private void updateItemDetailValues(T item)
                {
#if NETFX_45
                    var fcollection = typeof(T).GetRuntimeFields();
#else
                    var fcollection = typeof(T).GetFields();
#endif
                    foreach (var field in fcollection)
                    {
#if NETFX_45
                        var vfield = view.GetType().GetRuntimeField(field.Name);
#else
                        var vfield = view.GetType().GetField(field.Name);
#endif
                        if (vfield != null)
                            vfield.SetValue(view, field.GetValue(item));
                    }

#if NETFX_45
                    var pcollection = typeof(T).GetRuntimeProperties();
#else
                    var pcollection = typeof(T).GetProperties();
#endif
                    foreach (var prop in pcollection)
                    {
#if NETFX_45
                        var vprop = view.GetType().GetRuntimeProperty(prop.Name);
#else
                        var vprop = view.GetType().GetProperty(prop.Name);
#endif
                        if (vprop != null && vprop.CanWrite)
                            vprop.SetValue(view, prop.GetValue(item, null), null);
                    }
                }

                private void updateItemWithViewValues(T item)
                {
#if NETFX_45
                    var fcollection = view.GetType().GetRuntimeFields();
#else
                    var fcollection = view.GetType().GetFields();
#endif

                    foreach (var field in fcollection)
                    {
#if NETFX_45
                        var vfield = typeof(T).GetRuntimeField(field.Name);
#else
                        var vfield = typeof(T).GetField(field.Name);
#endif
                        if (vfield != null)
                            vfield.SetValue(view, field.GetValue(item));
                    }

#if NETFX_45
                    var pcollection = view.GetType().GetRuntimeProperties();
#else
                    var pcollection = view.GetType().GetProperties();
#endif
                    foreach (var prop in pcollection)
                    {
#if NETFX_45
                        var vprop = typeof(T).GetRuntimeProperty(prop.Name);
#else
                        var vprop = typeof(T).GetProperty(prop.Name);
#endif
                        if (vprop != null && vprop.CanWrite)
                            vprop.SetValue(view, prop.GetValue(item, null), null);
                    }
                }

                public void LoadView()
                {
                    view.ClearGrid();
                    foreach (T item in items)
                        view.AddItemToGrid(item);

                    view.SetSelectedItemInGrid((T)items[0]);
                }

                public void SelectedItemChanged(T selectedItem)
                {
                    foreach (T item in items)
                    {
                        if (item.Equals(selectedItem))
                        {
                            selectedItem = item;
                            updateItemDetailValues(item);
                            view.SetSelectedItemInGrid(item);
                            break;
                        }
                    }
                }

                public void AddNewItem(object[] args)
                {
                    selectedItem = (T)Activator.CreateInstance(typeof(T), args);
                    updateItemDetailValues(selectedItem);
                }

                public void RemoveItem()
                {
                    string id = view.GetIdOfSelectedItemInGrid();
                    T itemToRemove = default(T);

                    if (id != "")
                    {
                        foreach (T item in items)
                        {
                            if (item.Equals(id))
                            {
                                itemToRemove = item;
                                break;
                            }
                        }

                        if (itemToRemove != null)
                        {
                            int newSelectedIndex = items.IndexOf(itemToRemove);
                            items.Remove(itemToRemove);
                            view.RemoveItemFromGrid(itemToRemove);

                            if (newSelectedIndex > -1 && newSelectedIndex < items.Count)
                            {
                                view.SetSelectedItemInGrid((T)items[newSelectedIndex]);
                            }
                        }
                    }
                }

                public void Save()
                {
                    updateItemWithViewValues(selectedItem);
                    if (!items.Contains(selectedItem))
                    {
                        // Add new
                        items.Add(selectedItem);
                        view.AddItemToGrid(selectedItem);
                    }
                    else
                    {
                        // Update existing
                        view.UpdateGridWithChangedItem(selectedItem);
                    }
                    view.SetSelectedItemInGrid(selectedItem);
                }
            }
        }

#if PORTABLE
    }

#endif
}