using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Telerik.DataSource.Extensions;

namespace Totalview.BlazorClient.Client
{
    public class SmartObservableCollection<T> : ObservableCollection<T>
    {
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection?.Any() != true)
                return;

            Items.AddRange(collection);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, collection));
        }

        public void Replace(int index, T item)
        {
            SetItem(index, item);
        }
    }
}
