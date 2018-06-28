using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore.Codecs.WAV;
using SpeechkinApp.Settings;

namespace SpeechkinApp.Speech
{
    public class WaveWriterClient:IDisposable, ISoundDataDestination
    {
        private readonly WaveFormatAdapter _waveFormatAdapter;

        private readonly IDocumentsSettings _documentsSettings;

        private WaveWriter _waveWriter;

        private bool _isStarted;

        public WaveWriterClient(WaveFormatAdapter waveFormatAdapter, IDocumentsSettings documentsSettings)
        {
            _waveFormatAdapter = waveFormatAdapter;
            _documentsSettings = documentsSettings;
        }

        private string GetNewName()
        {
            var name = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".wav";
            return Path.Combine(_documentsSettings.DocumentsPath, name);
        }

        public bool Start()
        {
            if (!Directory.Exists(_documentsSettings.DocumentsPath))
            {
                return false;
            }

            if (_isStarted)
            {
                Stop();
            }

            _waveWriter = new WaveWriter(GetNewName(),_waveFormatAdapter.WaveFormatFromCurrentSettings());

            _isStarted = true;

            return true;
        }

        public void OnData(byte[] data, int count)
        {
            _waveWriter?.Write(data, 0, count);
        }

        public void Stop()
        {
            if (_isStarted)
            {
                _waveWriter.Dispose();
                _waveWriter = null;
            }

            _isStarted = false;
        }

        public void Dispose()
        {
            _waveWriter?.Dispose();
            _isStarted = false;
            _waveWriter = null;
        }
    }
}
