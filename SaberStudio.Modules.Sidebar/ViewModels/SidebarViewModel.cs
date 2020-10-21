using Prism.Mvvm;
using SaberStudio.Modules.Sidebar.Models;
using System.Collections.Generic;

namespace SaberStudio.Modules.Sidebar.ViewModels
{
    public class SidebarViewModel : BindableBase
    {
        public IEnumerable<SidebarGroup> Groups { get; set; }

        public SidebarViewModel()
        {
            Groups = new List<SidebarGroup>()
            {
                new SidebarGroup()
                {
                    Title = "Library",
                    Items = new List<SidebarItem>()
                    {
                        new SidebarItem() { Title = "Maps" }
                    }
                }
            };
        }
    }
}
