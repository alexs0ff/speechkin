using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
using SpeechkinApp.Settings;

namespace SpeechkinApp.Speech
{
    public class SystemSource:SoundSource
    {
        private readonly ISpeechSettings _speechSettings;

        private readonly WaveFormatAdapter _waveFormatAdapter;

        private WasapiCapture _soundIn;

        private IWaveSource _waveSource;

        private bool _started;

        public SystemSource(ISpeechSettings speechSettings, WaveFormatAdapter waveFormatAdapter)
        {
            _speechSettings = speechSettings;
            _waveFormatAdapter = waveFormatAdapter;
        }

        public override void Dispose()
        {
            _waveSource?.Dispose();
            _soundIn?.Dispose();
        }

        public override void Start()
        {
            if (_started)
            {
                Stop();
            }
            DataFlow dataFlow = (DataFlow)_speechSettings.SelectedDataFlowId;

            var devices = MMDeviceEnumerator.EnumerateDevices(dataFlow, DeviceState.Active);

            
            if (devices.Count -1 <_speechSettings.InputDeviceIndex)
            {
                throw new Exception($" device Index {_speechSettings.InputDeviceIndex} is not avalibe");
            }

            if (dataFlow == DataFlow.Render)
            {
                var wasapiFormat = _waveFormatAdapter.WaveFormatFromCurrentSettings();
                _soundIn = new WasapiLoopbackCapture(100, wasapiFormat);
            }
            else
            {
                _soundIn = new WasapiCapture();
            }

            _soundIn.Device = devices[_speechSettings.InputDeviceIndex];
            
            _soundIn.Initialize();

            var wasapiCaptureSource = new SoundInSource(_soundIn) { FillWithZeros = false };

            _waveSource = wasapiCaptureSource
                .ChangeSampleRate(_speechSettings.SampleRateValue) // sample rate
                .ToSampleSource()
                .ToWaveSource(_speechSettings.BitsPerSampleValue); //bits per sample;

            if (_speechSettings.ChannelValue == 1)
            {
                _waveSource = _waveSource.ToMono();
            }
            else
            {
                _waveSource = _waveSource.ToStereo();
            }

            
            wasapiCaptureSource.DataAvailable += (s, e) =>
            {
                //read data from the converedSource
                //important: don't use the e.Data here
                //the e.Data contains the raw data provided by the 
                //soundInSource which won't have your target format
                byte[] buffer = new byte[_waveSource.WaveFormat.BytesPerSecond / 2];
                int read;

                //keep reading as long as we still get some data
                //if you're using such a loop, make sure that soundInSource.FillWithZeros is set to false
                while ((read = _waveSource.Read(buffer, 0, buffer.Length)) > 0)
                {
                    SendData(buffer,read);
                }
            };

            _soundIn.Start();

            _started = true;
        }

        public override void Stop()
        {
            _started = false;
            _soundIn?.Stop();
            Dispose();
        }
    }
}
