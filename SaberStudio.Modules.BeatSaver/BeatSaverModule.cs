﻿using Prism.Ioc;
using Prism.Modularity;
using SaberStudio.Services.BeatSaver;
using SaberStudio.Services.BeatSaver.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Unity.Microsoft.DependencyInjection;
using Prism.Unity;
using Prism.Regions;
using SaberStudio.Modules.BeatSaver.Views;
using SaberStudio.Core;
using SaberStudio.Modules.BeatSaver.ViewModels;

namespace SaberStudio.Modules.BeatSaver
{
    public class BeatSaverModule : IModule
    {
        private readonly IRegionManager regionManager;
        private ISidebarManager sidebarManager;

        public BeatSaverModule(IRegionManager regionManager, ISidebarManager sidebarManager)
        {
            this.regionManager = regionManager;
            this.sidebarManager = sidebarManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
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

            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();
            regionManager.RequestNavigate(Regions.ContentRegion, typeof(ViewA).Name);
        }
    }
}