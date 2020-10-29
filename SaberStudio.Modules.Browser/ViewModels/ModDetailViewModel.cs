using Prism.Mvvm;
using Prism.Regions;
using SaberStudio.Services.BeatMods.Models.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaberStudio.Modules.Browser.ViewModels
{
    public class ModDetailViewModel : BindableBase, INavigationAware
    {
        private Mod mod;
        public Mod Mod
        {
            get { return mod; }
            set { SetProperty(ref mod, value); }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var newMod = navigationContext.Parameters["Mod"] as Mod;
            return newMod == Mod;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Mod = navigationContext.Parameters["Mod"] as Mod;

        }
    }
}
