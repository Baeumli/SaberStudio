using SaberStudio.Modules.Library.Views;
using Prism.Ioc;
using Prism.Modularity;
using SaberStudio.Services.BeatSaber;
using SaberStudio.Modules.Library.ViewModels;
using SaberStudio.Core;
using SaberStudio.Domain.Models.Sidebar;
using System.Collections.Generic;
using SaberStudio.Services.Steam;

namespace SaberStudio.Modules.Library
{
    public class LibraryModule : IModule
    {
        private readonly ISidebarManager sidebarManager;

        public LibraryModule(ISidebarManager sidebarManager)
        {
            this.sidebarManager = sidebarManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var group = new SidebarGroup
            {
                Title = "Library",
                Items = new List<SidebarItem>()
                {
                    new SidebarItem() { Title = "Maps", TargetView = nameof(MapLibraryView) },
                    new SidebarItem() { Title = "Mods" },
                    new SidebarItem() { Title = "Favorites" }
                }
            };
            sidebarManager.Add(group);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IBeatSaberService, BeatSaberService>();
            containerRegistry.Register<ISteamService, SteamService>();

            containerRegistry.RegisterForNavigation<MapLibraryView, MapLibraryViewModel>();
            containerRegistry.RegisterForNavigation<MapLibraryDetailView, MapLibraryDetailViewModel>();
        }
    }
}