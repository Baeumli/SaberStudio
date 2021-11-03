using System.Collections.Generic;
using SaberStudio.Services.Settings.Models;

namespace SaberStudio.Services.Settings.Interfaces
{
    public interface ISettingsStore
    {
        public void RegisterSetting(string key, string name, string description, DataType? dataType, object value = null);
        public void Remove(string key);
        public HashSet<Setting> GetSettings();
        public void UpdateValue(string key, object value);
        public object GetValueByKey(string key);

    }
}
