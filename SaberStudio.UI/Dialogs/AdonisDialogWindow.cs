using System.Windows;
using AdonisUI.Controls;
using Prism.Services.Dialogs;

namespace SaberStudio.UI.Dialogs
{
    public partial class AdonisDialogWindow : AdonisWindow, IDialogWindow
    {
        public IDialogResult Result { get; set; }
    }
}
