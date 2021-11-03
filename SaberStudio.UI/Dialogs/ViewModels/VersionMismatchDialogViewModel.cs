using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace SaberStudio.UI.Dialogs.ViewModels
{
    public class VersionMismatchDialogViewModel: BindableBase, IDialogAware
    {
        public string Title => "Version mismatch detected!";

        private bool showDialogAgain;
        public bool ShowDialogAgain
        {
            get => showDialogAgain;
            set => SetProperty(ref showDialogAgain, value);
        }

        private DelegateCommand<string> closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand => closeDialogCommand ??= new DelegateCommand<string>(ExecuteCloseDialogCommand);

        private void ExecuteCloseDialogCommand(string result)
        {
            if (Enum.TryParse(result, out ButtonResult enumResult))
            {
                RequestClose?.Invoke(new DialogResult(enumResult));
            }
        }


        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        public event Action<IDialogResult> RequestClose;
    }
}
