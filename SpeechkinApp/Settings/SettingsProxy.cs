using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp.Settings
{
    public class SettingsProxy:ISpeechSettings
    {
        private readonly IsolatedStorageFacade _isolatedStorage;

        public SettingsProxy(IsolatedStorageFacade isolatedStorage)
        {
            _isolatedStorage = isolatedStorage;
        }

        public string AzureSpeechPrimaryKey { get; private set; }

        public string AzureSpeechSecondaryKey { get; private set; }

        public string AzureSpeechAuthUrl { get; private set; }

        public string SpeechLanguage { get; private set; }

        public void Load()
        {
            AzureSpeechPrimaryKey = _isolatedStorage.GetData(nameof(AzureSpeechPrimaryKey));
            AzureSpeechSecondaryKey = _isolatedStorage.GetData(nameof(AzureSpeechSecondaryKey));
            AzureSpeechAuthUrl = SpeechkinAppSettings.Default.SpeechAuthUrl;
            SpeechLanguage = SpeechkinAppSettings.Default.SpeechLanguageDefault;
        }

        public void Save()
        {
            _isolatedStorage.SaveData(nameof(AzureSpeechPrimaryKey), AzureSpeechPrimaryKey);
            _isolatedStorage.SaveData(nameof(AzureSpeechSecondaryKey), AzureSpeechSecondaryKey);

            SpeechkinAppSettings.Default.SpeechAuthUrl = AzureSpeechAuthUrl;
        }
    }
}
