using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SaberStudio.Services.Settings.Interfaces;
using SaberStudio.Services.Settings.Models;

namespace SaberStudio.Services.Settings
{
    public class SettingsStore : ISettingsStore
    {
        public HashSet<Setting> Settings { get; set; } = new HashSet<Setting>();

        public void RegisterSetting(string key, string name, string description, DataType? dataType, object value)
        {
            if (Settings.Any(x => x.Key == key))
                throw new DuplicateNameException();

            var setting = new Setting()
            {
                Key = key,
                Value = value,
                Name = name,
                Description = description,
                DataType = dataType,
            };

            Settings.Add(setting);
            SaveSettingsToFile();
        }

        public void Remove(string key)
        {
            if (Settings.All(x => x.Key != key))
                throw new KeyNotFoundException();

            var setting = Settings.First(item => item.Key == key);

            Settings.Remove(setting);
            SaveSettingsToFile();
        }

        public HashSet<Setting> GetSettings()
        {
            return Settings;
        }

        public void UpdateValue(string key, object value)
        {
            if (Settings.All(x => x.Key != key))
                throw new KeyNotFoundException();

            var setting = Settings.First(item => item.Key == key);

            if (setting.Value == value) 
                return;

            setting.Value = value;
            SaveSettingsToFile();
        }

        public object GetValueByKey(string key)
        {
            if (Settings.All(x => x.Key != key))
                throw new KeyNotFoundException();

            var setting = Settings.First(item => item.Key == key);

            return setting.Value;
        }

        private void SaveSettingsToFile()
        {
            var dictionary = Settings.ToDictionary(x => x.Key, x => x.Value);

            var json = JsonConvert.SerializeObject(dictionary);
            var configFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "config.json");
            File.WriteAllText(configFile, json);
        }

        private void GetSettingsFromFile()
        {
            var configFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "config.json");

            string json;
            using (var reader = File.OpenText(configFile))
            {
                json = reader.ReadToEnd();
            }

            var jsonSettings = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            foreach (var setting in Settings)
            {
                foreach (var jsonSetting in jsonSettings)
                {
                    if (jsonSettings.ContainsKey(setting.Key))
                    {
                        setting.Value = jsonSetting.Value;
                    }
                }
            }
        }
    }
}
