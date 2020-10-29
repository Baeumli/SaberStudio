using Prism.Ioc;
using Prism.Modularity;
using SaberStudio.Core;
using SaberStudio.Desktop.Views;
using SaberStudio.Modules.Browser;
using SaberStudio.Modules.Library;
using SaberStudio.Modules.Sidebar;
using System.Windows;

namespace SaberStudio.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISidebarManager, SidebarManager>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule(typeof(BrowserModule));
            moduleCatalog.AddModule(typeof(LibraryModule));
            moduleCatalog.AddModule(typeof(SidebarModule));
        }
    }
}
