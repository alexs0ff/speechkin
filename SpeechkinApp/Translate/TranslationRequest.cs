using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp.Translate
{
    public class TranslationRequest
    {
        public TranslationRequest()
        {
            Items = new List<TranslationItem>();
        }

        public List<TranslationItem> Items { get; }

        public TranslationLanguage From { get; set; }

        public TranslationLanguage To { get; set; }
    }
}
