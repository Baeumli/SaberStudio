using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Services.BeatSaver.Parser.Models;

namespace SaberStudio.Modules.Browser.ViewModels
{
    public class MapDetailViewModel : BindableBase, INavigationAware
    {
        private BeatMap beatMap;
        public BeatMap BeatMap
        {
            get { return beatMap; }
            set { SetProperty(ref beatMap, value); }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // check if its the same map
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            BeatMap = navigationContext.Parameters["BeatMap"] as BeatMap;
        }
    }
}
