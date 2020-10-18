using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SaberStudio.Services.Steam
{
    public class SteamService
    {
        public Dictionary<int, string> GetLibraryLocations(string steamAppsDirectory)
        {
            var libraryFolders = Path.Combine(steamAppsDirectory, "libraryfolders.vdf");

            if (File.Exists(libraryFolders))
            {
                using (var reader = new StreamReader(libraryFolders))
                {
                    var content = reader.ReadToEnd();
                    var matches = Regex.Matches(content, "\"(.*)\"\t*\"(.*)\"");
                    var locations = new Dictionary<int, string>();
                    foreach (Match match in matches)
                    {
                        if (int.TryParse(match.Groups[1].Value, out int key))
                        {
                            locations.Add(key, match.Groups[2].Value);
                        }
                    }
                    return locations;
                }
            }
            throw new FileNotFoundException("libraryfolders.vdf was not found");
        }

        public string GetBaseDirectory()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Valve\Steam"))
            {
                if (key != null)
                {
                    object value = key.GetValue("InstallPath");
                    if (value != null)
                    {
                        return value.ToString();
                    }
                }
            }
            throw new DirectoryNotFoundException("Steam installation folder was not found");
        }
    }
}
