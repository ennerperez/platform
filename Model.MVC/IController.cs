using System.Collections;

namespace Platform.Model
{
#if PORTABLE

    namespace Core
    {
#endif

        namespace MVC
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

#if PORTABLE
    }

#endif
}