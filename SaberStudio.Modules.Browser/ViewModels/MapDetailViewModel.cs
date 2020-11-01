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
            get => beatMap;
            set => SetProperty(ref beatMap, value);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var newMap = navigationContext.Parameters["BeatMap"] as BeatMap;   
            return newMap == BeatMap;
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
