using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp.Translate
{
    public class TranslationResponse
    {
        public TranslationResponse()
        {
            TranslatedResults = new List<TranslatedResult>();
        }

        public List<TranslatedResult> TranslatedResults { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }
}
