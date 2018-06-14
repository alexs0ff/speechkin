using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp.Speech
{
    public interface ISpeechSettings
    {
        string AzureSpeechPrimaryKey { get; }

        string AzureSpeechSecondaryKey { get; }

        string AzureAuthUrl { get; }

        string SpeechLanguage { get; }

    }
}
