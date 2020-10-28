using SaberStudio.Modules.Browser.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SaberStudio.Core;
using SaberStudio.Domain.Models.Sidebar;
using System.Collections.Generic;
using SaberStudio.Services.BeatSaver;
using SaberStudio.Modules.Browser.ViewModels;
using SaberStudio.Services.BeatSaver.Interfaces;
using System.Net.Http;
using Prism.Unity;
using Microsoft.Extensions.DependencyInjection;
using Unity.Microsoft.DependencyInjection;
using SaberStudio.Services.BeatMods.Interfaces;
using SaberStudio.Services.BeatMods;

namespace SaberStudio.Modules.Browser
{
    public class BrowserModule : IModule
    {
        private readonly IRegionManager regionManager;
        private ISidebarManager sidebarManager;

        public BrowserModule(IRegionManager regionManager, ISidebarManager sidebarManager)
        {
            this.regionManager = regionManager;
            this.sidebarManager = sidebarManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var group = new SidebarGroup
            {
                Title = "Browse",
                Items = new List<SidebarItem>()
                {
                    new SidebarItem() { Title = "Maps", TargetView = typeof(MapBrowserView).Name },
                    new SidebarItem() { Title = "Mods", TargetView = typeof(ModBrowserView).Name },
                }
            };
            sidebarManager.Add(group);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient<BeatSaverClient>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });

            serviceCollection.BuildServiceProvider(containerRegistry.GetContainer());

            containerRegistry.Register<IBeatSaverClient, BeatSaverClient>();
            containerRegistry.Register<IBeatModsClient, BeatModsClient>();

            containerRegistry.RegisterForNavigation<ModBrowserView, ModBrowserViewModel>();
            containerRegistry.RegisterForNavigation<MapBrowserView, MapBrowserViewModel>();
            containerRegistry.RegisterForNavigation<MapCategoryView, MapCategoryViewModel>();
            containerRegistry.RegisterForNavigation<MapDetailView, MapDetailViewModel>();
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();
            regionManager.RequestNavigate(Regions.ContentRegion, typeof(ViewA).Name);
        }
    }
}