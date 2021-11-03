using System.Diagnostics;
using Prism.Mvvm;

namespace SaberStudio.Domain.Models
{
    public class Setting : BindableBase
    {
        public Setting(ISettingsStore settingsStore)
        {
            
        }

        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DataType? DataType { get; set; }

        private object test;
        public object Value
        {
            get => test;
            set
            {
                SetProperty(ref test, value);
                Debug.WriteLine("Changed");
            }
        }
    }
}
