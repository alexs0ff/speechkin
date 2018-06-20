using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeechkinApp.Speech.SpeechEventArgs;

namespace SpeechkinApp.Speech
{
    public class RecognitionStartParameters
    {
        public Action<RecognitionItem> OnNewItemAction { get; set; }

        public Action<string> OnEnd { get; set; }
    }
}
