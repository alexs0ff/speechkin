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

        public int SelectedDataFlowId { get; private set; }

        public int InputDeviceIndex { get; private set; }

        public int SampleRateValue { get; private set; }

        public int BitsPerSampleValue { get; private set; }

        public int ChannelValue { get; private set; }


        public void Load()
        {
            AzureSpeechPrimaryKey = _isolatedStorage.GetData(nameof(AzureSpeechPrimaryKey));
            AzureSpeechSecondaryKey = _isolatedStorage.GetData(nameof(AzureSpeechSecondaryKey));
            AzureSpeechAuthUrl = SpeechkinAppSettings.Default.SpeechAuthUrl;
            SpeechLanguage = SpeechkinAppSettings.Default.SpeechLanguageDefault;
            SelectedDataFlowId = SpeechkinAppSettings.Default.SelectedDataFlowId;
            InputDeviceIndex = SpeechkinAppSettings.Default.InputDeviceIndex;
            SampleRateValue = SpeechkinAppSettings.Default.SampleRateValue;
            BitsPerSampleValue = SpeechkinAppSettings.Default.BitsPerSampleValue;
            ChannelValue = SpeechkinAppSettings.Default.ChannelValue;
        }

        public void Save()
        {
            _isolatedStorage.SaveData(nameof(AzureSpeechPrimaryKey), AzureSpeechPrimaryKey);
            _isolatedStorage.SaveData(nameof(AzureSpeechSecondaryKey), AzureSpeechSecondaryKey);

            SpeechkinAppSettings.Default.SpeechAuthUrl = AzureSpeechAuthUrl;
            SpeechkinAppSettings.Default.SelectedDataFlowId = SelectedDataFlowId;
            SpeechkinAppSettings.Default.InputDeviceIndex = InputDeviceIndex;
            SpeechkinAppSettings.Default.SampleRateValue = SampleRateValue;
            SpeechkinAppSettings.Default.BitsPerSampleValue = BitsPerSampleValue;
            SpeechkinAppSettings.Default.ChannelValue = ChannelValue;

            SpeechkinAppSettings.Default.Save();
        }
    }
}
