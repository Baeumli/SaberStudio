using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SaberStudio.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }
    }
}
