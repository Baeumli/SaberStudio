using Prism.Mvvm;

namespace SaberStudio.Desktop.Models
{
    public class Setting : BindableBase
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DataType? DataType { get; set; }

        private object val;
        public object Value
        {
            get => val;
            set => SetProperty(ref val, value);
        }

    }
}
