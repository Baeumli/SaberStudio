using SaberStudio.Domain.Models.Sidebar;
using System.Collections.Generic;

namespace SaberStudio.Core
{
    public class SidebarManager : ISidebarManager
    {
        public List<SidebarGroup> Groups { get; set; } = new List<SidebarGroup>();

        public void Add(SidebarGroup group)
        {
            Groups.Add(group);
        }

        public void AddRange(List<SidebarGroup> groups)
        {
            Groups.AddRange(groups);
        }

        public IEnumerable<SidebarGroup> GetAll()
        {
            return Groups;
        }

        public void Remove(SidebarGroup group)
        {
            Groups.Remove(group);
        }
    }
}
