using System.Collections.Generic;
using SaberStudio.UI.Models;

namespace SaberStudio.UI.Interfaces
{
    public interface ISidebarManager
    {
        public void Add(MenuItem item);
        public void AddRange(IEnumerable<MenuItem> items);
        public void Remove(MenuItem item);
        public IEnumerable<MenuItem> GetAll();
    }
}