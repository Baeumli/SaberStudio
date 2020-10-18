using Prism.Ioc;
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

namespace SaberStudio.Modules.BeatSaver
{
    public class BeatSaverModule : IModule
    {
        private readonly IRegionManager regionManager;

        public BeatSaverModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var contentRegion = regionManager.Regions[Regions.ContentRegion];
            contentRegion.Add(containerProvider.Resolve<ViewA>());
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
        }
    }
}