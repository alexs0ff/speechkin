using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace SpeechkinApp.Speech
{
    public class DefaultMicrophoneSource:SoundSource
    {
        private readonly WaveIn _waveSource;

        private bool _started;

        public DefaultMicrophoneSource()
        {

            _waveSource = new WaveIn();
            _waveSource.WaveFormat = new WaveFormat(WaveFormats.Frequency, WaveFormats.Bits, WaveFormats.Channels);

            _waveSource.DataAvailable += (sender, args) =>
            {
                SendData(args.Buffer, args.BytesRecorded);
            };
        }

        public override void Dispose()
        {
            if (_waveSource != null)
            {
                _waveSource.Dispose();
            }
        }

        public override void Start()
        {
            if(_started)
            {
                return;
            }

            _waveSource.StartRecording();

            _started = true;
        }

        public override void Stop()
        {
            _waveSource.StopRecording();
            _started = false;
        }
    }
}
