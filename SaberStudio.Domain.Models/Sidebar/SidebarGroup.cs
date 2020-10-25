using System.Collections.Generic;

namespace SaberStudio.Domain.Models.Sidebar
{
    public class SidebarGroup
    {
        public string Title { get; set; }
        public IEnumerable<SidebarItem> Items { get; set; }
    }
}
