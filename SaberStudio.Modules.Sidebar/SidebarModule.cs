using SaberStudio.Modules.Sidebar.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SaberStudio.Core;

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
            var sidebarRegion = regionManager.Regions[Regions.LeftRegion];
            sidebarRegion.Add(containerProvider.Resolve<SidebarView>());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }


    }
}