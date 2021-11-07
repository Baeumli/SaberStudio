using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberStudio.Modules.Browser.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        public ViewAViewModel()
        {
            Message = "View A from your Prism Module";
        }
    }
}
