using System.Collections.Generic;
using System.IO;
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
                    || _settings.DefaultDegradation == null
                    || _settings.MaxQuality == 0)
                    throw new JsonException("Invalid settings.json file");
            }
            catch
            {
                throw new JsonException("Invalid settings.json file");
            }
        }

        public QualityChangeSettings DefaultDegradation { get; set; }
        public int MaxQuality { get; set; }
        public Dictionary<string, int> SpecialMaxQuality { get; set; }

        public static Settings GetSettings()
        {
            return _settings;
        }

        public static int GetMaxQuality(string itemType)
        {
            var defaultSettings = GetSettings();
            var maxQuality = defaultSettings.SpecialMaxQuality.TryGetValue(itemType, out var specialSettings) ? specialSettings : defaultSettings.MaxQuality;
            return maxQuality;

        }
    }
}
