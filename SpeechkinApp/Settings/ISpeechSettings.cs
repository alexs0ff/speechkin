namespace SpeechkinApp.Settings
{
    public interface ISpeechSettings
    {
        string AzureSpeechPrimaryKey { get; }

        string AzureSpeechSecondaryKey { get; }

        string AzureSpeechAuthUrl { get; }

        string SpeechLanguage { get; }
        int SelectedDataFlowId { get; }
        int InputDeviceIndex { get; }
        int SampleRateValue { get; }
        int BitsPerSampleValue { get; }
        int ChannelValue { get; }
    }
}
