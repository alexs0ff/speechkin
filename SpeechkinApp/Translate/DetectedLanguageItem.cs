using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp.Translate
{
    public class DetectedLanguageItem
    {
        public string Language { get; set; }

        public float Score { get; set; }
    }
}
