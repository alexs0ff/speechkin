using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore;
using SpeechkinApp.Settings;

namespace SpeechkinApp.Speech
{
    public class WaveFormatAdapter
    {
        private readonly ISpeechSettings _speechSettings;

        public WaveFormatAdapter(ISpeechSettings speechSettings)
        {
            _speechSettings = speechSettings;
        }

        public WaveFormat WaveFormatFromCurrentSettings()
        {
            return new WaveFormat(_speechSettings.SampleRateValue, _speechSettings.BitsPerSampleValue,
                _speechSettings.ChannelValue);
        }
    }
}
