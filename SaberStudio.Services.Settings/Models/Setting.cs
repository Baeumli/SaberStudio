namespace SaberStudio.Services.Settings.Models
{
    public class Setting
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DataType? DataType { get; set; }
    }
}
