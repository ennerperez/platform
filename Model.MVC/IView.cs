using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model
{
#if PORTABLE
    namespace Core
    {
#endif
        namespace MVC { 
        public interface IView<T> where T : IModel
        {

            void SetController(Controller<T> controller);
            void ClearGrid();
            void AddItemToGrid(T item);
            void UpdateGridWithChangedItem(T item);
            void RemoveItemFromGrid(T item);
            string GetIdOfSelectedItemInGrid();
            void SetSelectedItemInGrid(T item);


        }
    }
#if PORTABLE
    }
#endif
        }
