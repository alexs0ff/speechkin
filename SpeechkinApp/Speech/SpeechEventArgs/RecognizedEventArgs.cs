using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp.Speech.SpeechEventArgs
{
    public class RecognizedEventArgs:EventArgs
    {
        public RecognizedEventArgs(RecognitionItem item)
        {
            Item = item;
        }

        public RecognitionItem Item { get; }
    }
}
