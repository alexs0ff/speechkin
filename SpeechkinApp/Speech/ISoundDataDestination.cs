using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp.Speech
{
    public interface ISoundDataDestination
    {
        void OnData(byte[] data, int actualBytes);
    }
}
