﻿using Prism.Mvvm;

namespace SaberStudio.Desktop.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Saber Studio";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
