using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using Ookii.Dialogs.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Desktop.Models;
using SaberStudio.Services.Settings.Interfaces;

namespace SaberStudio.Desktop.ViewModels
{
    public class SettingsViewModel : BindableBase, INavigationAware
    {
        private readonly ISettingsStore settingsStore;

        #region Properties

        public BindingList<Setting> Settings { get; set; }

        #endregion

        public SettingsViewModel(ISettingsStore settingsStore)
        {
            this.settingsStore = settingsStore;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Services.Settings.Models.Setting, Setting>());
            var mapper = config.CreateMapper();
            var settings = this.settingsStore.GetSettings().ToList();

            Settings = mapper.Map<BindingList<Setting>>(settings);
        }

        #region Commands

        private DelegateCommand<Setting> updateSettingCommand;
        public DelegateCommand<Setting> UpdateSettingCommand => updateSettingCommand ??= new DelegateCommand<Setting>(ExecuteUpdateSettingCommand);

        private void ExecuteUpdateSettingCommand(Setting setting)
        {
            settingsStore.UpdateValue(setting.Key, setting.Value);
        }

        private DelegateCommand<Setting> browseFolderCommand;
        public DelegateCommand<Setting> BrowseFolderCommand => browseFolderCommand ??= new DelegateCommand<Setting>(ExecuteBrowseFolderCommand);

        private void ExecuteBrowseFolderCommand(Setting setting)
        {
            var dialog = new VistaFolderBrowserDialog()
            {
                Description = "Select Beat Saber installation folder",
                UseDescriptionForTitle = true
            };

            var result = dialog.ShowDialog();

            if (result ?? false)
            {
                setting.Value = dialog.SelectedPath;
                settingsStore.UpdateValue(setting.Key, dialog.SelectedPath);
            }
        }

        #endregion

        #region Navigation

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        #endregion

        #region Events
            
        #endregion

    }
}
