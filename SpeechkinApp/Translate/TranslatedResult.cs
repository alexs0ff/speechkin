using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp.Translate
{
    public class TranslatedResult
    {
        public DetectedLanguageItem DetectedLanguage { get; set; }

        public List<TranslatedItem> Translations { get; set; }
    }
}
