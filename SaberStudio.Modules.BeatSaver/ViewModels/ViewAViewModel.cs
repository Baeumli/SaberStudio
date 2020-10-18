using Prism.Mvvm;
using SaberStudio.Services.BeatSaver.Interfaces;
using System.Linq;
using System.Threading;

namespace SaberStudio.Modules.BeatSaver.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _message;
        private IBeatSaverClient client;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ViewAViewModel(IBeatSaverClient client)
        {
            this.client = client;
            GetMaps();
        }

        private async void GetMaps()
        {
            var maps = await client.GetMostDownloadedMaps(CancellationToken.None);
            Message = maps.First().MapName;
        }


    }
}
