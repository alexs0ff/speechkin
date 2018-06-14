using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.Compression;

namespace SpeechkinApp.Speech
{
    public class SystemSource:SoundSource
    {
        private readonly WasapiLoopbackCapture _waveIn;

        private AcmStream _acmStream;

        private bool _started;

        public SystemSource()
        {
            _waveIn = new WasapiLoopbackCapture();

            _waveIn.DataAvailable += (sender, args) =>
            {
                if (_acmStream==null)
                {
                    return;
                }

                byte[] newArray16Bit = new byte[args.BytesRecorded / 2];
                short two;
                float value;
                for (int i = 0, j = 0; i < args.BytesRecorded; i += 4, j += 2)
                {
                    value = (BitConverter.ToSingle(args.Buffer, i));
                    two = (short)(value * short.MaxValue);
                    newArray16Bit[j] = (byte)(two & 0xFF);
                    newArray16Bit[j + 1] = (byte)((two >> 8) & 0xFF);
                }

                Buffer.BlockCopy(newArray16Bit, 0, _acmStream.SourceBuffer, 0, newArray16Bit.Length);
                int sourceBytesConverted = 0;
                var convertedBytes = _acmStream.Convert(newArray16Bit.Length, out sourceBytesConverted);
                
                var converted = new byte[convertedBytes];
                Buffer.BlockCopy(_acmStream.DestBuffer, 0, converted, 0, convertedBytes);

                SendData(converted, convertedBytes);
            };

        }

        public override void Dispose()
        {
            
        }

        public override void Start()
        {
            if (_started)
            {
                return;
            }

            if (_acmStream!=null)
            {
                _acmStream.Dispose();
            }

            WaveFormat outf = new WaveFormat(WaveFormats.Frequency,WaveFormats.Bits, WaveFormats.Channels);

            var inFormat = new WaveFormat(_waveIn.WaveFormat.SampleRate, _waveIn.WaveFormat.Channels);

            _acmStream = new AcmStream(inFormat, outf);

            _started = true;
        }

        public override void Stop()
        {
            _started = false;
        }
    }
}
