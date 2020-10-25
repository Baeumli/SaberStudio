using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Core;
using SaberStudio.Domain.Models.Sidebar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaberStudio.Modules.Sidebar.ViewModels
{
    public class SidebarViewModel : BindableBase
    {
        public List<SidebarGroup> Groups { get; set; }

        private IRegionManager regionManager;

        public SidebarViewModel(IRegionManager regionManager, ISidebarManager sidebarManager)
        {
            this.regionManager = regionManager;
            Groups = sidebarManager.GetAll().ToList();
            var x = new List<SidebarGroup>()
            {
                new SidebarGroup()
                {
                    Title = "Library",
                    Items = new List<SidebarItem>()
                    {
                        new SidebarItem() { Title = "Maps" },
                        new SidebarItem() { Title = "Mods" },
                        new SidebarItem() { Title = "Favorites" }
                    }
                },
                new SidebarGroup()
                {
                    Title = "Playlists",
                    Items = new List<SidebarItem>()
                    {
                        new SidebarItem() { Title = "My Playlist 1" },
                        new SidebarItem() { Title = "My Playlist 2" },
                        new SidebarItem() { Title = "My Playlist 3" }
                    }
                }
            };
            Groups.AddRange(x);
        }

        private DelegateCommand<string> selectedCommand;

        public DelegateCommand<string> SelectedCommand => selectedCommand ??= new DelegateCommand<string>(ExecuteSelectedCommand);

        private void ExecuteSelectedCommand(string targetView)
        {
            if (string.IsNullOrEmpty(targetView))
                throw new ArgumentNullException();

            var activeView = regionManager.Regions[Regions.ContentRegion].ActiveViews.FirstOrDefault();

            if (activeView.GetType().Name != targetView)
            {
                regionManager.RequestNavigate(Regions.ContentRegion, targetView);
            }
        }
    }
}
