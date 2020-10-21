using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SaberStudio.Core;
using SaberStudio.Desktop.Views;
using SaberStudio.Modules.BeatSaver;
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
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule(typeof(BeatSaverModule));
            moduleCatalog.AddModule(typeof(SidebarModule));
        }
    }
}
