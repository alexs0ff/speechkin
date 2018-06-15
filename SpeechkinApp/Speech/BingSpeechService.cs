﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.SpeechRecognition;
using SpeechkinApp.Settings;

namespace SpeechkinApp.Speech
{
    public class BingSpeechService:ISoundDataDestination, IDisposable
    {
        private readonly ISpeechSettings _speechSettings;

        private DataRecognitionClient _client;

        private bool _started;

        public BingSpeechService(ISpeechSettings speechSettings)
        {
            _speechSettings = speechSettings;
        }

        public void OnData(byte[] data, int actualBytes)
        {
            if (!_started)
            {
                return;
            }

            _client.SendAudio(data,actualBytes);
        }

        public void Start()
        {
            _client?.Dispose();

            _client = SpeechRecognitionServiceFactory.CreateDataClient(
                SpeechRecognitionMode.LongDictation,
                _speechSettings.SpeechLanguage,
                _speechSettings.AzureSpeechPrimaryKey, _speechSettings.AzureSpeechSecondaryKey,
                _speechSettings.AzureSpeechAuthUrl);

            _started = true;
            _client.SendAudioFormat(SpeechAudioFormat.create16BitPCMFormat(WaveFormats.Frequency));
        }

        public void Stop()
        {
            _client?.EndAudio();

            _client?.Dispose();
            _client = null;
            _started = false;

        }

        public void Dispose()
        {
            _client?.Dispose();
            _started = false;
        }
    }
}
