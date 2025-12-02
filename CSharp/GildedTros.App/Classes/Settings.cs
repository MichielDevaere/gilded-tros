using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Text.Json;

namespace GildedTros.App.Classes
{

    public class QualityChangeSettings
    {
        public int BeforeSellInExpired { get; set; }
        public int AfterSellInExpired { get; set; }
    }

    public class Settings
    {
        private static readonly Settings _settings;
        static Settings()
        {
            try
            {
                var json = File.ReadAllText("settings.json");
                _settings = JsonSerializer.Deserialize<Settings>(json);

                if (_settings == null
                    || _settings.Degradation == null
                    || _settings.Improvement == null
                    || _settings.MaxQuality == 0)
                    throw new JsonException("Invalid settings.json file");
            }
            catch
            {
                throw new JsonException("Invalid settings.json file");
            }
        }

        public QualityChangeSettings Degradation { get; set; }
        public QualityChangeSettings Improvement { get; set; }

        public int MaxQuality { get; set; }
        public Dictionary<string, Settings> SpecialItems { get; set; }

        public static Settings GetSettings()
        {
            return _settings;
        }

        public static Settings GetSpecialItemSettings(string itemType)
        {
            var deafultSettings = GetSettings();
            var settings = deafultSettings.SpecialItems.TryGetValue(itemType, out var specialSettings) ? specialSettings : deafultSettings;
            if (settings.MaxQuality == 0) settings.MaxQuality = deafultSettings.MaxQuality;

            return settings;

        }
    }
}
