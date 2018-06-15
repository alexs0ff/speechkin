namespace SpeechkinApp.Settings
{
    public interface ISpeechSettings
    {
        string AzureSpeechPrimaryKey { get; }

        string AzureSpeechSecondaryKey { get; }

        string AzureSpeechAuthUrl { get; }

        string SpeechLanguage { get; }

    }
}
