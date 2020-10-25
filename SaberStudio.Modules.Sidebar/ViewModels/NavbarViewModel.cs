using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Core;

namespace SaberStudio.Modules.Sidebar.ViewModels
{
    public class NavbarViewModel : BindableBase
    {
        private readonly IRegionNavigationService navigationService;

        private DelegateCommand navigateBackCommand;
        private DelegateCommand navigateForwardCommand;
        public DelegateCommand NavigateBackCommand => navigateBackCommand ??= new DelegateCommand(ExecuteNavigateBackCommand, CanGoBack);
        public DelegateCommand NavigateForwardCommand => navigateForwardCommand ??= new DelegateCommand(ExecuteNavigateForwardCommand, CanGoForward);

        public NavbarViewModel(IRegionManager regionManager)
        {
            navigationService = regionManager.Regions[Regions.ContentRegion].NavigationService;
            navigationService.Navigated += ContentRegionNavigatedEvent;
        }

        private bool CanGoBack()
        {
            return navigationService.Journal != null && navigationService.Journal.CanGoBack;
        }

        private bool CanGoForward()
        {
            return navigationService.Journal != null && navigationService.Journal.CanGoForward;
        }

        private void ExecuteNavigateBackCommand()
        {
            if (CanGoBack())
            {
                navigationService.Journal.GoBack();
            }
        }

        private void ExecuteNavigateForwardCommand()
        {
            if(CanGoForward())
            {
                navigationService.Journal.GoForward();
            }
        }

        private void ContentRegionNavigatedEvent(object sender, RegionNavigationEventArgs e)
        {
            NavigateBackCommand.RaiseCanExecuteChanged();
            NavigateForwardCommand.RaiseCanExecuteChanged();
        }
    }
}
