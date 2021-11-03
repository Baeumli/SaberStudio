using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Desktop.Views;
using SaberStudio.Services.BeatSaber;
using SaberStudio.UI;

namespace SaberStudio.Desktop.ViewModels
{
    public class NavbarViewModel : BindableBase
    {
        private readonly IRegionNavigationService navigationService;
        private readonly IRegionManager regionManager;
        private readonly IBeatSaberService beatSaberService;

        public string GameVersion => beatSaberService.GetGameVersion();

        public NavbarViewModel(IRegionManager regionManager, IBeatSaberService beatSaberService)
        {
            this.regionManager = regionManager;
            this.beatSaberService = beatSaberService;

            navigationService = this.regionManager.Regions[Regions.ContentRegion].NavigationService;
            navigationService.Navigated += ContentRegionNavigatedEvent;
        }

        private void ContentRegionNavigatedEvent(object sender, RegionNavigationEventArgs e)
        {
            NavigateBackCommand.RaiseCanExecuteChanged();
            NavigateForwardCommand.RaiseCanExecuteChanged();
        }

        #region Commands

        private DelegateCommand navigateBackCommand;
        public DelegateCommand NavigateBackCommand => navigateBackCommand ??= new DelegateCommand(ExecuteNavigateBackCommand, CanGoBack);

        private void ExecuteNavigateBackCommand()
        {
            if (CanGoBack())
            {
                navigationService.Journal.GoBack();
            }
        }

        private bool CanGoBack()
        {
            return navigationService.Journal is { CanGoBack: true };
        }

        private DelegateCommand navigateForwardCommand;
        public DelegateCommand NavigateForwardCommand => navigateForwardCommand ??= new DelegateCommand(ExecuteNavigateForwardCommand, CanGoForward);

        private void ExecuteNavigateForwardCommand()
        {
            if (CanGoForward())
            {
                navigationService.Journal.GoForward();
            }
        }

        private bool CanGoForward()
        {
            return navigationService.Journal is { CanGoForward: true };
        }

        private DelegateCommand goToSettingsCommand;
        public DelegateCommand GoToSettingsCommand => goToSettingsCommand ??= new DelegateCommand(ExecuteGoToSettingsCommand);

        private void ExecuteGoToSettingsCommand()
        {
            var currentView = regionManager.Regions["ContentRegion"].ActiveViews.FirstOrDefault();
            if (currentView != null && currentView.GetType().Name != nameof(SettingsView))
            {
                regionManager.RequestNavigate(Regions.ContentRegion, nameof(SettingsView));
            }
        }

        #endregion

    }
}
