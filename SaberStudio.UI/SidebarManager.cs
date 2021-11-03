using System.Collections.Generic;
using SaberStudio.UI.Interfaces;
using SaberStudio.UI.Models;

namespace SaberStudio.UI
{
    public class SidebarManager : ISidebarManager
    {
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        public void Add(MenuItem group)
        {
            MenuItems.Add(group);
        }

        public void AddRange(IEnumerable<MenuItem> groups)
        {
            MenuItems.AddRange(groups);
        }

        public IEnumerable<MenuItem> GetAll()
        {
            return MenuItems;
        }

        public void Remove(MenuItem group)
        {
            MenuItems.Remove(group);
        }
    }
}
