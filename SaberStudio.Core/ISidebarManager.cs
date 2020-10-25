using SaberStudio.Domain.Models.Sidebar;
using System.Collections.Generic;

namespace SaberStudio.Core
{
    public interface ISidebarManager
    {
        public void Add(SidebarGroup group);
        public void AddRange(List<SidebarGroup> groups);
        public void Remove(SidebarGroup group);
        public IEnumerable<SidebarGroup> GetAll();
    }
}