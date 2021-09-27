using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager_CS_WPF
{
    public static class PublicStuffClass
    {
        public static int RemoveAllOnCondition<T>(this ObservableCollection<T> collection, Func<T, bool> condition)
        {
            if (collection is null) return -1;
            List<T> items_to_remove = collection.Where(condition).ToList();

            foreach (T item in items_to_remove)
            {
                collection.Remove(item);
            }

            return items_to_remove.Count;
        }
    }
}
