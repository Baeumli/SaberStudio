using System;

namespace SaberStudio.Domain.Models.Components
{
    public class Tile
    {
        public string Content { get; set; }
        public string TargetView { get; set; }
        public Uri ImageUri { get; set; }
        public string NavigationPath { get; set; }
    }
}
