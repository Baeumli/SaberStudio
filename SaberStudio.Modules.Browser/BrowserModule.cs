using SaberStudio.Modules.Browser.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
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
using SaberStudio.Services.Settings.Interfaces;
using SaberStudio.UI;
using SaberStudio.UI.Interfaces;
using SaberStudio.UI.Models;
using Localization = SaberStudio.Core.Resources.Localization;
using SaberStudio.Services.Settings.Models;
using SaberStudio.UI.Dialogs;
using SaberStudio.UI.Dialogs.ViewModels;
using SaberStudio.UI.Dialogs.Views;

namespace SaberStudio.Modules.Browser
{
    public class BrowserModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly ISidebarManager sidebarManager;

        public BrowserModule(IRegionManager regionManager, ISidebarManager sidebarManager)
        {
            this.regionManager = regionManager;
            this.sidebarManager = sidebarManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            RegisterSettings(containerProvider.Resolve<ISettingsStore>());

            var menu = new List<MenuItem>()
            {
                new MenuItem() {Group = AppNavigationGroup.Browse, Name = "Maps", Target = nameof(MapBrowserView), Icon = Icon.Cog },
                new MenuItem() {Group = AppNavigationGroup.Browse, Name = "Mods", Target = nameof(ModBrowserView), Icon = Icon.Cog }
            };

            sidebarManager.AddRange(menu);

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
            containerRegistry.RegisterForNavigation<ModDetailView, ModDetailViewModel>();
            containerRegistry.RegisterForNavigation<MapBrowserView, MapBrowserViewModel>();
            containerRegistry.RegisterForNavigation<MapDetailView, MapDetailViewModel>();
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();

            containerRegistry.RegisterDialogWindow<AdonisDialogWindow>();
            containerRegistry.RegisterDialog<VersionMismatchDialog, VersionMismatchDialogViewModel>();

            regionManager.RequestNavigate(Regions.ContentRegion, nameof(ViewA));
        }

        public void RegisterSettings(ISettingsStore settingsStore)
        {
            settingsStore.RegisterSetting("beatsaber.installdir", Localization.Settings_BeatSaberDirectory, Localization.Settings_BeatSaberDirectoryDescription, DataType.Path);
            settingsStore.RegisterSetting("test.boolvalue", "Test bool value", "bool description", DataType.Bool, true);
        }
    }
}