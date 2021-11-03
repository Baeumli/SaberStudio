namespace SaberStudio.UI.Models
{
    public class MenuItem
    {
        public AppNavigationGroup? Group { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Target { get; set; }
        public object Parameter { get; set; }
    }
}
