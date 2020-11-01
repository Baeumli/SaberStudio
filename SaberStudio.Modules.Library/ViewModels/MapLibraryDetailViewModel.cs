using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Services.BeatSaber.Parser.Models;

namespace SaberStudio.Modules.Library.ViewModels
{
    public class MapLibraryDetailViewModel : BindableBase, INavigationAware
    {
        private BeatMap beatMap;
        public BeatMap BeatMap
        {
            get => beatMap;
            set => SetProperty(ref beatMap, value);
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var newMap = navigationContext.Parameters[nameof(BeatMap)] as BeatMap;
            return newMap == BeatMap;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            BeatMap = navigationContext.Parameters[nameof(BeatMap)] as BeatMap;
        }
    }
}
