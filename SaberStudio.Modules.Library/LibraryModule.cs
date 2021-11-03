using SaberStudio.Modules.Library.Views;
using Prism.Ioc;
using Prism.Modularity;
using SaberStudio.Services.BeatSaber;
using SaberStudio.Modules.Library.ViewModels;
using System.Collections.Generic;
using System.Linq;
using SaberStudio.Services.Steam;
using SaberStudio.UI.Interfaces;
using SaberStudio.UI.Models;

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
            var menu = new List<MenuItem>
            {
                new MenuItem() { Group = AppNavigationGroup.Library, Name = "Maps", Target = nameof(MapLibraryView), Icon = Icon.Cog },
                new MenuItem() { Group = AppNavigationGroup.Library, Name = "Mods", Target = "", Icon = Icon.Cog},
                new MenuItem() { Group = AppNavigationGroup.Library, Name = "Favorites", Target = "", Icon = Icon.Cog },
            };

            var beatSaberService = containerProvider.Resolve<IBeatSaberService>();

            var sidebarGroup = beatSaberService.GetPlaylists()
                .Select(playlist => new MenuItem()
                { Group = AppNavigationGroup.Playlists, Name = playlist.PlaylistTitle, Target = "" })
                .ToList();

            menu.AddRange(sidebarGroup);


            sidebarManager.AddRange(menu);
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