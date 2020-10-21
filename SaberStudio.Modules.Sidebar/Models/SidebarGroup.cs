using System;
using System.Collections.Generic;
using System.Text;

namespace SaberStudio.Modules.Sidebar.Models
{
    public class SidebarGroup
    {
        public string Title { get; set; }
        public IEnumerable<SidebarItem> Items { get; set; }
    }
}
