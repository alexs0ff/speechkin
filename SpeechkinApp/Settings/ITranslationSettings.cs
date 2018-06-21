using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp.Settings
{
    public interface ITranslationSettings
    {
        string TranslatorUrl { get; }

        string TranslatorPrimaryKey { get; }

        TimeSpan TranslatorTimeout { get; }
    }
}
