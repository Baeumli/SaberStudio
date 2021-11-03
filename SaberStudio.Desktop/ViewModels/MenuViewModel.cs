using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.UI;
using SaberStudio.UI.Interfaces;
using SaberStudio.UI.Models;

namespace SaberStudio.Desktop.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        private readonly IRegionNavigationService navigationService;
        private readonly IRegionManager regionManager;

        #region Properties 

        public ICollectionView PagesCollectionView { get; }
        public List<MenuItem> MenuItems { get; set; }

        private MenuItem selectedMenuItem;
        public MenuItem SelectedMenuItem
        {
            get => selectedMenuItem;
            set => SetProperty(ref selectedMenuItem, value);
        }

        #endregion

        public MenuViewModel(IRegionManager regionManager, ISidebarManager sidebarManager)
        {
            this.regionManager = regionManager;

            navigationService = this.regionManager.Regions[Regions.ContentRegion].NavigationService;
            navigationService.Navigated += ContentRegionNavigatedEvent;

            MenuItems = sidebarManager.GetAll().ToList();

            PagesCollectionView = CollectionViewSource.GetDefaultView(MenuItems);
            PagesCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
        }

        private void ContentRegionNavigatedEvent(object sender, RegionNavigationEventArgs e)
        {
            if (MenuItems.Any(x => x.Target == e.Uri.ToString()))
            {
                SelectedMenuItem = MenuItems.SingleOrDefault(x => x.Target == e.Uri.ToString());
                return;
            }

            SelectedMenuItem = null;
        }

        #region Commands 

        private DelegateCommand<object[]> selectedCommand;
        public DelegateCommand<object[]> SelectedCommand => selectedCommand ??= new DelegateCommand<object[]>(ExecuteSelectedCommand);

        private void ExecuteSelectedCommand(object[] addedItems)
        {
            if (addedItems == null || addedItems.Length == 0)
                return;

            if (!(addedItems[0] is MenuItem menuitem))
                return;

            if (string.IsNullOrEmpty(menuitem.Target))
                return;

            var activeView = regionManager.Regions[Regions.ContentRegion].ActiveViews.FirstOrDefault();

            if (activeView != null && activeView.GetType().Name != menuitem.Target)
            {
                regionManager.RequestNavigate(Regions.ContentRegion, menuitem.Target);
            }
        }

        #endregion

    }
}
