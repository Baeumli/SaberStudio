using System.ComponentModel;
using System.Reflection;
using Prism.Ioc;
using Prism.Modularity;
using SaberStudio.Desktop.Views;
using SaberStudio.Modules.Browser;
using SaberStudio.Modules.Library;
using System.Windows;
using Prism.Regions;
using SaberStudio.Desktop.ViewModels;
using SaberStudio.Services.Settings;
using SaberStudio.Services.Settings.Interfaces;
using SaberStudio.UI;
using SaberStudio.UI.Interfaces;

namespace SaberStudio.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private FieldInfo menuDropAlignmentField;

        protected override Window CreateShell()
        {
            // Align ContextMenu Popup from left to right https://stackoverflow.com/a/22065976
            menuDropAlignmentField = typeof(SystemParameters).GetField("_menuDropAlignment", BindingFlags.NonPublic | BindingFlags.Static);
            System.Diagnostics.Debug.Assert(menuDropAlignmentField != null);

            EnsureStandardPopupAlignment();
            SystemParameters.StaticPropertyChanged += SystemParameters_StaticPropertyChanged;

            var settingsStore = Container.Resolve<ISettingsStore>();
            settingsStore.GetSettings();

            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISidebarManager, SidebarManager>();
            containerRegistry.RegisterSingleton<ISettingsStore, SettingsStore>();
            
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<BrowserModule>();
            moduleCatalog.AddModule<LibraryModule>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var regionManager = Container.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(Regions.LeftRegion, typeof(MenuView));
            regionManager.RegisterViewWithRegion(Regions.TopRegion, typeof(NavbarView));
        }


        private void SystemParameters_StaticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            EnsureStandardPopupAlignment();
        }

        private void EnsureStandardPopupAlignment()
        {
            if (SystemParameters.MenuDropAlignment && menuDropAlignmentField != null)
            {
                menuDropAlignmentField.SetValue(null, false);
            }
        }
    }
}
