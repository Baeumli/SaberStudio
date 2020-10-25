using SaberStudio.Modules.Sidebar.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SaberStudio.Core;
using SaberStudio.Modules.Sidebar.ViewModels;

namespace SaberStudio.Modules.Sidebar
{
    public class SidebarModule : IModule
    {
        private readonly IRegionManager regionManager;

        public SidebarModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            regionManager.RegisterViewWithRegion(Regions.TopRegion, typeof(NavbarView));
            regionManager.RegisterViewWithRegion(Regions.LeftRegion, typeof(SidebarView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        
        }
    }
}