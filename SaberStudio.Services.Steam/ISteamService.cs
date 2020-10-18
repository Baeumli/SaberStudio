using System;
using System.Collections.Generic;
using System.Text;

namespace SaberStudio.Services.Steam
{
    public interface ISteamService
    {
        string GetBaseDirectory();
        Dictionary<int, string> GetLibraryLocations(string steamAppsDirectory);
    }
}
